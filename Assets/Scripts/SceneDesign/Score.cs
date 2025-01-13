using TMPro;
using UnityEngine;

public class Score : MonoBehaviour //LevelController içerisinde kullandým
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

    public static void AddScore(int remainingSeconds) //kalan saniyeyi puana dönüþtürmek için (1sn = 1puan)
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
        score = 0; //yalnýzca o anki levelin puaný sýfýrlanýr
    }
}
