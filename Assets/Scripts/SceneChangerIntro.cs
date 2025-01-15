using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangerIntro : MonoBehaviour
{
    //intro cutscene için

    public float changeTime;

    void Update()
    {
        changeTime -= Time.deltaTime;
        if (changeTime <= 0)
        {
            SceneManager.LoadScene("Main Menu");
        }
    }
}
