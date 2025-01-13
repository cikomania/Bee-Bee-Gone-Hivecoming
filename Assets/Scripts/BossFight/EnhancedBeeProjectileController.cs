using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnhancedBeeProjectileController : MonoBehaviour
{
    AudioManager audioManager;
    BossController bossController;

    public float lifetime = 1f;

    void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        bossController = FindObjectOfType<BossController>();
    }

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyBody"))
        {
            Destroy(gameObject);

            Transform enemyParent = collision.transform.parent;
            if (enemyParent != null && enemyParent.CompareTag("Enemy"))
            {
                audioManager.PlaySFX(audioManager.enemyGetsHit);
                Destroy(enemyParent.gameObject);
            }
        }
        else if (collision.CompareTag("BossBeeBody"))
        {
            Destroy(gameObject);
            bossController.TakeDamage();           
        }
    }
}
