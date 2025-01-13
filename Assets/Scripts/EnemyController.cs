using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Transform bee;
    bool isChasing = false;
    public float speed = 2.9f;

    public GameObject enemyProjectile;
    public Transform needle;
    public float fireSpeed = 8f;
    public float fireRate = 0.5f;
    public float fireCoolDown = 0.1f;

    void Update()
    {
        if (isChasing && bee != null)
        {
            MoveTowardsBee();
            Fire();
        }
    }

    void MoveTowardsBee()
    {
        Vector3 direction = (bee.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }

    void Fire()
    {        
        if (fireCoolDown <= 0f)
        {
            FireProjectile();
            fireCoolDown = fireRate;
        }
        fireCoolDown -= Time.deltaTime;
    }

    void FireProjectile()
    {
        if (needle != null && bee != null)
        {
            GameObject projectile = Instantiate(enemyProjectile, needle.position, Quaternion.identity);
            Vector3 direction = (bee.position - needle.position).normalized;
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = direction * fireSpeed;
            }
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            bee = collision.transform;
            isChasing = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isChasing = false;
            bee = null;
        }
    }
}
