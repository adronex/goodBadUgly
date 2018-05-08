using System;
using UnityEngine;

namespace Graphics
{
    public class Bullet : MonoBehaviour
    {
        #region Fields
        private float speed;
        #endregion

        #region Properties
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
        private void FixedUpdate()
        {
            if (!gameObject.activeSelf)
            {
                return;
            }

            var position = transform.position;
            if (Mathf.Abs(position.x) > 20 || Mathf.Abs(position.y) > 20)
            {
                gameObject.SetActive(false);
            }

            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, speed * Time.deltaTime, 1 << 8);
            if (hit.collider != null)
            {
                CreateBlood(hit.transform, transform);
            }

            transform.position += transform.right * speed * Time.deltaTime;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            CreateBlood(collision.transform, transform);
        }
        #endregion

        #region Public fields
        internal void Create(Vector2 position, Quaternion rotation, float speed)
        {
            transform.position = position;
            transform.rotation = rotation;
            this.speed = speed;

            gameObject.SetActive(true);
        }
        #endregion

        private void CreateBlood(Transform collision, Transform bullet)
        {
            gameObject.SetActive(false);
            ClientGraphic.CreateBlood(collision, bullet);
        }
    }
}
