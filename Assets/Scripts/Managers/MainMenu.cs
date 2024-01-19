using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject _book;
    [SerializeField] RectTransform _bookOptTransform;
    [SerializeField] RectTransform _bookPlayTransform;
    [SerializeField] float _initialPosition;
    [SerializeField] float _finalPosition; 
    [SerializeField] float _bookSpeed = 5f;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Update is called once per frame
    public void QuitGame()
    {
       Application.Quit(); 
    }

    public void GoToSeetingsMenu()
    {
        SceneManager.LoadScene("OptionsMenu");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void GoToGameMenu()
    {
        SceneManager.LoadScene("GameMenu");
    }

    public void OpenOptions()
    {
        StartCoroutine("BookOpt");
    }

    public void OpenMenu()
    {
        StartCoroutine("ReturnMenuOpt");
    }

    IEnumerator BookOpt()
    {
        
        while (_bookOptTransform.position.y > _finalPosition)
        {
            _bookOptTransform.localPosition -= new Vector3(0, 1 * _bookSpeed, 0);
            yield return new WaitForEndOfFrame();
        }

    }

    IEnumerator ReturnMenuOpt()
    {
        while (_bookOptTransform.position.y < _initialPosition)
        {
            _bookOptTransform.localPosition += new Vector3(0, 1 * _bookSpeed, 0);
            yield return new WaitForEndOfFrame();
        }
    }

    public void OpenPlay()
    {
        StartCoroutine("BookPlay");
    }

    public void OpenPlayMenu()
    {
        StartCoroutine("ReturnMenuPlay");
    }

    IEnumerator BookPlay()
    {
        
        while (_bookPlayTransform.position.y > _finalPosition)
        {
            _bookPlayTransform.localPosition -= new Vector3(0, 1 * _bookSpeed, 0);
            yield return new WaitForEndOfFrame();
        }

    }

    IEnumerator ReturnMenuPlay()
    {
        while (_bookPlayTransform.position.y < _initialPosition)
        {
            _bookPlayTransform.localPosition += new Vector3(0, 1 * _bookSpeed, 0);
            yield return new WaitForEndOfFrame();
        }
    }
}
