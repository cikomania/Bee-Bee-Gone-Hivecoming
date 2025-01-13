using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class PauseMenu : MonoBehaviour
{
    bool gamePaused = false;
    [SerializeField] GameObject pausePanel;

    AudioManager audioManager;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!gamePaused)
            {
                PauseGame();
            }
            else
            {
                Resume();
            }
        }
    }

    void PauseGame()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0;
        gamePaused = true;
        
        if (audioManager != null && audioManager.moveSource.isPlaying)
        {
            audioManager.moveSource.Stop(); //Pause() çalýþmadý
        }
    }

    public void Resume()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
        gamePaused = false;    
        
        //if (audioManager !=null && !audioManager.moveSource.isPlaying)
        //{
        //    audioManager.moveSource.UnPause();
        //}
    }

    public void Restart()
    {        
        Score.ResetCurrentLevelScore();
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    public void MainMenu()
    {
        SceneManager.LoadSceneAsync(1);
        Time.timeScale = 1;
    }
}
