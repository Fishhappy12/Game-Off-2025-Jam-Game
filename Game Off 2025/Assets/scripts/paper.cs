using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class paper : MonoBehaviour
{
    public bool Down;
    public bool CanClick = true;

    public TextToDecipher TextDeciper;
    public GameObject Rows;

    private void Update()
    {
        if(transform.position.y < -5f)
        {
            Down = true;
        }
        else
        {
            Down = false;
        }
    }

    public void ResetPaper()
    {
        for (int i = 0; i < Rows.transform.childCount; i++)
        {
            Rows.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = TextDeciper.RandomSymbols.Output[i];
        }
    }

    public void OnMouseOver()
    {
        if(CanClick)
        transform.localScale = new Vector2(1.1f, 1.1f);
        else transform.localScale = new Vector2(1f, 1f);

        if (Input.GetMouseButtonDown(0) && CanClick)
        {
            StartCoroutine(Move());
        }
    }

    public void OnMouseExit()
    {
        transform.localScale = new Vector2(1f, 1f);
    }

    IEnumerator Move()
    {
        Vector2 Startpos = transform.position;
        Vector2 Endpos;

        float speed = 2f;

        float time = 1f;
        float progress = 0f;

        CanClick = false;
        if (Down)
        {
            Endpos = new Vector2(0, 0);
        }
        else
        {
            Endpos = new Vector2(0, -7.4f);
        }


        while (progress < time)
        {
            progress += speed/100f;
            transform.position = Vector2.Lerp(Startpos, Endpos, progress);

            yield return new WaitForSeconds(0.01f);
        }
        
        CanClick = true;
    }
}
