using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossFightSceneController : MonoBehaviour
{
    public BeeController bee;
    public GameObject bossBee;
    public Timer timer;

    void Start()
    {   
        if (bee == null)
        {
            bee = FindObjectOfType<BeeController>();
        }

        if (bossBee == null)
        {
            bossBee = GameObject.FindWithTag("BossBee");
        }
    }

    void Update()
    {
        if (timer.remainingTime <= 1 || bee.lives <= 0)
        {
            SceneManager.LoadScene("Game Over");
        }
        
        if (bossBee == null)
        {
            SceneManager.LoadScene("Ending Cutscene");
        }
    }
}
