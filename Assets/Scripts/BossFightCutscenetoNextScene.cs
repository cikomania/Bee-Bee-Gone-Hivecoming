using UnityEngine;
using UnityEngine.SceneManagement;

public class BossFightCutscenetoNextScene : MonoBehaviour
{
    public float changeTime;

    void Update()
    {
        changeTime -= Time.deltaTime;
        if (changeTime <= 0)
        {
            SceneManager.LoadScene("Boss Fight");
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Boss Fight");
        }
    }
}
