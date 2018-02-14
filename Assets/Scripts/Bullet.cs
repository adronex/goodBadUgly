using UnityEngine;

public class Bullet : MonoBehaviour
{
    private const float speed = 15f;

    private Transform enemy;
    private BodyPart[] bodyParts;


    private void Awake()
    {
        enemy = GameObject.Find("EnemyHero").transform;

        bodyParts = new BodyPart[enemy.childCount];

        for (int i = 0; i < enemy.childCount; i++)
        {
            bodyParts[i] = new BodyPart(enemy.GetChild(i).transform);
        }
    }


    private void Update()
    {
        //The bag when the handAxis's x is 2.88; 
        transform.position += transform.right * speed * Time.deltaTime;

        for (int i = 0; i < bodyParts.Length; i++)
        {
            if (bodyParts[i].Check(transform.position))
            {
                bodyParts[i].GetDamage();
                Destroy(gameObject);
                break;
            }
        }
    }
}
