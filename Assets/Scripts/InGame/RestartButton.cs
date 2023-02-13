using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class RestartButton : MonoBehaviour
{
    private Image restartButtonImage;
    private FadeToBlack fadeScript;

    public TMP_Text restartText;
    private FadeToBlack textScript;

    // Start is called before the first frame update
    void Start()
    {
        restartButtonImage = GetComponent<Image>();
        fadeScript = GetComponent<FadeToBlack>();
        textScript = restartText.GetComponent<FadeToBlack>();

        restartButtonImage.gameObject.SetActive(false);
        restartButtonImage.color = new Color(255, 255, 255, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("My Game");
    }

    public void GameOver()
    {
        restartButtonImage.gameObject.SetActive(true);
        fadeScript.FadeInImage(255, 255, 255);
        textScript.FadeInText(0, 0, 0);
    }
}
