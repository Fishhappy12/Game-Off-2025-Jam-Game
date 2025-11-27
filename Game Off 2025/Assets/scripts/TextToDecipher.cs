using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Collections;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class TextToDecipher : MonoBehaviour
{
    public Stages Stage;
    public TextStages TextStages;

    public int AmountGot;

    [TextArea] public List<string> possibleTexts;
    public RandomizeSymbols RandomSymbols;

    public string CurString;
    private List<char> Alphabet = new List<char>();

    public List<Sprite> OutputSymbols = new List<Sprite>();

    public int CurSprite;
    public SpriteRenderer Renderer;

    public Sprite[] radioSprites;
    public GameObject Radio;

    public bool HasMsg;

    public Initiation initiation;

    public GameObject BrokenSymbol;

    public Timer Timer;

    private void Start()
    {
        Alphabet.AddRange("abcdefghijklmnopqrstuvwxyz".ToCharArray());
        CurSprite = 0;
        StartCoroutine(GiveMsg());
    }

    private void Update()
    {
        if (initiation.radioOn)
        {
            if (HasMsg)
            {
                Radio.GetComponent<SpriteRenderer>().sprite = radioSprites[1];
            }
            else
            {
                Radio.GetComponent<SpriteRenderer>().sprite = radioSprites[0];
            }
        }

        if (Timer.TimeUp)
        {
            TimeUp();
        }
    }

    public void TimeUp()
    {
        Reset();
    }

    public void Reset()
    {
        HasMsg = false;
        CurString = null;
        CurSprite = 0;
        Renderer.sprite = OutputSymbols[CurSprite];
        OutputSymbols.RemoveRange(1, OutputSymbols.Count - 1);
        BrokenSymbol.SetActive(false);
        Timer.StopTimer();
    }

    public void LoadStageList()
    {
        possibleTexts.Clear();

        if(AmountGot<3)
        {
            possibleTexts.AddRange(TextStages.Stage1);
        }
        else if (AmountGot >= 3 && AmountGot < 6)
        {
            possibleTexts.AddRange(TextStages.Stage2);
        }
        else if (AmountGot >= 6 && AmountGot < 9)
        {
            possibleTexts.AddRange(TextStages.Stage3);
        }
        else if (AmountGot < 9)
        {
            possibleTexts.AddRange(TextStages.Stage4);
        }
    }

    public void RandString()
    {
        if (AmountGot! < 9)
        {
            LoadStageList();
            RandomSymbols.Randomize();
            if (OutputSymbols.Count > 1)
            {
                OutputSymbols.RemoveRange(1, OutputSymbols.Count - 1);
            }
            CurString = possibleTexts[Random.Range(0, possibleTexts.Count)].ToLower();
            possibleTexts.Remove(CurString);

            foreach (char letter in CurString)
            {
                if (OutputSymbols[OutputSymbols.Count - 1] == null || Random.Range(0, 10) != 7)
                {
                    for (int i = 0; i < Alphabet.Count; i++)
                    {
                        if (Alphabet[i] == letter)
                        {
                            OutputSymbols.Add(RandomSymbols.Output[i]);
                        }
                    }
                }
                else
                {
                    OutputSymbols.Add(null);
                }
            }

            CurSprite = 0;
            HasMsg = true;
            Timer.StartTimer();
        }
        else
        {
            SceneManager.LoadScene(2);
        }
    }

    public void OnMouseOver()
    {
        if (initiation.radioOn)
        {
            transform.localScale = new Vector2(1.1f, 1.1f);

            if (HasMsg)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (CurSprite < OutputSymbols.Count - 1) CurSprite++;
                    else
                    {
                        CurSprite = 0;
                    }

                    if (OutputSymbols[CurSprite] == null && CurSprite != 0)
                    {
                        BrokenSymbol.SetActive(true);
                        Renderer.sprite = null;
                    }
                    else
                    {
                        BrokenSymbol.SetActive(false);
                        Renderer.sprite = OutputSymbols[CurSprite];
                    }
                }
            }
        }
    }

    public void OnMouseExit()
    {
        transform.localScale = new Vector2(1f, 1f);
    }

    IEnumerator GiveMsg()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(5, 10));

            if (!HasMsg && initiation.radioOn)
            {
                RandString();
            }
        }
    }
}
