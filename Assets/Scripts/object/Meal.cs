using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meal : MonoBehaviour
{
    public InventoryItem inventoryItem; // Предмет, который будет добавлен в инвентарь при использовании еды.
    public float eda = 30.0f; // Количество голода, которое восполняет этот объект еды.
    public float interactionRange = 3.0f; // Дистанция взаимодействия.
    private Camera playerCamera; // Ссылка на камеру игрока.
    private StaminaHunger staminaHunger; // Ссылка на компонент StaminaHunger игрока.

    void Start()
    {
        // Предполагаем, что у игрока есть основная камера.
        playerCamera = Camera.main;

        // Находим объект игрока по имени "Player".
        GameObject playerObject = GameObject.Find("Player");

        // Получаем компонент StaminaHunger с найденного объекта игрока.
        if (playerObject != null)
        {
            staminaHunger = playerObject.GetComponent<StaminaHunger>();
            if (staminaHunger == null)
            {
                Debug.LogWarning("Не найден компонент StaminaHunger на объекте Player.");
            }
        }
        else
        {
            Debug.LogWarning("Не найден объект Player.");
        }
    }

    void Update()
    {
        // Проверяем нажатие клавиши "E" каждый кадр.
        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    void Interact()
    {
        // Выполняем рейкастинг от позиции камеры игрока в направлении, в которое она смотрит.
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, interactionRange))
        {
            // Проверяем, что объект, с которым столкнулся рейкаст, является этим объектом еды.
            if (hit.collider.gameObject == gameObject)
            {
                // Если это так, вызываем метод Eda.
                Eda();
            }
        }
    }

    // Метод для уменьшения голода и добавления еды в инвентарь.
    public void Eda()
    {
        if (staminaHunger != null)
        {
            // Уменьшаем уровень голода игрока на значение eda, не позволяя ему опуститься ниже 0.
            staminaHunger.playerHunger = Mathf.Max(staminaHunger.playerHunger - eda, 0.0f);
            // Обновляем UI после изменения голода.
            staminaHunger.UpdateStamina();

            // Добавляем предмет в инвентарь
            if (inventoryItem != null)
            {
                Inventory inventory = FindObjectOfType<Inventory>(); // Получаем ссылку на инвентарь
                if (inventory != null)
                {
                    inventory.AddItem(inventoryItem);
                }
                else
                {
                    Debug.LogWarning("Не найден объект Inventory.");
                }
            }
            else
            {
                Debug.LogWarning("Не задан предмет для добавления в инвентарь.");
            }

            // Уничтожаем объект еды после того, как он был съеден.
            Destroy(gameObject);
        }
        else
        {
            Debug.LogWarning("Не найден компонент StaminaHunger на объекте Player.");
        }
    }
}