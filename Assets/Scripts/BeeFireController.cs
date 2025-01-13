using UnityEngine;
using UnityEngine.SceneManagement;

public class BeeFireController : MonoBehaviour
{
    public GameObject beeProjectile;
    public Transform mouth;
    public float fireSpeed = 4f;
    public int unlockFireLevel = 6;
    public int currentLevel;

    BeeController beeController;
    void Start()
    {
        beeController = GetComponent<BeeController>();
        currentLevel = DetermineCurrentLevel(); //Bossfight sahnesinin levelini görmediði için ekledim
        Debug.Log("Current Level: " + currentLevel);
    }
    void Update()
    {
        if (beeController != null && beeController.isDead) 
        {
            return; //arý ölünce ateþ etmemesi için
        }

        if (currentLevel >= unlockFireLevel && Input.GetKeyDown(KeyCode.Space))
        {
            FireProjectile();
        }
    }

    int DetermineCurrentLevel()
    {
        if (SceneManager.GetActiveScene().name == "Bossfight")
        {
            return 11;
        }
        else
        {
            return PlayerPrefs.GetInt("CurrentLevel", 1);
        }
    }

    void FireProjectile()
    {
        if(beeProjectile != null && mouth != null)
        {
            GameObject projectile = Instantiate(beeProjectile, mouth.position, Quaternion.identity);
            Vector2 direction = Vector2.up;

            //Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z * -1));
            //Vector3 direction = (mousePosition - mouth.position).normalized;

            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                float adjustedSpeed = fireSpeed + Camera.main.GetComponent<CameraController>().speed * 2f;
                rb.velocity = direction * adjustedSpeed;
            }
        }
    }
}
