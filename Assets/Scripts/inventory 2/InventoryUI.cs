using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public GameObject inventorySlotPrefab; // Префаб для слота инвентаря
    public List<Transform> inventoryPanels; // Список панелей, на которых будут размещаться слоты

    private List<GameObject> slots = new List<GameObject>(); // Список созданных слотов

    // Метод обновления интерфейса инвентаря
    public void UpdateInventoryUI(List<InventoryItem> items)
    {
        if (inventoryPanels == null || inventoryPanels.Count == 0)
        {
            Debug.LogError("No inventory panels are set in the InventoryUI script.");
            return;
        }

        // Очистка старых слотов
        ClearInventorySlots();

        // Создание новых слотов
        for (int i = 0; i < items.Count; i++)
        {
            CreateInventorySlot(items[i], inventoryPanels[i % inventoryPanels.Count]);
        }
    }

    // Метод для создания одного слота инвентаря
    private void CreateInventorySlot(InventoryItem item, Transform panel)
    {
        GameObject newSlot = Instantiate(inventorySlotPrefab, panel);
        RectTransform slotRectTransform = newSlot.GetComponent<RectTransform>();
        slotRectTransform.localScale = Vector3.one; // Сброс масштаба, чтобы избежать неправильного масштабирования

        Image itemImage = newSlot.transform.Find("ItemImage").GetComponent<Image>();
        TextMeshProUGUI itemText = newSlot.transform.Find("ItemText").GetComponent<TextMeshProUGUI>();

        // Проверка на null перед установкой
        if (itemImage != null)
        {
            itemImage.sprite = item.itemIcon;
            itemImage.gameObject.SetActive(true);
        }
        if (itemText != null)
        {
            itemText.text = item.itemName;
            itemText.gameObject.SetActive(true);
        }

        slots.Add(newSlot);
    }

    // Метод для удаления всех ячеек из инвентаря
    public void ClearInventorySlots()
    {
        foreach (GameObject slot in slots)
        {
            Destroy(slot);
        }
        slots.Clear();
    }
}
