using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    Transform bee;

    public int bossHealth = 50;

    public GameObject bossProjectile;
    public Transform needle;
    public float fireSpeed = 9f;
    public float fireRate = 3f;
    public float fireCoolDown = 0.5f;

    Animator animator;
    int phase = 1;

    EnemyBeeSpawner enemyBeeSpawner;

    void Start()
    {
        animator = GetComponent<Animator>();
        enemyBeeSpawner = FindObjectOfType<EnemyBeeSpawner>();
    }

    void Update()
    {
        if (bossHealth <= 0)
        {
            if (enemyBeeSpawner != null)
            {
                enemyBeeSpawner.StopSpawning();
            }
            Destroy(gameObject);
        }

        CheckPhase();

        if (bee != null)
        {
            Fire();
        }
    }

    void CheckPhase()
    {
        if (bossHealth <= 25 && phase == 1)
        {
            phase = 2;
            animator.SetTrigger("PhaseTwo");

            if (enemyBeeSpawner != null)
            {
                enemyBeeSpawner.StartSpawning();
            }
        }
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
            GameObject projectile = Instantiate(bossProjectile, needle.position, Quaternion.identity);
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
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            bee = null;
        }
    }

    public void TakeDamage()
    {
        bossHealth--;
        if (bossHealth < 0)
        {
            Destroy(gameObject);
        }        
    }
}
