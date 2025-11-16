using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Collections;
using UnityEditor.SceneManagement;

public class TextToDecipher : MonoBehaviour
{
    public Stages Stage;
    public TextStages TextStages;

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
    }

    public void Reset()
    {
        HasMsg = false;
        CurString = null;
        CurSprite = 0;
        Renderer.sprite = OutputSymbols[CurSprite];
        OutputSymbols.RemoveRange(1, OutputSymbols.Count - 1);

    }

    public void LoadStageList()
    {
        if(Stage == Stages.stage1)
        {
            possibleTexts.AddRange(TextStages.Stage1);
        }
        else if (Stage == Stages.stage2)
        {
            possibleTexts.AddRange(TextStages.Stage2);
        }
        else if (Stage == Stages.stage3)
        {
            possibleTexts.AddRange(TextStages.Stage3);
        }
        else if (Stage == Stages.stage4)
        {
            possibleTexts.AddRange(TextStages.Stage4);
        }
    }

    public void RandString()
    {
        LoadStageList();
        RandomSymbols.Randomize();
        if (OutputSymbols.Count > 1)
        {
            OutputSymbols.RemoveRange(1, OutputSymbols.Count-1);
        }
        CurString = possibleTexts[Random.Range(0, possibleTexts.Count)].ToLower();

        foreach(char letter in CurString)
        {
            for(int i = 0; i < Alphabet.Count; i++)
            {
                if(Alphabet[i] == letter)
                {
                    print(Alphabet[i] + " " + letter);
                    OutputSymbols.Add(RandomSymbols.Output[i]);
                }
            }
        }

        CurSprite = 0;
        HasMsg = true;
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

                    Renderer.sprite = OutputSymbols[CurSprite];
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
