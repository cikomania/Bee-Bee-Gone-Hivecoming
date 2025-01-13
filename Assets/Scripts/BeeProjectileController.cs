using UnityEngine;

public class BeeProjectileController : MonoBehaviour
{
    AudioManager audioManager;

    public float lifetime = 0.75f;

    void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyBody"))
        {
            Destroy(gameObject); //projectile yok edilir

            Transform enemyParent = collision.transform.parent;
            if (enemyParent != null && enemyParent.CompareTag("Enemy"))
            {
                audioManager.PlaySFX(audioManager.enemyGetsHit);
                Score.score += 25;
                Destroy(enemyParent.gameObject);
            }
        }
    }
}
