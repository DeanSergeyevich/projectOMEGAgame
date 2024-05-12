using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public GameObject keyObject;
    private bool isPickedUp = false;
    private InventoryManager inventoryManager; // Ссылка на скрипт управления инвентарём

    void Start()
    {
        // Получаем ссылку на скрипт управления инвентарём
        inventoryManager = FindObjectOfType<InventoryManager>();
    }

    void Update()
    {
        // Проверяем, смотрит ли игрок на ключ и находится ли он рядом с ним
        if (!isPickedUp && Input.GetKeyDown(KeyCode.E) && IsPlayerLookingAtKey() && IsPlayerNearKey())
        {
            isPickedUp = true;
            keyObject.SetActive(false);
            // Добавляем ключ в инвентарь
            inventoryManager.AddItem(new Item { name = "Key" });
            Debug.Log("Key picked up!");
        }
        // Проверяем нажатие кнопки для использования ключа
        if (isPickedUp && Input.GetKeyDown(KeyCode.U))
        {
            UseKey();
        }
    }

    // Метод для проверки, смотрит ли игрок на ключ
    private bool IsPlayerLookingAtKey()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit))
        {
            if (hit.collider.gameObject == keyObject)
            {
                return true;
            }
        }
        return false;
    }

    // Метод для проверки, находится ли игрок рядом с ключом
    private bool IsPlayerNearKey()
    {
        float distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        return distance < 2f; //  расстояние, которое считается "рядом с ключом"
    }

    public bool IsPickedUp()
    {
        return isPickedUp;
    }

    // Метод для использования ключа
    public void UseKey()
    {
        if (isPickedUp)
        {
            isPickedUp = false;
            inventoryManager.RemoveItem(new Item { name = "Key" });
            inventoryManager.RemoveKeySprite(); // Убираем спрайт ключа из инвентаря
        }
    }
}
