using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPause : MonoBehaviour
{

    [SerializeField] private GameObject botonPausa;
    [SerializeField] private GameObject menuPausa;
    [SerializeField] private GameObject options;

    private Animator _animator;

    void Start()
    {
        _animator = GetComponentInChildren<Animator>();
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
}
