using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance {get; private set;}

    [SerializeField]AudioSource bgmSource;
    [SerializeField]AudioSource sfxSource;
    public AudioClip buttonSound;
    public AudioClip playSound;
    public AudioClip bgmSound;

    
    // Start is called before the first frame update
    void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance =this;
        }
    }

    public void PlaySound(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    public void PlayBGM(AudioClip clip)
    {
        bgmSource.clip = clip;
        bgmSource.Play();
    }

    public void StopBGM()
    {
        bgmSource.Stop();
    }

    //SoundManager.instance.PlaySound(SoundManager.instance.gameOver);
    //SoundManager.instance.PlaySound(SoundManager.instance.jump);

}
