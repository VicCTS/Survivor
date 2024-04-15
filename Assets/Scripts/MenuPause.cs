using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPause : MonoBehaviour
{
    public static MenuPause instance;

    [SerializeField] private GameObject botonPausa;
    [SerializeField] private GameObject menuPausa;
    [SerializeField] private GameObject options;

    [SerializeField] private GameObject gameOverCanvas;
    [SerializeField] private GameObject winCanvas;


    private Animator _animator;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    public void ShowGameOverCanvas()
    {
        gameOverCanvas.SetActive(true);
    }
    public void ShowWinCanvas()
    {
        winCanvas.SetActive(true);
    }

    public void Pausa()
    {

        botonPausa.SetActive(false);
        menuPausa.SetActive(true);
        options.SetActive(false);
        

        _animator.SetBool("MenuPause", true);
    }

    public void Reaunudar()
    {

        botonPausa.SetActive(true);
        menuPausa.SetActive(true);
        options.SetActive(false);
        
        _animator.SetBool("MenuPause", false);
    }

    public void Back()
    {
        botonPausa.SetActive(false);
        menuPausa.SetActive(true);
        options.SetActive(false);
    }

    public void Options()
    {
        botonPausa.SetActive(false);
        menuPausa.SetActive(true);
        options.SetActive(true);   
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
