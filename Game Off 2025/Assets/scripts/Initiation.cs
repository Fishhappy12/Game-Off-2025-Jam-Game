using UnityEngine;

public class Initiation : MonoBehaviour
{
    public bool AllReady;
    public GameObject LightSwitch;

    public void SizeIncrease()
    {
        print("over");
        if (!AllReady)
        {
            LightSwitch.transform.localScale = new Vector3(1.1f, 1.1f, 1);
        }
    }

    public void SizeDecrease()
    {
        if (!AllReady)
        {
            LightSwitch.transform.localScale = new Vector3(1f, 1f, 1);
        }
    }
}
