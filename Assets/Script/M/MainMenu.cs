using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void BtnPlay() // click and load new scene
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void BtnQuit() // click and quit game
    {
        Application.Quit();
    }
}
