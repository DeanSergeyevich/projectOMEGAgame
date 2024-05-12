using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public GameObject keyObject;
    private bool isPickedUp = false;
    private InventoryManager inventoryManager; // ������ �� ������ ���������� ���������

    void Start()
    {
        // �������� ������ �� ������ ���������� ���������
        inventoryManager = FindObjectOfType<InventoryManager>();
    }

    void Update()
    {
        // ���������, ������� �� ����� �� ���� � ��������� �� �� ����� � ���
        if (!isPickedUp && Input.GetKeyDown(KeyCode.E) && IsPlayerLookingAtKey() && IsPlayerNearKey())
        {
            isPickedUp = true;
            keyObject.SetActive(false);
            // ��������� ���� � ���������
            inventoryManager.AddItem(new Item { name = "Key" });
            Debug.Log("Key picked up!");
        }
        // ��������� ������� ������ ��� ������������� �����
        if (isPickedUp && Input.GetKeyDown(KeyCode.U))
        {
            UseKey();
        }
    }

    // ����� ��� ��������, ������� �� ����� �� ����
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

    // ����� ��� ��������, ��������� �� ����� ����� � ������
    private bool IsPlayerNearKey()
    {
        float distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        return distance < 2f; //  ����������, ������� ��������� "����� � ������"
    }

    public bool IsPickedUp()
    {
        return isPickedUp;
    }

    // ����� ��� ������������� �����
    public void UseKey()
    {
        if (isPickedUp)
        {
            isPickedUp = false;
            inventoryManager.RemoveItem(new Item { name = "Key" });
            inventoryManager.RemoveKeySprite(); // ������� ������ ����� �� ���������
        }
    }
}
