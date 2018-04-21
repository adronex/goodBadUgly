using System;
using Core;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    #region Fields
    private static Quaternion AddRotation = Quaternion.Euler(0, 0, -90);

    private static Transform bulletStorage;

    private float bulletSpeed;

    public float BulletSpeed
    {
        get { return bulletSpeed; }
    }
    #endregion
    #region Unity lifecycle

    private void Update()
    {
        transform.position += transform.right * bulletSpeed * Time.deltaTime;
    }

    public static Transform CreateBullet(GameObject prefab, float speed, Transform gunpoint, Vector3 offset, out Vector2 bulletRotation)
    {
        if (bulletStorage == null)
        {
            bulletStorage = new GameObject("BulletStorage").transform;
        }

        var bullet = Instantiate(prefab, gunpoint.position - offset, gunpoint.rotation * AddRotation, bulletStorage);
        bullet.GetComponent<Bullet>().bulletSpeed = speed;

        var transform = bullet.transform;
        bulletRotation = transform.right;
        return transform;
    }

    internal static void DestroyBullet(BulletInfo bulletInfo)
    {
        DestroyBullet(bulletInfo.transform);
    }

    internal static void DestroyBullet(Transform bullet)
    {
        var gameObject = bullet.gameObject;
        Destroy(gameObject);
    }
    #endregion
}
