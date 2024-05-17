using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance {get; private set;}

    public bool _gameOver = false;

    public GameObject[] trees;
    public int totalTrees;

    public int totalEnemies;
    public int totalWaves;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        trees = GameObject.FindGameObjectsWithTag("Arbol");
        totalTrees = trees.Length;
    }

    public void TreeDestroyed()
    {
        totalTrees--;

        if(totalTrees <= 0)
        {
            GameOver();
        }
    }

    public void EnemyDestroyed()
    {
        totalEnemies--;

        if(totalEnemies <= 0)
        {
            Win();
        }
    }

    public void GameOver()
    {
        SoundManager.instance.PlayBGM(SoundManager.instance.GameOverBGM);
        MenuPause.instance.ShowGameOverCanvas();
        _gameOver = true;
    }

    public void Win()
    {
        SoundManager.instance.PlayBGM(SoundManager.instance.winBGM);
        MenuPause.instance.ShowStatsCanvas();
        _gameOver = true;
    }

}
