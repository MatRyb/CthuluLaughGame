using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

  public void PlayGame()
    {

        StartCoroutine(AudioEffect());
        
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator AudioEffect()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadSceneAsync(1);
    }
}



