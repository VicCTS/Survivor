using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] private float waitTime;
    [SerializeField] private string nextScene;

    void Start()
    {
        StartCoroutine("NextScene");
    }

    public IEnumerator NextScene()
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(nextScene);
    }
}