using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPause : MonoBehaviour
{
    public static MenuPause instance;

    [SerializeField] private GameObject botonPausa;
    [SerializeField] private GameObject menuPausa;
    [SerializeField] private GameObject options;

    [SerializeField] private GameObject gameOverCanvas;
    [SerializeField] private GameObject winCanvas;
    [SerializeField] private GameObject statsCanvas;
    
    [SerializeField] private Text health;
    [SerializeField] private Text movementSpeed;
    [SerializeField] private Text attackSpeed;
    [SerializeField] private Text attackDamage;


    private Animator _animator;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        _animator = GetComponentInChildren<Animator>();

        health.text = Global.playerMaxHealth.ToString() + " " + "+10";
        movementSpeed.text = Global.playerSpeed.ToString()  + " " + "+1";
        attackSpeed.text = Global.fireRateTimer.ToString()  + " " + "+0.2";
        attackDamage.text = Global.playerDamage.ToString()  + " " + "+1";
    }

    public void ShowGameOverCanvas()
    {
        gameOverCanvas.SetActive(true);
    }
    public void ShowStatsCanvas()
    {
        statsCanvas.SetActive(true);
    }

    public void Pausa()
    {

        botonPausa.SetActive(false);
        menuPausa.SetActive(true);
        options.SetActive(false);
        

        //_animator.SetBool("MenuPause", true);

        Time.timeScale =  0;
    }

    public void Reaunudar()
    {

        botonPausa.SetActive(true);
        menuPausa.SetActive(false);
        options.SetActive(false);
        
        //_animator.SetBool("MenuPause", false);

        Time.timeScale =  1;
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

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void UpgradeMaxHealth()
    {
        Global.playerMaxHealth += 10;
        Global.level++;

        statsCanvas.SetActive(false);
        winCanvas.SetActive(true);

        StatsSaver.instance.SaveStats();

     //   _animator.SetBool("MenuPause", true);
    }

    public void UpgradeMovementSpeed()
    {
        Global.playerSpeed++;
        Global.level++;

        statsCanvas.SetActive(false);
        winCanvas.SetActive(true);

        StatsSaver.instance.SaveStats();
        
    //    _animator.SetBool("MenuPause", false);
    }

    public void UpgradeAttackDamage()
    {
        Global.playerDamage++;
        Global.level++;

        statsCanvas.SetActive(false);
        winCanvas.SetActive(true);

        StatsSaver.instance.SaveStats();
    }

    public void UpgradeAttackSpeed()
    {
        Global.fireRate -= 0.2f;
        Global.fireRateTimer += 0.2f;
        Global.level++;
        
        statsCanvas.SetActive(false);
        winCanvas.SetActive(true);

        StatsSaver.instance.SaveStats();
    }
}
