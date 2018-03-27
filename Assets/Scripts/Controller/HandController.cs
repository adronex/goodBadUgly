using UnityEngine;

namespace Controller
{
    public class HandController
    {
        private readonly Transform axis; //плечо
        private readonly Transform gunpoint; //

        public Transform GunPoint { get { return gunpoint; } }

        public HandController(Transform handAxis)
        {
            axis = handAxis;
            gunpoint = axis.Find("ArmLower").Find("Hand").Find("Gun").Find("Gunpoint");
        }

        public void LookTo(Vector2 aim) //INCORRECT METHOD! todo: 
        {
            //aim - куда навели мышкой
            //axis - плечо, которым управляем
            //gunpoint - дуло пистолета

            //var target = (Vector3)aim - gunpoint.position;
            //var a = Quaternion.LookRotation(Vector3.Scale(target, new Vector3(1, 1, 0)), Vector3.down);
            //var rotation = Quaternion.LookRotation(target, Vector3.);
            //gunpoint.rotation = a;

            //Vector3 global_vector = (Vector3)axis.position * axis.rotation;

            //axis.rotation = rotation;
            //var localEuler = axis.localRotation.eulerAngles;
            //var newLocalEuler = new Vector3(0, 0, localEuler.x + 90);
            //axis.localRotation = Quaternion.Euler(newLocalEuler);

            //  var target = gunpoint.position - (Vector3)aim;
            //  var temp = Quaternion.LookRotation(target);
            //  transform.rotation = temp;
            //  var local = transform.localRotation.eulerAngles;
            //  var newLocal = new Vector3(0, 0, local.x);
            //  transform.localRotation = Quaternion.Euler(newLocal);

            /*
            """ Get rotation Quaternion between 2 vectors """
            v1.normalize(), v2.normalize()
            v = v1+v2
            v.normalize()
            angle = v.dot(v2)
            axis = v.cross(v2)
            return Quaternion( angle, *axis ) 
             */

            LookAt2D(axis, -axis.up, aim);
        }


        public void LookAt2D(Transform me, Vector2 eye, Vector2 target)
        {
            Vector2 look = target - (Vector2)me.position;

            float angle = Vector2.Angle(eye, look);

            Vector2 right = Vector3.Cross(Vector3.forward, look);

            int dir = 1;

            if (Vector2.Angle(right, eye) < 90)
            {
                dir = -1;
            }

            me.rotation *= Quaternion.AngleAxis(angle * dir, Vector3.forward);
        }
    }
}