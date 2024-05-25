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
        // Здесь можно добавить логику для использования предмета
        // Например, если предмет - зелье здоровья, можно восстановить здоровье игрока и т. д.
    }

    // Метод для получения массива предметов из инвентаря
    public List<InventoryItem> GetItemsList()
    {
        return new List<InventoryItem>(items);
    }
}