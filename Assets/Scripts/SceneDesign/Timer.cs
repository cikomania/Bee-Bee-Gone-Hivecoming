using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour  //LevelController içerisinde kullandım
{
    [SerializeField] TextMeshProUGUI timerText;    
    [SerializeField] public float remainingTime;

    void Update()
    {
        remainingTime -= Time.deltaTime; // += ile yazılması normal timer        
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public float GetRemainingTime() 
    {
        return remainingTime;
    }
}
