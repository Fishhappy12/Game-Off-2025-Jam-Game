using UnityEngine;
using System.Collections;
using TMPro;
public class Typewriter : MonoBehaviour
{
    public GameObject Ui;
    private bool UiShown;

    public TextToDecipher TextToDecipher;
    private void OnMouseOver()
    {
        transform.localScale = new Vector2(1.1f, 1.1f);

        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(ClickDelay());
        }
    }

    public void OnMouseExit()
    {
        transform.localScale = new Vector2(1f, 1f);
    }

    IEnumerator ClickDelay()
    {
        if (UiShown)
        {
            Ui.SetActive(false);
            yield return new WaitForEndOfFrame();
        }
        else 
        {
            Ui.SetActive(true);
            yield return new WaitForEndOfFrame();
        }

        UiShown = !UiShown;

        StopCoroutine(ClickDelay());
    }

    public void Submit()
    {
        string InputTxt = Ui.GetComponentInChildren<TMP_InputField>().text;
        Ui.GetComponentInChildren<TMP_InputField>().text = "";

        if (InputTxt != "")
        {
            TextToDecipher.Reset();
        }
    }
}
