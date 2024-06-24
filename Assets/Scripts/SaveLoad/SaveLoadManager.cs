using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    public CameraMovement cameraMovement; // ��������� ���������� ��������� ������
    public Movement movement; // ��������� ���������� ��������� ������
    public BatteryController batteryController; // ��������� ���������� ��������
    public HealthBar healthBar; // ��������� ���������� ���������
    public StaminaHunger staminaHunger; // ��������� ���������� ������������� � �������
    public Transform cameraTransform; // ��������� ������
    public Transform playerTransform; // ��������� ������

    public EnemyController enemyController; // ���������� �����
    public Transform enemyTransform; // ��������� �����

    private string savePath; // ���� � ����� ����������

    void Start()
    {
        // ��������� ���� � ����� ����������
        savePath = Path.Combine(Application.persistentDataPath, "savefile.json");

        // ���� ���� ���������� �� ����������, ���������� �������� �� ��������� � ��������� ����
        if (!File.Exists(savePath))
        {
            SetDefaultValues();
            SaveGame();
        }
    }

    // ��������� �������� �� ��������� ��� ���� �����������
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

    // ���������� ����
    public void SaveGame()
    {
        // �������� ������� GameData � ���������� ��� �������� ����������
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
            playerViewDirection = playerTransform.forward, // ��������� ����������� ������� ������
            enemyPosition = enemyTransform.position,
            enemyRotation = enemyTransform.rotation.eulerAngles,
        };

        // �������������� ������ � JSON � ������ � ����
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

    // �������� ����
    public void LoadGame()
    {
        if (File.Exists(savePath))
        {
            try
            {
                // ������ ����� � �������������� JSON � ������ GameData
                string json = File.ReadAllText(savePath);
                GameData gameData = JsonUtility.FromJson<GameData>(json);

                // ��������� �������� ����������� �� ����������� ������
                cameraMovement.mouseSensitivity = gameData.mouseSensitivity;
                movement.Speed = gameData.speed;
                movement.jumpHeight = gameData.jumpHeight;
                movement.runSpeed = gameData.runSpeed;
                movement.stamina.playerStamina = gameData.playerStamina;
                movement.stamina.maxStamina = gameData.maxStamina;
                batteryController.currentBattery = gameData.currentBattery;
                healthBar.fill = gameData.health;
                staminaHunger.playerHunger = gameData.playerHunger;

                // ����������� ������ � ���������� ������� � �������������� ����������� �������
                StartCoroutine(MovePlayerToSavedPosition(gameData.playerPosition, gameData.playerViewDirection));

                // ��������� ������� � ��������� �����
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

    // �������� ��� �������� ����������� ������ � ���������� ������� � ��������������� ����������� �������
    private IEnumerator MovePlayerToSavedPosition(Vector3 targetPosition, Vector3 viewDirection)
    {
        // ������ ����������� ������
        float startTime = Time.time;
        float journeyLength = Vector3.Distance(playerTransform.position, targetPosition);
        float duration = 0.1f; // ����������������� �����������

        while (Time.time - startTime < duration)
        {
            float fraction = (Time.time - startTime) / duration;
            playerTransform.position = Vector3.Lerp(playerTransform.position, targetPosition, fraction);
            yield return null;
        }

        // ��������� ������ ������� ������ � ��� ����������� �������
        playerTransform.position = targetPosition;
        playerTransform.forward = viewDirection;
    }
}