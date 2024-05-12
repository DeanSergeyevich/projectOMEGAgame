using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI; 

public class InventoryManager : MonoBehaviour
{
    public GameObject inventoryPanel; // ������ �� ������ ��������� � Unity
    public Image[] itemSlots; // ������ ������ ��� ����������� ���������
    public TMP_Text goalText; // ������ �� TextMeshPro ��� ����������� ����� ������
    public Sprite keySprite; // ����������� �����
    private List<Item> items = new List<Item>(); // ������ ��������� � ���������

    void Update()
    {
        // ��������� ������� ������� "Tab"
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            // ��������� ���������� ��������� ��� ��� �����������
            if (inventoryPanel.activeSelf)
                UpdateInventory();
        }
    }



    // ����� ��� ���������� ����������� ���������
    void UpdateInventory()
    {
        // ���������� ��� ����� ���������
        for (int i = 0; i < itemSlots.Length; i++)
        {
            // ���� ���� ������� ��� ����������� � ���� ����� � ���� ������� ���� � ���������
            if (i < items.Count)
            {
                // ���������� ������� � ���� �����
                itemSlots[i].sprite = keySprite; // ������������� ����������� �����
                itemSlots[i].gameObject.SetActive(true); // �������� ����������� �������
            }
            else
            {
                // ���� ���� ������ ��� ������� ��� �����������
                itemSlots[i].sprite = null; // ������� ������
                itemSlots[i].gameObject.SetActive(false); // ��������� ����������� �������
            }
        }

        // ������������� ����� ����� ������
        goalText.text = "Task:";
    }

    // ����� ��� ���������� �������� � ���������
    public void AddItem(Item item)
    {
        // ��������� ������� � ���������
        items.Add(item);

        // ������������� ����������� �������� � ��������������� �����
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (!itemSlots[i].gameObject.activeSelf)
            {
                itemSlots[i].sprite = keySprite; 
                itemSlots[i].gameObject.SetActive(true);
                break;
            }
        }

        // ��������� ���������� ���������
        UpdateInventory();
    }

    // ����� ��� �������� �������� �� ���������
    public void RemoveItem(Item item)
    {
        // ������� ������� �� ���������
        items.Remove(item);

        // ������� Image �������� �� ���������������� �����
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i].sprite == keySprite)
            {
                Destroy(itemSlots[i].gameObject); // ������� ������ Image
                break;
            }
        }

        // ��������� ���������� ���������
        UpdateInventory();
    }

    // ����� ��� �������� ������� ����� �� ���������
    public void RemoveKeySprite()
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i].sprite == keySprite) // ���������, ���� ������� ������ ��������� �� �������� �����
            {
                itemSlots[i].gameObject.SetActive(false); // ��������� ������ �����
                itemSlots[i].sprite = null; // ������� ������ �����
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