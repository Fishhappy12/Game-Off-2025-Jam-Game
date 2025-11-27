using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using JetBrains.Annotations;
public class FinalRadio : MonoBehaviour
{
    public Sprite LightOn;
    public bool On;
    private bool Over;
    private bool clicked;
    public RandomizeSymbols RandomSymbols;
    public List<char> Alphabet = new List<char>();

    public GameObject character;
    public GameObject BlackOutScreen;
    public GameObject text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Alphabet.AddRange("abcdefghijklmnopqrstuvwxyz".ToCharArray());
        RandomSymbols.Randomize();
        Invoke("TurnOnLight", 5f);
        StartCoroutine(Fade(true, BlackOutScreen.GetComponent<SpriteRenderer>(), false));
    }

    void TurnOnLight()
    {
        GetComponent<SpriteRenderer>().sprite = LightOn;
        On = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Over)
        {
            if (Input.GetMouseButtonDown(0))
            {
                clicked = true;
            }
        }

        if (clicked)
        {
            if (Input.inputString != "") PrintCharacter(Input.inputString);
        }
    }

    void PrintCharacter(string c)
    {
        character.GetComponent<SpriteRenderer>().sprite = DecodeCharacter(c);
    }

    public Sprite DecodeCharacter(string c)
    {
        for (int i = 0; i < Alphabet.Count; i++)
        {
            if (Alphabet[i] == c.ToLower().ToCharArray()[0])
            {
                return RandomSymbols.Output[i];
            }
        }
        return null;
    }

    private void OnMouseEnter()
    {
        if (On && !clicked)
        {
            transform.localScale = new Vector3(1.1f, 1.1f, 1);
            Over = true;
        }
    }

    private void OnMouseExit()
    {
        if (!clicked)
        {
            transform.localScale = new Vector3(1f, 1f, 1);
            Over = false;
        }
    }

    public void DoneButton()
    {
        StartCoroutine(Fade(false, BlackOutScreen.GetComponent<SpriteRenderer>(), true));
    }

    public void ShowText()
    {
        StartCoroutine(Fade(false, text.GetComponent<SpriteRenderer>(), false));
    }

    IEnumerator Fade(bool DoesFade, SpriteRenderer ObjRend, bool Trueness)
    {
        Color NewColor = ObjRend.color;
        float trans = ObjRend.color.a;
        float change;

        if (DoesFade) change = 0.01f;
        else change = -0.01f;

        for (int i = 0; i < 100; i++)
        {
            trans -= change;
            NewColor = new Color(ObjRend.color.r, ObjRend.color.g, ObjRend.color.b, trans);
            ObjRend.color = NewColor;
            yield return new WaitForSeconds(0.005f);
        }

        if (Trueness)
        {
            ShowText();
        }
    }
}
