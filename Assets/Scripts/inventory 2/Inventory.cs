using UnityEngine;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    private List<InventoryItem> items = new List<InventoryItem>(); // Список предметов в инвентаре

    // Метод для добавления предмета в инвентарь
    public void AddItem(InventoryItem newItem)
    {
        items.Add(newItem);
    }

    // Метод для удаления предмета из инвентаря
    public void RemoveItem(InventoryItem itemToRemove)
    {
        items.Remove(itemToRemove);
    }

    // Метод для использования предмета из инвентаря
    public void UseItem(InventoryItem itemToUse)
    {
        // Добавьте здесь логику использования предмета
        Debug.Log("Использовано: " + itemToUse.itemName);
    }


    // Метод для получения массива предметов из инвентаря
    public List<InventoryItem> GetItemsList()
    {
        return new List<InventoryItem>(items);
    }
}