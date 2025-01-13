using TMPro;
using UnityEngine;

public class Score : MonoBehaviour //LevelController i�erisinde kulland�m
{
    public static int score;
    [SerializeField] TextMeshProUGUI scoreText;

    void Start()
    {
        score = GetSavedScore();
    }

    void Update()
    {
        scoreText.text = score.ToString();
    }

    public static void AddScore(int remainingSeconds) //kalan saniyeyi puana d�n��t�rmek i�in (1sn = 1puan)
    {
        score += remainingSeconds;
        SaveScore();
    }

    public static void SaveScore()
    {
        PlayerPrefs.SetInt("TotalScore", score);
    }

    public static int GetSavedScore()
    {
        return PlayerPrefs.GetInt("TotalScore", 0);
    }

    public static void ResetCurrentLevelScore()
    {
        score = 0; //yaln�zca o anki levelin puan� s�f�rlan�r
    }
}
