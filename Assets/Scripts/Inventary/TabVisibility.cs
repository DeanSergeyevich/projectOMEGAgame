using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabVisibility : MonoBehaviour
{
    public GameObject UIInventoryTaskPlayer;
    private Animator animator;
    private bool isPanelVisible = false;
    private InventoryManager inventoryManager; // ������ �� ������ ���������� ����������

    // Start ���������� ����� ����������� ������� �����
    void Start()
    {
        animator = UIInventoryTaskPlayer.GetComponent<Animator>();
        inventoryManager = FindObjectOfType<InventoryManager>(); // �������� ������ �� ������ ���������� ����������
    }

    // ���������� ���������� ���� ��� ��� ������� �����
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            // ����������� �������� isVisible � ��������� ��������������� ��������
            isPanelVisible = !isPanelVisible;
            animator.SetBool("isVisible", isPanelVisible);
        }
    }

    // ����� ��� ������ �� ������ �������� ��� ������������ ��������� ���������
    public void ToggleInventoryVisibility()
    {
        isPanelVisible = !isPanelVisible;
        animator.SetBool("isVisible", isPanelVisible);
    }
}