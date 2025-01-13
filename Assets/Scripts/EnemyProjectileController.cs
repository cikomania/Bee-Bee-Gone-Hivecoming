using UnityEngine;

public class EnemyProjectileController : MonoBehaviour
{
    public float lifetime = 5f;
    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
