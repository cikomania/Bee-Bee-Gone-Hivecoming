using TMPro;
using UnityEngine;

public class FlowerCounter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI flowerCounterText;
    int totalFlowers;
    int remainingFlowers;

    LevelController levelController;

    void Start()
    {
        levelController = FindObjectOfType<LevelController>();
        totalFlowers = levelController.totalFlowers;
        remainingFlowers = totalFlowers;
        UpdateFlowerCounter();
    }

    public void FlowerCollected()
    {
        remainingFlowers--;
        UpdateFlowerCounter();
    }

    void UpdateFlowerCounter()
    {
        flowerCounterText.text = remainingFlowers.ToString();
    }
}
