using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public Animator gateAnimator; // Ссылка на компонент аниматора ворот
    public GameObject keyObject; // Ссылка на объект ключа
    private InventoryManager inventoryManager; // Ссылка на скрипт управления инвентарем

    private bool isOpen = false;

    void Start()
    {
        // Получаем ссылку на скрипт управления инвентарем
        inventoryManager = FindObjectOfType<InventoryManager>();
    }

    private void Update()
    {
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

    private bool IsPlayerNearGate()
    {
        float distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        return distance < 2f; //расстояние, которое считается "рядом с воротами"
    }

    private bool KeyIsPickedUp()
    {
        if (keyObject != null)
        {
            Key keyScript = keyObject.GetComponent<Key>();
            if (keyScript != null)
            {
                return keyScript.IsPickedUp();
            }
        }
        return false;
    }
}