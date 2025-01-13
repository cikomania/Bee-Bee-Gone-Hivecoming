using System.Collections;
using UnityEngine;

public class BeeController : MonoBehaviour
{
    float normalSpeed = 3.5f;
    float withrainSpeed = 2.7f;
    float speed;
    float teleportSpeed = 3.7f;
    float fallSpeed = 0.8f;
    float rotationSpeed = 4f;
    bool isOnLeaf = false;
    public int lives = 2;
    public bool isDead = false;
    public GameObject Rain;

    Vector3 velocity;
    Animator animator;
    AudioManager audioManager;
    Collider2D[] colliders;
    SpriteRenderer spriteRenderer;    
    [SerializeField] Sprite deathSprite;

    LevelController levelController;

    void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Start()
    {        
        animator = GetComponent<Animator>();
        colliders = GetComponents<Collider2D>();        
        spriteRenderer = GetComponent<SpriteRenderer>();
        levelController = FindObjectOfType<LevelController>();
        speed = normalSpeed;
    }

    void FixedUpdate()
    {
        CheckRainStatus();
        if (lives >= 0 && !isDead)
        {
            Move();
        }
    }

    void CheckRainStatus()
    {
        GameObject rain = GameObject.FindWithTag("Rain");
        if (rain != null)
        {
            speed = withrainSpeed;
        }
        else
        {
            speed = normalSpeed;
        }
    }

    void Move()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        GameObject rain = GameObject.FindWithTag("Rain");

        if (rain != null && vertical > 0)
        {
            animator.SetBool("isWet", true);
            audioManager.moveSource.enabled = true;
        }
        else
        {
            animator.SetBool("isWet", false);
            if (vertical > 0)
            {
                audioManager.moveSource.enabled = true;
            }
            else
            {
                audioManager.moveSource.enabled = false;
            }
        }

        if (isOnLeaf || vertical > 0)
        {            
            velocity = new Vector3(horizontal, vertical, 0f);
            animator.SetBool("isMoving", vertical > 0);
                       
            if (vertical > 0)
            {
                audioManager.moveSource.enabled = true;
            }
            else
            {
                audioManager.moveSource.enabled = false;
            }
        }
        else
        {
            audioManager.moveSource.enabled = false;
            velocity = new Vector3(horizontal, -fallSpeed, 0f);
            animator.SetBool("isMoving", false);
        }

        transform.position += velocity * speed * Time.deltaTime;


        Quaternion targetRotation;

        if (horizontal > 0)
        {
            targetRotation = Quaternion.Euler(0, 0, -15);
        }
        else if (horizontal < 0)
        {
            targetRotation = Quaternion.Euler(0, 0, 20);
        }
        else
        {
            targetRotation = Quaternion.Euler(0, 0, 0);
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Leaf"))
        {
            isOnLeaf = true;
        }
        else if (collision.CompareTag("Thorn"))
        {
            TakeDamage();
        }
        else if (collision.CompareTag("ManEater"))
        {
            TakeDamage();
        }
        else if (collision.CompareTag("ManEaterRadar"))
        {
            Animator maneateranim = collision.GetComponentInParent<Animator>();
            if (maneateranim != null )
            {
                maneateranim.SetBool("isActivated", true);
            }
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Leaf"))
        {
            isOnLeaf = false;
        }
        else if (collision.CompareTag("ManEaterRadar"))
        {
            Animator maneateranim = collision.GetComponentInParent<Animator>();
            if (maneateranim != null)
            {
                maneateranim.SetBool("isActivated", false);
            }
        }
    }

    bool isTakingDamage = false; //TakeDamage() metodu birden fazla kez tetiklendiði için bayrak(flag) ekledim
    void TakeDamage()
    {
        if (isTakingDamage) return; //zaten zarar alýyorsa iþlem durur
        isTakingDamage = true;

        lives--;
        if (lives >= 0)
        {
            StartCoroutine(DeathAnimation());
        }
        else
        {
            lives = 0;
            if (audioManager != null && audioManager.moveSource != null)
            {
                audioManager.moveSource.enabled = false;
            }
            levelController.GameOver();
        }

        StartCoroutine(ResetTakingDamage());
    }

    IEnumerator ResetTakingDamage()
    {
        yield return new WaitForSeconds(0.5f);
        isTakingDamage = false;
    }

    IEnumerator DeathAnimation()
    {
        audioManager.moveSource.enabled = false;
        audioManager.PlaySFX(audioManager.death);
        isDead = true;
        SetCollidersActive(false);
        animator.enabled = false;
        spriteRenderer.sprite = deathSprite;
        yield return StartCoroutine(MoveToNearestLeaf());
        animator.enabled = true;
        SetCollidersActive(true);
        isDead = false;
    }

    void SetCollidersActive(bool isActive)
    {
        foreach (Collider2D collider in colliders)
        {
            collider.enabled = isActive;
        }
    }

    IEnumerator MoveToNearestLeaf()
    {
        GameObject nearestLeaf = FindNearestLeaf();
        if (nearestLeaf != null)
        {
            Collider2D leafCollider = nearestLeaf.GetComponent<Collider2D>();

            //yukarý doðru collider'ýn yüksekliðinin yarýsý kadar bir mesafe eklenir (yapraðýn üstünü hesaplamak için)
            Vector2 leafTop = (Vector2)nearestLeaf.transform.position + (Vector2.up * (leafCollider.bounds.extents.y));
            Vector2 targetPosition = leafTop + Vector2.up * 0.7f;

            float distance = Vector2.Distance(transform.position, targetPosition);
            float duration = distance / teleportSpeed;
            float elapsedTime = 0f;
            Vector2 startPosition = transform.position;

            while (elapsedTime < duration) //arý hedefe ulaþana kadar yapýlýr
            {
                transform.position = Vector2.Lerp(startPosition, targetPosition, elapsedTime / duration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            transform.position = targetPosition;
        }
    }

    GameObject FindNearestLeaf()
    {
        GameObject[] leaves = GameObject.FindGameObjectsWithTag("Leaf");
        GameObject nearestLeaf = null;
        float closestDistance = Mathf.Infinity; //baþlangýçta en yakýn mesafe sýnýrsýz ayarlanýr

        foreach (GameObject leaf in leaves)
        {
            float distance = Vector2.Distance(transform.position, leaf.transform.position);
            if (distance < closestDistance)
            {
                nearestLeaf = leaf;
                closestDistance = distance;
            }
        }
        return nearestLeaf;
    }

}
