using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medkit : MonoBehaviour
{
    public float healingAmount = 30.0f; // ���������� ��������, ������� ��������������� �������
    public float interactionRange = 3.0f; // ��������� ��������������
    private Camera playerCamera; // ������ �� ������ ������
    private HealthBar healthBar; // ������ �� ��������� HealthBar ������

    void Start()
    {
        // ������������, ��� � ������ ���� �������� ������
        playerCamera = Camera.main;

        // ������� ������ ������ �� ����� "Player"
        GameObject playerObject = GameObject.Find("Player");

        // �������� ��������� HealthBar � ���������� ������� ������
        if (playerObject != null)
        {
            healthBar = playerObject.GetComponent<HealthBar>();
            if (healthBar == null)
            {
                Debug.LogWarning("�� ������ ��������� HealthBar �� ������� Player.");
            }
        }
        else
        {
            Debug.LogWarning("�� ������ ������ Player.");
        }
    }

    void Update()
    {
        // ��������� ������� ������� "E" ������ ����
        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    void Interact()
    {
        // ��������� ���������� �� ������� ������ ������ � �����������, � ������� ��� �������
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, interactionRange))
        {
            // ���������, ��� ������, � ������� ���������� �������, �������� ���� �������� �������
            if (hit.collider.gameObject == gameObject)
            {
                // ���� ��� ���, �������� ����� Heal
                Heal();
            }
        }
    }

    // ����� ��� �������������� ��������
    public void Heal()
    {
        if (healthBar != null)
        {
            // ����������� ������� �������� ������ �� �������� healingAmount
            healthBar.ChangeHealth(healingAmount);
            // ���������� ������ ������� ����� �������������
            Destroy(gameObject);
        }
        else
        {
            Debug.LogWarning("�� ������ ��������� HealthBar �� ������� Player.");
        }
    }
}