using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medkit : MonoBehaviour
{
    public float healingAmount = 30.0f; // Количество здоровья, которое восстанавливает аптечка
    public float interactionRange = 3.0f; // Дистанция взаимодействия
    private Camera playerCamera; // Ссылка на камеру игрока
    private HealthBar healthBar; // Ссылка на компонент HealthBar игрока

    void Start()
    {
        // Предполагаем, что у игрока есть основная камера
        playerCamera = Camera.main;

        // Находим объект игрока по имени "Player"
        GameObject playerObject = GameObject.Find("Player");

        // Получаем компонент HealthBar с найденного объекта игрока
        if (playerObject != null)
        {
            healthBar = playerObject.GetComponent<HealthBar>();
            if (healthBar == null)
            {
                Debug.LogWarning("Не найден компонент HealthBar на объекте Player.");
            }
        }
        else
        {
            Debug.LogWarning("Не найден объект Player.");
        }
    }

    void Update()
    {
        // Проверяем нажатие клавиши "E" каждый кадр
        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    void Interact()
    {
        // Выполняем рейкастинг от позиции камеры игрока в направлении, в которое она смотрит
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, interactionRange))
        {
            // Проверяем, что объект, с которым столкнулся рейкаст, является этим объектом аптечки
            if (hit.collider.gameObject == gameObject)
            {
                // Если это так, вызываем метод Heal
                Heal();
            }
        }
    }

    // Метод для восстановления здоровья
    public void Heal()
    {
        if (healthBar != null)
        {
            // Увеличиваем уровень здоровья игрока на значение healingAmount
            healthBar.ChangeHealth(healingAmount);
            // Уничтожаем объект аптечки после использования
            Destroy(gameObject);
        }
        else
        {
            Debug.LogWarning("Не найден компонент HealthBar на объекте Player.");
        }
    }
}