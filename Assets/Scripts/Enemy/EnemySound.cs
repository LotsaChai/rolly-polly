using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySound : MonoBehaviour
{
    public AudioSource enemySound;
    public AudioClip deathCoin;
    public float deathCoinVolume = 0.2f;
    public AudioClip hitSound;
    public float hitSoundVolume = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        enemySound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playHitSound()
    {
        enemySound.PlayOneShot(hitSound, hitSoundVolume);
    }

    public void playDeathCoin()
    {
        enemySound.PlayOneShot(deathCoin, deathCoinVolume);
    }
}
