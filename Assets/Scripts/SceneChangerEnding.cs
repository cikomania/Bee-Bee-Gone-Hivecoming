using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangerEnding : MonoBehaviour
{
    //ending cutscene için

    public float changeTime;
        
    void Update()
    {
        changeTime -= Time.deltaTime;
        if (changeTime <= 0)
        {
            SceneManager.LoadScene("Stats");
        } 
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Stats");
        }
    }
}
