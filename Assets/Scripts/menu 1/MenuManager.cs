using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject settingsPanel;

    public void PlayGame() 
    {
        Application.LoadLevel("Game");
    }

    public void ExitGame() 
    {
        Application.Quit();
    }

    public void Exit() 
    {
        settingsPanel.SetActive(false);
    }
}
