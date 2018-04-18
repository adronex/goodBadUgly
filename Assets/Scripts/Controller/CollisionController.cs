using UnityEngine;

namespace Controller
{
    public static class CollisionController
    {
        #region Public methods
        public static int GetCollidedBodyPartId(Vector2 previous,Vector2 current, BodyPart[] bodyParts)
        {
            bool success = false;
            for (int id = 0; id < bodyParts.Length; id++)
            {
                Vector2[] points = GetRealPoints(bodyParts[id]);

                success = Intersect(previous, current, points[0], points[1]);
                if (!success)
                {
                    success = Intersect(previous, current, points[1], points[2]);
                }
                else if (!success)
                {
                    success = Intersect(previous, current, points[2], points[3]);
                }
                else if (!success)
                {
                    success = Intersect(previous, current, points[3], points[0]);
                }

                if (success)
                {
                    return id;
                }
            }
            return -1;
        }
        #endregion
        #region Private methods
        private static Vector2[] GetRealPoints(BodyPart bodyPart)
        {
            var pos = bodyPart.Transform.position;

            return new[]
                {
                    new Vector2(pos.x - bodyPart.HalfWidth, pos.y - bodyPart.HalfHeight),
                    new Vector2(pos.x + bodyPart.HalfWidth, pos.y - bodyPart.HalfHeight),
                    new Vector2(pos.x + bodyPart.HalfWidth, pos.y + bodyPart.HalfHeight),
                    new Vector2(pos.x - bodyPart.HalfWidth, pos.y + bodyPart.HalfHeight),
                };
        }


        private static bool Intersect(Vector2 a, Vector2 b, Vector2 c, Vector2 d)
        {
            if (!Intersect(a.x, b.x, c.x, d.x) || !Intersect(a.y, b.y, c.y, d.y))
            {
                return false;
            }

            Vector3 m = GetLine(a, b);
            Vector3 n = GetLine(c, d);

            float zn = Det(m.x, m.y, n.x, n.y);
            if (Mathf.Abs(zn) < Mathf.Epsilon)
            {
                if (Mathf.Abs(Dist(m, c)) > Mathf.Epsilon || Mathf.Abs(Dist(n, a)) > Mathf.Epsilon)
                {
                    return false;
                }

                if (CompareVector2(b, a))
                {
                    Swap(ref a, ref b);
                }

                if (CompareVector2(d, c))
                {
                    Swap(ref c, ref d);
                }

                return true;
            }
            else
            {
                Vector2 line;
                line.x = -Det(m.z, m.y, n.z, n.y) / zn;
                line.y = -Det(m.x, m.z, n.x, n.z) / zn;
                return IsBetweenPoints(a.x, b.x, line.x)
                    && IsBetweenPoints(a.y, b.y, line.y)
                    && IsBetweenPoints(c.x, d.x, line.x)
                    && IsBetweenPoints(c.y, d.y, line.y);
            }
        }

        private static Vector3 GetLine(Vector2 p, Vector2 q)
        {
            var a = p.y - q.y;
            var b = q.x - p.x;
            var c = (-a * p.x) - (b * p.y);

            float z = Mathf.Sqrt(a * a + b * b);
            if (Mathf.Abs(z) > Mathf.Epsilon)
            {
                a /= z;
                b /= z;
                c /= z;
            }

            return new Vector3(a, b, c);
        }

        private static float Dist(Vector3 line, Vector2 point)
        {
            return line.x * point.x + line.y * point.y + line.z;
        }

        private static bool IsBetweenPoints(float l, float r, float x)
        {
            return Mathf.Min(l, r) <= x + Mathf.Epsilon && x <= Mathf.Max(l, r) + Mathf.Epsilon;
        }

        private static bool Intersect(float a, float b, float c, float d)
        {
            if (a > b) Swap(ref a, ref b);
            if (c > d) Swap(ref c, ref d);
            return Mathf.Max(a, c) <= Mathf.Min(b, d) + Mathf.Epsilon;
        }

        private static void Swap(ref float a, ref float b)
        {
            var temp = a;
            a = b;
            b = temp;
        }

        private static void Swap(ref Vector2 a, ref Vector2 b)
        {
            var temp = a;
            a = b;
            b = temp;
        }

        private static float Det(float a, float b, float c, float d)
        {
            return (a * d - b * c);
        }

        private static bool CompareVector2(Vector2 a, Vector2 b)
        {
            return a.x < b.x - Mathf.Epsilon || Mathf.Abs(a.x - b.x) < Mathf.Epsilon && a.y < b.y - Mathf.Epsilon;
        }
        #endregion
    }
}