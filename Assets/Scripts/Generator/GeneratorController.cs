using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GeneratorController : MonoBehaviour
{
    public int requiredCanisters = 4; // ���������� ��������� ������� ��� ������� ����������
    private List<GameObject> insertedCanisters = new List<GameObject>(); // ������ ����������� �������
    public GameObject lift; // ������ �� ����

    private GameObject carriedCanister = null; // ������� �������� ��������

    void Update()
    {
        // ���������, ���� ������ E ������ � ������ ������� �� ���������
        if (Input.GetKeyDown(KeyCode.E) && IsPlayerLookingAtGenerator() && carriedCanister != null)
        {
            InsertCanister(); // ��������� �������� � ���������
        }
    }

    // ����� ��� ��������, ������� �� ����� �� ���������
    private bool IsPlayerLookingAtGenerator()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // ���������, ����� �� ��� � ��������� ����������
        if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Generator"))
        {
            return true;
        }
        return false;
    }

    // ����� ��� �������� �������� �������
    public void PickUpCanister(GameObject canister)
    {
        carriedCanister = canister;
    }

    // ����� ��� ������� �������� � ���������
    public void InsertCanister()
    {
        if (carriedCanister != null)
        {
            insertedCanisters.Add(carriedCanister); // ��������� �������� � ������ �����������
            carriedCanister = null; // ���������� ������� �������� ��������

            Debug.Log("�������� ���������. ������� ����������: " + insertedCanisters.Count);

            // ���������� ��������� ��������
            CanisterController.ResetCanisterState();

            // ���� ���������� ����������� ������� �������� ������������ ��������
            if (insertedCanisters.Count == requiredCanisters)
            {
                Debug.Log("��������� ��������� ���������! ���������...");
                ActivateGenerator(); // �������� ����� ��������� ����������
            }
        }
    }

    // ����� ��� ��������� ���������� � ������� �����
    private void ActivateGenerator()
    {
        Debug.Log("��������� �����������!");

        // ���������, ��� ������ �� ���� �����������
        if (lift != null)
        {
            lift.GetComponent<LiftController>().ActivateLift(); // ���������� ����
            Debug.Log("���� �����������!");
        }
        else
        {
            Debug.LogError("������ �� ���� �� ����������� � ����������!");
        }
    }
}