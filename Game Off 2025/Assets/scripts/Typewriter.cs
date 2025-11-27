using UnityEngine;
using System.Collections;
using TMPro;
public class Typewriter : MonoBehaviour
{
    public GameObject Ui;
    private bool UiShown;

    public TextToDecipher TextToDecipher;
    public bool CanUse = false;
    private void OnMouseOver()
    {
        if (CanUse)
        {
            transform.localScale = new Vector2(1.1f, 1.1f);

            if (Input.GetMouseButtonDown(0))
            {
                StartCoroutine(ClickDelay());
            }
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

        int amountRight = 0;

        for (int i = 0; i < InputTxt.Length; i++)
        {
            if (InputTxt[i] == TextToDecipher.CurString[i])
            {
                amountRight++;
            }
        }

        if (amountRight / InputTxt.Length >= 0.8f)
        {
            TextToDecipher.AmountGot++;
        }

        if (InputTxt != "")
        {
            TextToDecipher.Reset();
        }
    }
}
