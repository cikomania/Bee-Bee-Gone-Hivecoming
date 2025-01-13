using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    
    public static LevelController instance; //bu scripte her yerden kolay eriþim için ekleniyor
        
    [SerializeField] Timer timer;
    [SerializeField] FlowerCounter flowerCounter;
    [SerializeField] LevelDisplayText levelDisplayText;

    GameObject[] flowers;
    public GameObject flowerPrefab;
    public int totalFlowers;
    int collectedFlowers;

    void Start()
    {
        flowers = GameObject.FindGameObjectsWithTag("Flower");
        totalFlowers = flowers.Length;
        collectedFlowers = 0;

        ShowLevelAtStart();
    }

    void Update()
    {
        if (timer.remainingTime <= 1)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        SceneManager.LoadScene("Game Over");
    }

    public void CollectFlower()
    {
        collectedFlowers++;
        flowerCounter.FlowerCollected();

        if (collectedFlowers >= totalFlowers)
        {
            NextScene();
        }
    }

    void NextScene()
    {
        int remainingSeconds = Mathf.FloorToInt(timer.GetRemainingTime());

        Score.AddScore(remainingSeconds); 
        PlayerPrefs.SetInt("AddedScore", remainingSeconds);
        PlayerPrefs.Save();

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1; 

        PlayerPrefs.SetInt("CurrentLevel", nextSceneIndex - 4); //level 1'in indeksi 5, bu nedenle -4
        PlayerPrefs.Save();

        SceneManager.LoadScene(nextSceneIndex);
    }

    void ShowLevelAtStart()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (currentSceneIndex >= 5)
        {
            int levelToDisplay = (currentSceneIndex - 5) + 1; //5 - 5 + 1 = 1 yani level 1
            levelDisplayText.gameObject.SetActive(true);
            levelDisplayText.ShowLevelText(levelToDisplay);
        }
    }
}
