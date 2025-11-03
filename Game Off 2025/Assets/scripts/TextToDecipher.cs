using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TextToDecipher : MonoBehaviour
{
    [TextArea] public List<string> possibleTexts;
    public RandomizeSymbols RandomSymbols;

    public string CurString;
    private List<char> Alphabet = new List<char>();

    public List<Sprite> OutputSymbols = new List<Sprite>();

    public int CurSprite;
    public SpriteRenderer Renderer;
    private void Start()
    {
        Alphabet.AddRange("abcdefghijklmnopqrstuvwxyz".ToCharArray());
        CurSprite = 0;
    }

    public void RandString()
    {
        RandomSymbols.Randomize();
        if (OutputSymbols.Count > 1)
        {
            OutputSymbols.RemoveRange(1, OutputSymbols.Count-1);
        }
        CurString = possibleTexts[Random.Range(0, possibleTexts.Count)];

        foreach(char letter in CurString)
        {
            for(int i = 0; i < Alphabet.Count; i++)
            {
                if(Alphabet[i] == letter)
                {
                    OutputSymbols.Add(RandomSymbols.Output[i]);
                }
            }
        }

        CurSprite = 0;
    }

    public void OnMouseOver()
    {
        transform.localScale = new Vector2(1.1f, 1.1f);

        if(Input.GetMouseButtonDown(0))
        {
            if(CurSprite < OutputSymbols.Count-1) CurSprite++;
            else
            {
                CurSprite = 0;
            }

            Renderer.sprite = OutputSymbols[CurSprite];
        }
    }

    public void OnMouseExit()
    {
        transform.localScale = new Vector2(1f, 1f);
    }
}
