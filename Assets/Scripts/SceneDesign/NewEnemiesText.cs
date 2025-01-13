using System.Collections;
using TMPro;
using UnityEngine;

public class NewEnemiesText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI newEnemiesText;
    void Start()
    {
        newEnemiesText.text = "CAREFUL! New Enemies Ahead";
        StartCoroutine(NewEnemiesTextCoroutine());
    }

    IEnumerator NewEnemiesTextCoroutine()
    {
        yield return new WaitForSeconds(1.5f);
        newEnemiesText.gameObject.SetActive(false);
    }
}
