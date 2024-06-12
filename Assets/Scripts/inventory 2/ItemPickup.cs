using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public InventoryItem item; // Предполагается, что этот объект еды содержит информацию о предмете для инвентаря
    public Inventory inventory;
    public InventoryUI inventoryUI; // Ссылка на объект InventoryUI

    private bool isPlayerInRange = false; // Флаг для проверки, находится ли игрок в зоне триггера

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true; // Устанавливаем флаг в true, когда игрок входит в зону триггера
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false; // Устанавливаем флаг в false, когда игрок выходит из зоны триггера
        }
    }

    private void Update()
    {
        // Проверяем, находится ли игрок в зоне триггера и нажата ли клавиша "E"
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    private void Interact()
    {
        // Проверяем, является ли объект едой
        if (item != null)
        {
            // Если объект предмета, добавляем его в инвентарь и удаляем из сцены
            inventory.AddItem(item);
            inventoryUI.UpdateInventoryUI(inventory.GetItemsList());
            Destroy(gameObject);
            Debug.Log("Picked up: " + item.itemName); // Предполагается, что у предмета есть поле itemName
        }
    }
}