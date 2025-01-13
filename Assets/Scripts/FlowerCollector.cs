using UnityEngine;

public class FlowerCollector : MonoBehaviour
{
    AudioManager audioManager;
    LevelController levelController;
    void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    void Start()
    {
        levelController = FindObjectOfType<LevelController>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        audioManager.PlaySFX(audioManager.score);
        Score.score += 10;

        if (other.CompareTag("Player"))            
        {
            levelController.CollectFlower();
            Destroy(gameObject);                
        }
            
        
    }
}
