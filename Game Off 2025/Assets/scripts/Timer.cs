using UnityEngine;
using System.Collections;
public class Timer : MonoBehaviour
{
    public float Time;
    private float TimeAt;
    public GameObject TimerObj;
    public GameObject TimerBgObj;
    public bool TimeUp;


    public void StartTimer()
    {
        StartCoroutine(TimeGoDown());
        TimerBgObj.SetActive(true);
    }

    public void StopTimer()
    {
        StopAllCoroutines();
        TimerBgObj.SetActive(false);
    }

    IEnumerator TimeGoDown()
    {
        TimeAt = 1;
        while(TimeAt > 0)
        {
            yield return new WaitForSeconds(Time/1000);
            TimeAt -= 0.001f;
            TimerObj.transform.localScale = new Vector2(TimeAt, 1);
        }

        if(TimeAt < 0)
        {
            TimeUp = true;
        }
    }
}
