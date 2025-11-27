using UnityEngine;
using System.Collections.Generic;
public class RandomizeSymbols : MonoBehaviour
{
    public List<Sprite> Output = new List<Sprite>();
    public List<Sprite> Input = new List<Sprite>();
    private List<Sprite> NotUsed = new List<Sprite>();

    public paper Paper;

    public string path;
    public void Start()
    {
        Input.AddRange(Resources.LoadAll<Sprite>(path));
    }

    public List<Sprite> Randomize()
    {
        NotUsed.Clear();
        Output.Clear();
        NotUsed.AddRange(Input);
        for (int i = 0; i < Input.Count; i++)
        {
            Sprite go = NotUsed[Random.Range(0, NotUsed.Count)];
            Output.Add(go);
            NotUsed.Remove(go);
        }

        if(Paper != null) Paper.ResetPaper();

        return Output;
    }
}
