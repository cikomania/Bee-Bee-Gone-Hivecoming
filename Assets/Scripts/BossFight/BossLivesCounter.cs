using System.Collections;
using TMPro;
using UnityEngine;

public class BossLivesCounter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI bossLivesCounterText;
    int initialBossLives = 50;

    BossController bossController;

    void Start()
    {        
        bossController = FindObjectOfType<BossController>();

        if (bossController != null)
        {
            bossController.bossHealth = initialBossLives;
            UpdateBossLivesCounter();
        }
    }

    void Update()
    {
        if (bossController != null)
        {
            UpdateBossLivesCounter();
        }
    }

    void UpdateBossLivesCounter()
    {
        int displayedLives = bossController.bossHealth;
        bossLivesCounterText.text = displayedLives.ToString();
    }
}
