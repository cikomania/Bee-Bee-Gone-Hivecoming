using System.Collections;
using TMPro;
using UnityEngine;

public class AddTimeToScoreText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI addedTimeText;

    void Start()
    {
        int addedScore = PlayerPrefs.GetInt("AddedScore", 0);
        if (addedScore > 0)
        {
            ShowAddedText(addedScore);
        }
    }
    public void ShowAddedText(int seconds)
    {
        if (!addedTimeText.gameObject.activeSelf)
        {
            addedTimeText.gameObject.SetActive(true);
        }

        addedTimeText.text = "+ " + seconds.ToString();
        StartCoroutine(AddedTimeTextCoroutine());
    }

    IEnumerator AddedTimeTextCoroutine()
    {
        yield return new WaitForSeconds(1.5f);
        addedTimeText.gameObject.SetActive(false);
    }
}
