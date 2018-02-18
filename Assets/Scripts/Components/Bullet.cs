using UnityEngine;

public class Bullet : MonoBehaviour
{
    private const float speed = 15f;

    private void Update()
    {
        //The bag when the handAxis's x is 2.88; 
        transform.position += transform.right * speed * Time.deltaTime;
    }
}
