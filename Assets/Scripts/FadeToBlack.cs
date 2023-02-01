using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FadeToBlack : MonoBehaviour
{
    public Image getImage;
    private TMP_Text getText;

    public float duration = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FadeInImage(float red, float blue, float green)
    {
        getImage = GetComponent<Image>();
        StartCoroutine(FadeImage(0f, 1f, duration, red, blue, green, getImage));
    }

    public void FadeOutImage(float red, float blue, float green)
    {
        getImage = GetComponent<Image>();
        StartCoroutine(FadeImage(1f, 0f, duration, red, blue, green, getImage));
    }

    IEnumerator FadeImage(float start, float end, float duration, float red, float blue, float green, Image image)
    {
        float imageTime = 0.0f;

        while (imageTime < duration)
        {
            float t = imageTime / duration;
            image.color = new Color(red, blue, green, Mathf.Lerp(start, end, t));
            imageTime += Time.deltaTime;
            yield return null;
        }
        image.color = new Color(red, blue, green, end);
    }

    public void FadeInText(float red, float blue, float green)
    {
        getText = GetComponent<TMP_Text>();
        StartCoroutine(FadeText(0f, 1f, duration, red, blue, green, getText));
    }

    public void FadeOutText(float red, float blue, float green)
    {
        getText = GetComponent<TMP_Text>();
        StartCoroutine(FadeText(1f, 0f, duration, red, blue, green, getText));
    }

    IEnumerator FadeText(float start, float end, float duration, float red, float blue, float green, TMP_Text text)
    {
        float textTime = 0.0f;

        while (textTime < duration)
        {
            float t = textTime / duration;
            text.color = new Color(red, blue, green, Mathf.Lerp(start, end, t));
            textTime += Time.deltaTime;
            yield return null;
        }
        text.color = new Color(red, blue, green, end);
    }
}
