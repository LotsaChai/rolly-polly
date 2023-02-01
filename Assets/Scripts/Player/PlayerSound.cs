using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    public AudioSource playerAudio;
    public AudioClip playerRollSound;
    public float rollSoundVolume = 1.0f;
    public AudioClip playerShootSound;
    public float shootSoundVolume = 1.0f;
    public AudioClip deathSound;
    public float deathSoundVolume = 1.0f;
    public AudioClip collisionSound;
    public float collisionSoundVolume = 1.0f;

    public GameObject player;
    private PlayerController playerControllerScript;

    private float pitch;

    // Start is called before the first frame update
    void Start()
    {
        playerAudio = GetComponent<AudioSource>();
        playerControllerScript = player.GetComponent<PlayerController>();
        CheckPitch();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CheckPitch()
    {
        pitch = 0.5f / playerControllerScript.moveSpeed;
        playerAudio.pitch = pitch;
    }

    public void playRollSound()
    {
        CheckPitch();
        playerAudio.PlayOneShot(playerRollSound, rollSoundVolume);
    }

    public void playShootSound()
    {
        CheckPitch();
        playerAudio.PlayOneShot(playerShootSound, shootSoundVolume);
    }

    public void playDeathSound()
    {
        CheckPitch();
        playerAudio.PlayOneShot(deathSound, deathSoundVolume);
    }

    public void playCollisionSound()
    {
        CheckPitch();
        playerAudio.PlayOneShot(collisionSound, collisionSoundVolume);
    }
}
