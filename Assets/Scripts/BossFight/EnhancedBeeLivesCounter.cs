using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnhancedBeeLivesCounter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI livesCounterText;
    [SerializeField] GameObject enhancedBeeObject;

    BeeController beeController;

    void Start()
    {
        beeController = enhancedBeeObject.GetComponent<BeeController>();
        UpdateLivesCounter();
    }

    void Update()
    {
        if (beeController != null)
        {
            UpdateLivesCounter();
        }
    }

    void UpdateLivesCounter()
    {
        int displayedLives = Mathf.Max(0, beeController.lives);
        livesCounterText.text = displayedLives.ToString();
    }
}
