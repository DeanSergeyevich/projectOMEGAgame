using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public SaveLoadManager saveLoadManager; // Ссылка на компонент управления сохранением и загрузкой

    void Start()
    {
        // Пример загрузки игры при старте
        saveLoadManager.LoadGame();
    }

    void Update()
    {
        // Вручную сохранить игру при нажатии клавиши K
        if (Input.GetKeyDown(KeyCode.K))
        {
            saveLoadManager.SaveGame();
        }

        // Вручную загрузить игру при нажатии клавиши L
        if (Input.GetKeyDown(KeyCode.L))
        {
            saveLoadManager.LoadGame();
        }
    }
}