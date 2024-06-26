using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class LiftController : MonoBehaviour
{

    public GameObject settingsPanel; // Ссылка на панель настроек в меню
    private bool generatorActivated = false;

    void OnTriggerEnter(Collider other)
    {
        if (generatorActivated && other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            LoadNextLevel();
        }
    }

    // Метод для активации лифта
    public void ActivateLift()
    {
        generatorActivated = true;
        Debug.Log("Лифт активирован и готов к использованию!");
    }

    // Метод для загрузки следующей сцены
    private void LoadNextLevel()
    {
        Debug.Log("Загрузка EndGame...");
        UnlockCursor();
        SceneManager.LoadScene("EndGame");
    }
   
    // Метод для разблокировки и отображения курсора
    private void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
