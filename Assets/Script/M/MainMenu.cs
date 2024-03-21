using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void BtnPlay()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void BtnQuit()
    {
        Application.Quit();
    }
}