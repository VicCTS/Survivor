using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class winpause : MonoBehaviour
{

    [SerializeField] private GameObject habilidades;
    [SerializeField] private GameObject victoria;

    private Animator _animator;

    void Start()
    {
      //  _animator = GetComponentInChildren<Animator>();
    }

    public void Salud()
    {

        habilidades.SetActive(false);
        victoria.SetActive(true);

     //   _animator.SetBool("MenuPause", true);
    }

    public void correr()
    {

        habilidades.SetActive(false);
        victoria.SetActive(true);
        
    //    _animator.SetBool("MenuPause", false);
    }

    public void fuerza()
    {
        habilidades.SetActive(false);
        victoria.SetActive(true);
    }

    public void velocidad()
    {
        habilidades.SetActive(false);
        victoria.SetActive(true); 
    }
}
