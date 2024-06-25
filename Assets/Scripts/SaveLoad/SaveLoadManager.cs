using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
   // public CameraMovement cameraMovement; // Компонент управления движением камеры
    public Movement movement; // Компонент управления движением игрока
    public BatteryController batteryController; // Компонент управления батареей
    public HealthBar healthBar; // Компонент управления здоровьем
    public StaminaHunger staminaHunger; // Компонент управления выносливостью и голодом
    public Transform cameraTransform; // Трансформ камеры
    public Transform playerTransform; // Трансформ игрока

    public GeneratorController[] generators; // Массив генераторов
    public CanisterController[] canisters; // Массив канистр
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
       // cameraMovement.mouseSensitivity = 5f;
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

        foreach (var generator in generators)
        {
            generator.ResetGenerator();
        }

        foreach (var canister in canisters)
        {
            canister.ResetCanister();
        }
    }

    // Сохранение игры
    public void SaveGame()
    {
        // Создание объекта GameData и заполнение его текущими значениями
        GameData gameData = new GameData
        {
           // mouseSensitivity = cameraMovement.mouseSensitivity,
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
            enemyPosition = enemyTransform.position,
            enemyRotation = enemyTransform.rotation.eulerAngles,
            generators = new GeneratorData[generators.Length],
            canisters = new bool[canisters.Length]
        };

        // Сохранение состояний генераторов
        for (int i = 0; i < generators.Length; i++)
        {
            gameData.generators[i] = new GeneratorData
            {
                insertedCanistersCount = generators[i].GetInsertedCanistersCount()
            };
        }

        // Сохранение состояний канистр
        for (int i = 0; i < canisters.Length; i++)
        {
            gameData.canisters[i] = !canisters[i].gameObject.activeSelf; // Сохранение состояния (взята или нет)
        }

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
              //  cameraMovement.mouseSensitivity = gameData.mouseSensitivity;
                movement.Speed = gameData.speed;
                movement.jumpHeight = gameData.jumpHeight;
                movement.runSpeed = gameData.runSpeed;
                movement.stamina.playerStamina = gameData.playerStamina;
                movement.stamina.maxStamina = gameData.maxStamina;
                batteryController.currentBattery = gameData.currentBattery;
                healthBar.fill = gameData.health;
                staminaHunger.playerHunger = gameData.playerHunger;

                // Перемещение игрока к сохранённой позиции
                StartCoroutine(MovePlayerToSavedPosition(gameData.playerPosition));

                // Установка позиции и состояния врага
                enemyTransform.position = gameData.enemyPosition;
                enemyTransform.rotation = Quaternion.Euler(gameData.enemyRotation);

                // Восстановление состояний генераторов
                for (int i = 0; i < generators.Length; i++)
                {
                    generators[i].SetInsertedCanisters(gameData.generators[i].insertedCanistersCount);
                }

                // Восстановление состояний канистр
                for (int i = 0; i < canisters.Length; i++)
                {
                    if (gameData.canisters[i])
                    {
                        canisters[i].gameObject.SetActive(false);
                    }
                    else
                    {
                        canisters[i].gameObject.SetActive(true);
                    }
                }

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

    // Корутина для плавного перемещения игрока к сохранённой позиции
    private IEnumerator MovePlayerToSavedPosition(Vector3 targetPosition)
    {
        // Ваша логика перемещения игрока, например:
        float startTime = Time.time;
        float journeyLength = Vector3.Distance(playerTransform.position, targetPosition);
        float duration = 0.1f; // Продолжительность перемещения

        while (Time.time - startTime < duration)
        {
            float fraction = (Time.time - startTime) / duration;
            playerTransform.position = Vector3.Lerp(playerTransform.position, targetPosition, fraction);
            yield return null;
        }

        // В конце корутины установите точную позицию игрока
        playerTransform.position = targetPosition;
    }

    // Метод для удаления сохранения
    public void DeleteSave()
    {
        if (File.Exists(savePath))
        {
            try
            {
                File.Delete(savePath);
                Debug.Log("Save file deleted from " + savePath);
            }
            catch (IOException ex)
            {
                Debug.LogError("Failed to delete save file: " + ex.Message);
            }
        }
        else
        {
            Debug.LogWarning("Save file not found!");
        }
    }
}