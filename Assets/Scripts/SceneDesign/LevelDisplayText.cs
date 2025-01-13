using System.Collections;
using TMPro;
using UnityEngine;

public class LevelDisplayText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI levelText;

    public void ShowLevelText(int level)
    {
        if (!levelText.gameObject.activeSelf)
        {
            levelText.gameObject.SetActive(true);
        }

        levelText.text = "LEVEL " + level.ToString();
        StartCoroutine(DisplayLevelTextCoroutine(level));
    }

    IEnumerator DisplayLevelTextCoroutine(int level)
    {        
        yield return new WaitForSeconds(1.5f);
        levelText.gameObject.SetActive(false);
    }
}
