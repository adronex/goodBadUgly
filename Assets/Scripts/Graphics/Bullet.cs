using System;
using UI;
using UnityEngine;

namespace Graphics
{
    public class Bullet : MonoBehaviour
    {
        #region Fields
        private float speed;
        private Rigidbody2D rigidBody;
        private SpriteRenderer spriteRenderer;
        #endregion
        #region Properties
        public float Speed
        {
            get { return speed; }
        }


        internal bool IsBusy
        {
            get { return !gameObject.activeSelf; }
        }


        internal float DistanceFromWorldCenter
        {
            get
            {
                var position = transform.position;
                return Math.Abs(position.x) + Mathf.Abs(position.y);
            }
        }
        #endregion
        #region Unity lifecycle
        private void Awake()
        {
            rigidBody = GetComponent<Rigidbody2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }


        private void FixedUpdate()
        {
            if (!gameObject.activeSelf)
            {
                return;
            }

            Vector3 position = rigidBody.position;
            if (Mathf.Abs(position.x) > Helps.MaxWorldPosition || Mathf.Abs(position.y) > Helps.MaxWorldPosition)
            {
                gameObject.SetActive(false);
            }

            RaycastHit2D hit = Physics2D.Raycast(position, Vector2.right, speed * Time.deltaTime, Helps.HeroLayer);
            if (hit.collider != null)
            {
                CreateBlood(hit);
            }

            var newPosition = position + transform.right * speed * Time.deltaTime;

            rigidBody.MovePosition(newPosition);
        }
        #endregion
        #region Public fields
        internal void Create(Sprite bulletSprite, Vector2 position, Quaternion rotation, float speed)
        {
            if (spriteRenderer.sprite != bulletSprite)
            {
                spriteRenderer.sprite = bulletSprite;
            }

            transform.position = position;
            transform.localEulerAngles = new Vector3(0f, 0f, rotation.eulerAngles.z);
            this.speed = speed;

            gameObject.SetActive(true);
        }
        #endregion
        private void CreateBlood(RaycastHit2D hit)
        {
            gameObject.SetActive(false);

            var offset = transform.right * hit.distance;
            ClientGraphic.CreateBlood(hit.transform, transform, offset);

            Hat(hit);
        }


        private void Hat(RaycastHit2D hit)
        {
            var root = hit.transform;
            while (root.parent != null)
            {
                root = root.parent;
            }

            var angle = 90 - Vector3.Angle(transform.right, hit.normal);
            
            var ssa = Quaternion.AngleAxis(angle, Vector2.up);
            var sssa = ssa * Vector2.up;

            var rb2d = root.GetComponent<HeroInfo>().Hat.GetComponent<Rigidbody2D>();
            if (rb2d.IsSleeping())
            {
                root.GetComponent<HeroInfo>().Hat.parent = null;
                rb2d.WakeUp();
                rb2d.AddForce(transform.right * Helps.AddedForce);
                rb2d.AddTorque(Helps.AddedTorque);
            }
        }
    }
}
