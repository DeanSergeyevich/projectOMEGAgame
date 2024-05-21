using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public InventoryItem item;
    public Inventory inventory;
    public InventoryUI inventoryUI; // Ссылка на объект InventoryUI

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (inventory != null)
            {
                inventory.AddItem(item);
                inventoryUI.UpdateInventoryUI(inventory.GetItemsList());
                Destroy(gameObject);
                Debug.Log("Picked up: " + item.itemName);
            }
        }
    }
}
