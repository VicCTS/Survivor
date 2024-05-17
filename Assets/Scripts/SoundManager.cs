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
    public AudioClip bgmMenuSound;
    public AudioClip bgmGameSound;
    public AudioClip sonidoDisparo;
    public AudioClip AtaqueEnemgio;
    public AudioClip golpeArbol;
    public AudioClip muertePersonaje;
    public AudioClip muerteEnemigo;
    public AudioClip muerteArbol;
    public AudioClip winBGM;
    public AudioClip GameOverBGM;

    
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

        DontDestroyOnLoad(gameObject);
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
