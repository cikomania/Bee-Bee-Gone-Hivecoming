using System.Collections;
using TMPro;
using UnityEngine;

public class LivesCounter : MonoBehaviour
{    
    [SerializeField] TextMeshProUGUI livesCounterText;

    BeeController beeController;
    PrefabSpawner prefabSpawner;
    void Start()
    {        
        prefabSpawner = FindObjectOfType<PrefabSpawner>();
        StartCoroutine(WaitForBee()); //PrefabSpawner taraf�ndan spawnlanan ar�n�n sahneye geli�i BeeController'a g�re ge� kal�yor bu nedenle ekledim
                     
    }

    IEnumerator WaitForBee()
    {
        while (beeController == null)
        {
            if (prefabSpawner != null)
            {
                GameObject bee = prefabSpawner.GetSpawnedBee();
                if (bee != null)
                {
                    beeController = bee.GetComponent<BeeController>();
                }
            }
            yield return null;
        }

        UpdateLivesCounter();
    }

    void Update()
    {
        if (beeController != null)
        {
            UpdateLivesCounter();
        }
    }

    void UpdateLivesCounter()
    {
        int displayedLives = Mathf.Max(0, beeController.lives);
        livesCounterText.text = displayedLives.ToString();
    }
}
