using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    public CameraMovement cameraMovement; // Компонент управления движением камеры
    public Movement movement; // Компонент управления движением игрока
    public BatteryController batteryController; // Компонент управления батареей
    public HealthBar healthBar; // Компонент управления здоровьем
    public StaminaHunger staminaHunger; // Компонент управления выносливостью и голодом
    public Transform cameraTransform; // Трансформ камеры
    public Transform playerTransform; // Трансформ игрока

    public EnemyController enemyController; // Контроллер врага
    public Transform enemyTransform; // Трансформ врага

    private string savePath; // Путь к файлу сохранения

    void Start()
    {
        // Установка пути к файлу сохранения
        savePath = Path.Combine(Application.persistentDataPath, "savefile.json");

        // Если файл сохранения не существует, установить значения по умолчанию и сохранить игру
        if (!File.Exists(savePath))
        {
            SetDefaultValues();
            SaveGame();
        }
    }

    // Установка значений по умолчанию для всех компонентов
    void SetDefaultValues()
    {
        cameraMovement.mouseSensitivity = 5f;
        movement.Speed = 5f;
        movement.jumpHeight = 2f;
        movement.runSpeed = 10f;
        movement.stamina.playerStamina = 100f;
        movement.stamina.maxStamina = 100f;
        batteryController.currentBattery = 50f;
        healthBar.fill = 100f;
        staminaHunger.playerHunger = 0f;

        enemyTransform.position = new Vector3(0, 0, 0);
        enemyTransform.rotation = Quaternion.identity;
    }

    // Сохранение игры
    public void SaveGame()
    {
        // Создание объекта GameData и заполнение его текущими значениями
        GameData gameData = new GameData
        {
            mouseSensitivity = cameraMovement.mouseSensitivity,
            speed = movement.Speed,
            jumpHeight = movement.jumpHeight,
            runSpeed = movement.runSpeed,
            playerStamina = movement.stamina.playerStamina,
            maxStamina = movement.stamina.maxStamina,
            currentBattery = batteryController.currentBattery,
            health = healthBar.fill,
            playerHunger = staminaHunger.playerHunger,
            playerPosition = playerTransform.position,
            playerRotation = playerTransform.rotation.eulerAngles,
            cameraPosition = cameraTransform.position,
            cameraRotation = cameraTransform.rotation.eulerAngles,
            playerViewDirection = playerTransform.forward, // Сохраняем направление взгляда игрока
            enemyPosition = enemyTransform.position,
            enemyRotation = enemyTransform.rotation.eulerAngles,
        };

        // Преобразование данных в JSON и запись в файл
        string json = JsonUtility.ToJson(gameData);
        try
        {
            File.WriteAllText(savePath, json);
            Debug.Log("Game saved to " + savePath);
        }
        catch (IOException ex)
        {
            Debug.LogError("Failed to save game: " + ex.Message);
        }
    }

    // Загрузка игры
    public void LoadGame()
    {
        if (File.Exists(savePath))
        {
            try
            {
                // Чтение файла и преобразование JSON в объект GameData
                string json = File.ReadAllText(savePath);
                GameData gameData = JsonUtility.FromJson<GameData>(json);

                // Установка значений компонентов из загруженных данных
                cameraMovement.mouseSensitivity = gameData.mouseSensitivity;
                movement.Speed = gameData.speed;
                movement.jumpHeight = gameData.jumpHeight;
                movement.runSpeed = gameData.runSpeed;
                movement.stamina.playerStamina = gameData.playerStamina;
                movement.stamina.maxStamina = gameData.maxStamina;
                batteryController.currentBattery = gameData.currentBattery;
                healthBar.fill = gameData.health;
                staminaHunger.playerHunger = gameData.playerHunger;

                // Перемещение игрока к сохранённой позиции и восстановление направления взгляда
                StartCoroutine(MovePlayerToSavedPosition(gameData.playerPosition, gameData.playerViewDirection));

                // Установка позиции и состояния врага
                enemyTransform.position = gameData.enemyPosition;
                enemyTransform.rotation = Quaternion.Euler(gameData.enemyRotation);

                Debug.Log("Game loaded from " + savePath);
            }
            catch (IOException ex)
            {
                Debug.LogError("Failed to load game: " + ex.Message);
            }
        }
        else
        {
            Debug.LogWarning("Save file not found!");
        }
    }

    // Корутина для плавного перемещения игрока к сохранённой позиции с восстановлением направления взгляда
    private IEnumerator MovePlayerToSavedPosition(Vector3 targetPosition, Vector3 viewDirection)
    {
        // логика перемещения игрока
        float startTime = Time.time;
        float journeyLength = Vector3.Distance(playerTransform.position, targetPosition);
        float duration = 0.1f; // Продолжительность перемещения

        while (Time.time - startTime < duration)
        {
            float fraction = (Time.time - startTime) / duration;
            playerTransform.position = Vector3.Lerp(playerTransform.position, targetPosition, fraction);
            yield return null;
        }

        // Установка точной позиции игрока и его направления взгляда
        playerTransform.position = targetPosition;
        playerTransform.forward = viewDirection;
    }
}