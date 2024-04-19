using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsSaver : MonoBehaviour
{
    public static StatsSaver instance;

    void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        LoadStats();
    }

    // Start is called before the first frame update
    void Start()
    {
        //LoadStats();
    }

    public void LoadStats()
    {
        Global.level = PlayerPrefs.GetInt("level", 3);
        Global.maxScore = PlayerPrefs.GetInt("maxScore", 0);
        Global.playerMaxHealth = PlayerPrefs.GetInt("maxHealth", 50);
        Global.playerSpeed = PlayerPrefs.GetFloat("movementSpeed", 10);
        Global.playerDamage = PlayerPrefs.GetInt("damage", 5);
        Global.fireRate = PlayerPrefs.GetFloat("fireRate", 2);
        Global.fireRateTimer = PlayerPrefs.GetFloat("fireRateTimer", 0);
    }

    public void SaveStats()
    {
        PlayerPrefs.SetInt("level", Global.level);
        PlayerPrefs.SetInt("maxScore", Global.maxScore);
        PlayerPrefs.SetInt("maxHealth", Global.playerMaxHealth);
        PlayerPrefs.SetFloat("movementSpeed", Global.playerSpeed);
        PlayerPrefs.SetInt("damage", Global.playerDamage);
        PlayerPrefs.SetFloat("fireRate", Global.fireRate);
        PlayerPrefs.SetFloat("fireRateTimer", Global.fireRateTimer);
    }    
}
