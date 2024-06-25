using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    // Чувствительность мыши
    public float mouseSensitivity;
    // Скорость движения
    public float speed;
    // Высота прыжка
    public float jumpHeight;
    // Скорость бега
    public float runSpeed;
    // Текущая выносливость игрока
    public float playerStamina;
    // Максимальная выносливость игрока
    public float maxStamina;
    // Текущий уровень заряда батареи
    public float currentBattery;
    // Текущий уровень здоровья
    public float health;
    // Текущий уровень голода
    public float playerHunger;
    // Позиция игрока
    public Vector3 playerPosition;
    // Поворот игрока
    public Vector3 playerRotation;
    // Позиция камеры
    public Vector3 cameraPosition;
    public Vector3 cameraRotation;
    public Vector3 playerViewDirection;

    // Данные врага
    public Vector3 enemyPosition;
    public Vector3 enemyRotation;

    public GeneratorData[] generators; // Массив данных о генераторах
    public bool[] canisters; // Массив данных о состоянии канистр (взята или нет)
}

// Класс для хранения данных о генераторах
[System.Serializable]
public class GeneratorData
{
    public int insertedCanistersCount; // Количество вставленных канистр
}
