using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")] // Атрибут, который добавляет возможность создания объекта через контекстное меню
public class InventoryItem : ScriptableObject
{
    public string itemName; // Название предмета
    public Sprite itemIcon; // Иконка предмета
    //public string description; // Описание предмета
    // Другие данные о предмете (например, стоимость, вес, характеристики и т. д.)
}
