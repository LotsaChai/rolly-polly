using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundOST : MonoBehaviour
{
    public AudioSource backgroundMusic;
    public PlayerController playerControllerScript;

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        backgroundMusic = GetComponent<AudioSource>();
        backgroundMusic.Play();
        playerControllerScript = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerControllerScript.gameOver)
        {
            backgroundMusic.Stop();
        }
    }
}
