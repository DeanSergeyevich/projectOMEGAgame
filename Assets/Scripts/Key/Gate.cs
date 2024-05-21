using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public Animator gateAnimator; // Ссылка на компонент аниматора ворот
    public GameObject keyObject; // Ссылка на объект ключа
    private InventoryManager inventoryManager; // Ссылка на скрипт управления инвентарем

    private bool isOpen = false; // Флаг для отслеживания состояния ворот

    void Start()
    {
        // Получаем ссылку на скрипт управления инвентарем
        inventoryManager = FindObjectOfType<InventoryManager>();
    }

    private void Update()
    {
        // Проверка, находится ли игрок рядом с воротами, нажата ли кнопка "E" и ключ был подобран
        if (!isOpen && IsPlayerNearGate() && Input.GetKeyDown(KeyCode.E) && KeyIsPickedUp())
        {
            isOpen = true;
            OpenGate();
            Debug.Log("Gate opened!");
            // Удаляем ключ из инвентаря
            inventoryManager.RemoveItem(new Item { name = "Key" });
        }
    }

    private void OpenGate()
    {
        gateAnimator.SetTrigger("Open"); // Устанавливаем триггер "Open" в аниматоре
    }

    // Метод для проверки, находится ли игрок рядом с воротами
    private bool IsPlayerNearGate()
    {
        float distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        return distance < 2f; //расстояние, которое считается "рядом с воротами"
    }

    // Метод для проверки, был ли ключ подобран
    private bool KeyIsPickedUp()
    {
        if (keyObject != null)
        {
            Key keyScript = keyObject.GetComponent<Key>();
            if (keyScript != null)
            {
                return keyScript.IsPickedUp(); // Проверка, был ли ключ подобран
            }
        }
        return false;
    }
}