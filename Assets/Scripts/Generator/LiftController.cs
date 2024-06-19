using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LiftController : MonoBehaviour
{
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
        Debug.Log("Загрузка следующего уровня...");
        SceneManager.LoadScene("Game");
    }
}
