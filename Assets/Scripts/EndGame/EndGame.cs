using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    public GameObject endgame;

    void Start()
    {
        // Разблокировка и отображение курсора при загрузке сцены конца игры
        UnlockCursor();
    }

    // Метод для выхода из игры
    public void ExitInMenu()
    {
        // выход в меню
        Debug.Log("Выход в меню");
        SceneManager.LoadScene("Menu");
        UnlockCursor();
        
    }

    // Метод для разблокировки и отображения курсора
    private void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
