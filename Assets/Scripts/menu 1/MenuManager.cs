using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject settingsPanel; // Ссылка на панель настроек в меню
    

    // Метод для начала игры
    public void PlayGame() 
    {
        // Загрузка сцены "Game"
        SceneManager.LoadScene("Game");
    }

    // Метод для загрузки обучения
    public void loadEducation()
    {
        // Загрузка сцены "EducationScene"
        SceneManager.LoadScene("EducationScene");
    }

    // Метод для выхода из игры
    public void ExitGame() 
    {
        // Завершение приложения
        Application.Quit();

        Debug.Log("Выход из игры");
    }

    

    // Метод для закрытия панели настроек
    public void Exit() 
    {
        // Деактивация панели настроек
        settingsPanel.SetActive(false);
    }

    public void ExitInMenu()
    {
        // выход в меню
        SceneManager.LoadScene("Menu");
        Debug.Log("Выход в меню");
    }

    

 

}
