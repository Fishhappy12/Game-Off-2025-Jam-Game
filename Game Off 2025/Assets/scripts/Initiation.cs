using JetBrains.Annotations;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Initiation : MonoBehaviour
{
    [Header("Light")]
    private LightOn LightScript;
    public GameObject LightSwitch;
    public Light2D Light;
    public bool LightOn;

    [Header("Radio")]
    private RadioOn RadioScript;
    public bool radioOn;
    public GameObject Radio;

    [Header("Typewriter")]
    private TypeWriterOn TypeWriterOn;
    public bool TOn;
    public GameObject Paper;
    public GameObject TypeWriter;
    public Sprite[] Sprites;

    private void Start()
    {
        //light
        LightScript = gameObject.AddComponent<LightOn>();
        LightScript.Initiation = this;
        LightScript.LightSwitch = LightSwitch;
        LightScript.Light = Light;

        //Radio
        RadioScript = gameObject.AddComponent<RadioOn>();
        RadioScript.Initiation = this;
        RadioScript.RadioSwitch = Radio;
        RadioScript.Light = LightScript;

        //typewriter
        TypeWriterOn = gameObject.AddComponent<TypeWriterOn>();
        TypeWriterOn.Light = LightScript;
        TypeWriterOn.Initiation = this;
        TypeWriterOn.Paper = Paper;
        TypeWriterOn.TypeWriter = TypeWriter;
        TypeWriterOn.Sprites = Sprites;

    }
    
    public void LightOver(bool BigOrSmall)
    {
        if(BigOrSmall) LightScript.SizeIncrease();
        else LightScript.SizeDecrease();
    }

    public void RadioOver(bool BigOrSmall)
    {
        if (BigOrSmall) RadioScript.SizeIncrease();
        else RadioScript.SizeDecrease();
    }

    public void PaperOver(bool BigOrSmall)
    {
        if( BigOrSmall) TypeWriterOn.OnOver();
        else TypeWriterOn.NotOnOver();
    }
}

public class LightOn : MonoBehaviour
{
    public Initiation Initiation;
    public bool AllReady;
    public GameObject LightSwitch;
    public Light2D Light;
    private bool Over;
    public void SizeIncrease()
    {
        if (!AllReady)
        {
            LightSwitch.transform.localScale = new Vector3(1.1f, 1.1f, 1);
            Over = true;
        }
    }
    public void SizeDecrease()
    {
        if (!AllReady)
        {
            LightSwitch.transform.localScale = new Vector3(1f, 1f, 1);
            Over = false;
        }
    }
    public void Update()
    {
        if (Over || AllReady)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Light.intensity = 1f;
                AllReady = true;
                Initiation.LightOn = true;
                LightSwitch.transform.localScale = new Vector3(1f, 1f, 1);
            }
        }
    }
}

public class RadioOn : MonoBehaviour
{
    public LightOn Light;
    public Initiation Initiation;
    public bool AllReady;
    public GameObject RadioSwitch;

    public bool Over;

    public void SizeIncrease()
    {
        if (!AllReady && Light.AllReady)
        {
            RadioSwitch.transform.localScale = new Vector3(1.1f, 1.1f, 1);
            Over = true;
        }
    }
    public void SizeDecrease()
    {
        if (!AllReady && Light.AllReady)
        {
            RadioSwitch.transform.localScale = new Vector3(1f, 1f, 1);
            Over = false;
        }
    }

    public void Update()
    {
        if (Over || AllReady)
        {
            if (Input.GetMouseButtonDown(0))
            {
                AllReady = true;
                Initiation.radioOn = true;
                RadioSwitch.transform.localScale = new Vector3(1f, 1f, 1);
            }
        }
    }
}

public class TypeWriterOn : MonoBehaviour
{
    public Initiation Initiation;
    public GameObject Paper;
    public GameObject TypeWriter;
    public Sprite[] Sprites = new Sprite[3];
    private int SpriteNum;
    private bool IsTouching;
    private bool PickedUp;
    private bool CanUp;

    public LightOn Light;

    private void Update()
    {
        TypeWriter.GetComponent<SpriteRenderer>().sprite = Sprites[SpriteNum];

        if (!Initiation.TOn)
        {
            if (Paper != null && TypeWriter.GetComponent<Collider2D>().IsTouching(Paper.GetComponent<Collider2D>()))
            {
                IsTouching = true;
            }

            if (IsTouching)
            {
                if (Input.GetMouseButtonUp(0))
                {
                    Destroy(Paper);
                    TypeWriter.GetComponent<Typewriter>().CanUse = true;
                    Initiation.TOn = true;
                }
            }

            if (PickedUp && Paper != null && Input.GetMouseButton(0))
            {
                CanUp = true;
                SpriteNum = 1;
            }
            else
            {
                CanUp = false;
                SpriteNum = 0;
            }

            if (CanUp && Light.AllReady)
            {
                Paper.transform.localScale = new Vector3(1f, 1f, 1);
                Vector2 ScreenMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Paper.transform.position = ScreenMouse;
            }
        }
        else
        {
            SpriteNum = 2;
        }
    }


    public void OnOver()
    {
        if (Paper != null && Light.AllReady)
        {
            if (!PickedUp && !Initiation.TOn)
            {
                Paper.transform.localScale = new Vector3(1.1f, 1.1f, 1);
                PickedUp = true;
            }
        }
    }

    public void NotOnOver()
    {
        if (Paper != null && Light.AllReady)
        {
            Paper.transform.localScale = new Vector3(1f, 1f, 1);
            PickedUp = false;
        }
    }
}
