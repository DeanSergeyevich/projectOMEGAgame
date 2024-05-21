using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public Animator gateAnimator; // ������ �� ��������� ��������� �����
    public GameObject keyObject; // ������ �� ������ �����
    private InventoryManager inventoryManager; // ������ �� ������ ���������� ����������

    private bool isOpen = false; // ���� ��� ������������ ��������� �����

    void Start()
    {
        // �������� ������ �� ������ ���������� ����������
        inventoryManager = FindObjectOfType<InventoryManager>();
    }

    private void Update()
    {
        // ��������, ��������� �� ����� ����� � ��������, ������ �� ������ "E" � ���� ��� ��������
        if (!isOpen && IsPlayerNearGate() && Input.GetKeyDown(KeyCode.E) && KeyIsPickedUp())
        {
            isOpen = true;
            OpenGate();
            Debug.Log("Gate opened!");
            // ������� ���� �� ���������
            inventoryManager.RemoveItem(new Item { name = "Key" });
        }
    }

    private void OpenGate()
    {
        gateAnimator.SetTrigger("Open"); // ������������� ������� "Open" � ���������
    }

    // ����� ��� ��������, ��������� �� ����� ����� � ��������
    private bool IsPlayerNearGate()
    {
        float distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        return distance < 2f; //����������, ������� ��������� "����� � ��������"
    }

    // ����� ��� ��������, ��� �� ���� ��������
    private bool KeyIsPickedUp()
    {
        if (keyObject != null)
        {
            Key keyScript = keyObject.GetComponent<Key>();
            if (keyScript != null)
            {
                return keyScript.IsPickedUp(); // ��������, ��� �� ���� ��������
            }
        }
        return false;
    }
}