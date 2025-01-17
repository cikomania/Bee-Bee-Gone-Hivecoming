using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    void Start()
    {
        for (int i = 5; i <= 14; i++)
        {
            int levelIndex = i;
        }
    }

    public void LoadLevel(int levelIndex)
    {
        int currentLevel = levelIndex - 4; //level 1'in sahnesi 5'ten baþladýðý için
        PlayerPrefs.SetInt("CurrentLevel", currentLevel);
        PlayerPrefs.Save();

        Score.score = 0;
        PlayerPrefs.SetInt("AddedScore", 0);
        SceneManager.LoadSceneAsync(levelIndex);
    }

    public void PlayGame()
    {     
        PlayerPrefs.SetInt("CurrentLevel", 1); 
        PlayerPrefs.Save();

        PlayerPrefs.DeleteKey("TotalScore");
        PlayerPrefs.DeleteKey("AddedScore");

        Score.score = 0;
        PlayerPrefs.SetInt("AddedScore", 0);
        SceneManager.LoadSceneAsync(5);
    }

    public void LevelSelect()
    {
        PlayerPrefs.DeleteKey("TotalScore");
        PlayerPrefs.DeleteKey("AddedScore");
        SceneManager.LoadSceneAsync(3);
    }

    public void Options()
    {
        SceneManager.LoadSceneAsync(4);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void BacktoMainMenu()
    {
        SceneManager.LoadSceneAsync(1);
    }

}
