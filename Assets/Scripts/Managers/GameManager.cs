using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
public bool _gameOver = false;
public static GameManager instance {get; private set;}

    public void GameOver()
    {
        _gameOver=true;
    }

}
