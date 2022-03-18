using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BlinkingImage : MonoBehaviour
{
    Image image;

    public bool blinking = false;
    public bool wait = false;
    public float waitTime = 0.5f;

    void Start()
    {
        image = GetComponent<Image>();
    }

    void Update()
    {
        if (blinking  && !wait)
        {
            StartCoroutine(Blink());
        }
    }

    public void enableBlinking ()
    {
        blinking = true;
    }

    IEnumerator Blink()
    {
        wait = true;
        switch (image.color.a.ToString())
        {
            case "0":
                image.color = new Color(image.color.r, image.color.g, image.color.b, 1);
                //Play sound
                yield return new WaitForSeconds(waitTime);
                break;
            case "1":
                image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
                //Play sound
                yield return new WaitForSeconds(waitTime);
                break;
        }
        wait = false;
    }
}
