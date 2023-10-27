using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundManager : MonoBehaviour
{
    public static SoundManager instance {get; private set;}

    AudioSource source;
    public AudioClip gameOver;
    public AudioClip shoot;
    public AudioClip jump;
    // Start is called before the first frame update
    void Awake()
    {
        source = GetComponent<AudioSource>();
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void PlaySound(AudioClip clip)
    {
        source.PlayOneShot(clip);
    }

    //SoundManager.instance.PlaySound(SoundManager.instance.gameOver);
    //SoundManager.instance.PlaySound(SoundManager.instance.jump);
}