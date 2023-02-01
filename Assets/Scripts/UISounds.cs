using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISounds : MonoBehaviour
{

    AudioSource UIAudio;
    public AudioClip selectBeep;
    float beepVolume = 0.1f;
    public AudioClip chaChing;
    float chaChingVolume = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        UIAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playBeep()
    {
        UIAudio.PlayOneShot(selectBeep, beepVolume);
    }

    public void playChaChing()
    {
        UIAudio.PlayOneShot(chaChing, chaChingVolume);
    }
}
