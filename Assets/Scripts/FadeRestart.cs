using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FadeRestart : MonoBehaviour
{
    public TMP_Text gameOverText;

    public float duration = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        gameOverText.text = "GAME OVER";
        gameOverText.color = new Color(255, 0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FadeIn()
    {
        StartCoroutine(Fade(0f, 1f, duration));
    }

    public void FadeOut()
    {
        StartCoroutine(Fade(1f, 0f, duration));
    }

    IEnumerator Fade(float start, float end, float duration)
    {
        float time = 0.0f;

        while (time < duration)
        {
            float t = time / duration;
            gameOverText.color = new Color(255f, 0f, 0f, Mathf.Lerp(start, end, t));
            time += Time.deltaTime;
            yield return null;
        }
        gameOverText.color = new Color(255f, 0f, 0f, end);
    }
}
