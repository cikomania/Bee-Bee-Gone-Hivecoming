using System.Collections;
using TMPro;
using UnityEngine;

public class PressSpaceToShootText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI pressSpaceText;
    void Start()
    {
        PressSpaceText();
    }

    void PressSpaceText()
    {
        pressSpaceText.text = "Press Space to shoot!";
        StartCoroutine(ShowTextForDuration());
    }

    IEnumerator ShowTextForDuration()
    {
        yield return new WaitForSeconds(6f);
        pressSpaceText.gameObject.SetActive(false);
    }
}
