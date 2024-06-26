using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pause_menu : MonoBehaviour
{
    public static bool GameIsPause = false; // Флаг для отслеживания состояния паузы
    public GameObject pauseMenuUI; // Ссылка на UI паузы
    public GameObject gameUI; // Ссылка на основной игровой UI

    void Start()
    {
        // Начальная настройка
        Resume();
    }

    // Обновление вызывается один раз за кадр
    void Update()
    {
        // Проверка нажатия клавиши Esc
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Если игра на паузе, возобновляем игру, иначе приостанавливаем
            if (GameIsPause)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    // Метод для возобновления игры
    void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked; // Блокировка курсора
        pauseMenuUI.SetActive(false); // Скрытие меню паузы
        gameUI.SetActive(true); // Отображение основного UI
        Time.timeScale = 1f; // Установка нормального времени
        GameIsPause = false; // Снятие флага паузы
    }

    // Метод для приостановки игры
    void Pause()
    {
        Cursor.lockState = CursorLockMode.None; // Разблокировка курсора
        Cursor.visible = true; // Отображение курсора
        pauseMenuUI.SetActive(true); // Отображение меню паузы
        gameUI.SetActive(false); // Скрытие основного UI
        Time.timeScale = 0f; // Остановка времени
        GameIsPause = true; // Установка флага паузы
    }

    // Метод для выхода из игры
    public void QuitGame()
    {

        SceneManager.LoadScene("Menu");
        Debug.Log("quit"); // Вывод сообщения в консоль
    }
}
