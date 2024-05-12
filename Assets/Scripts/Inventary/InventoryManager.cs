using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI; 

public class InventoryManager : MonoBehaviour
{
    public GameObject inventoryPanel; // Ссылка на панель инвентаря в Unity
    public Image[] itemSlots; // Массив слотов для отображения предметов
    public TMP_Text goalText; // Ссылка на TextMeshPro для отображения целей игрока
    public Sprite keySprite; // Изображение ключа
    private List<Item> items = new List<Item>(); // Список предметов в инвентаре

    void Update()
    {
        // Проверяем нажатие клавиши "Tab"
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            // Обновляем содержимое инвентаря при его отображении
            if (inventoryPanel.activeSelf)
                UpdateInventory();
        }
    }



    // Метод для обновления содержимого инвентаря
    void UpdateInventory()
    {
        // Перебираем все слоты инвентаря
        for (int i = 0; i < itemSlots.Length; i++)
        {
            // Если есть предмет для отображения в этом слоте и этот предмет есть в инвентаре
            if (i < items.Count)
            {
                // Отображаем предмет в этом слоте
                itemSlots[i].sprite = keySprite; // Устанавливаем изображение ключа
                itemSlots[i].gameObject.SetActive(true); // Включаем отображение спрайта
            }
            else
            {
                // Если слот пустой или предмет уже использован
                itemSlots[i].sprite = null; // Стираем спрайт
                itemSlots[i].gameObject.SetActive(false); // Отключаем отображение спрайта
            }
        }

        // Устанавливаем текст целей игрока
        goalText.text = "Task:";
    }

    // Метод для добавления предмета в инвентарь
    public void AddItem(Item item)
    {
        // Добавляем предмет в инвентарь
        items.Add(item);

        // Устанавливаем изображение предмета в соответствующем слоте
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (!itemSlots[i].gameObject.activeSelf)
            {
                itemSlots[i].sprite = keySprite; 
                itemSlots[i].gameObject.SetActive(true);
                break;
            }
        }

        // Обновляем содержимое инвентаря
        UpdateInventory();
    }

    // Метод для удаления предмета из инвентаря
    public void RemoveItem(Item item)
    {
        // Удаляем предмет из инвентаря
        items.Remove(item);

        // Удаляем Image предмета из соответствующего слота
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i].sprite == keySprite)
            {
                Destroy(itemSlots[i].gameObject); // Удаляем объект Image
                break;
            }
        }

        // Обновляем содержимое инвентаря
        UpdateInventory();
    }

    // Метод для удаления спрайта ключа из инвентаря
    public void RemoveKeySprite()
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i].sprite == keySprite) // Проверяем, если текущий спрайт совпадает со спрайтом ключа
            {
                itemSlots[i].gameObject.SetActive(false); // Отключаем объект слота
                itemSlots[i].sprite = null; // Очищаем спрайт слота
                break;
            }
        }
    }
}


[System.Serializable]
public class Item
{
    public string name;
}