using UnityEngine;
using System.Collections.Generic;

public class GeneratorController : MonoBehaviour
{
    public int requiredCanisters = 4;
    public List<GameObject> canisters = new List<GameObject>(); // ������ ���� ������� � �����
    private List<GameObject> insertedCanisters = new List<GameObject>(); // ������ ����������� �������
    public GameObject lift;

    private void Start()
    {
        // ������� ��� �������� � ����� � ��������� �� � ������
        GameObject[] allCanisters = GameObject.FindGameObjectsWithTag("Canister");
        foreach (GameObject canister in allCanisters)
        {
            canisters.Add(canister);
        }
    }

    private void Update()
    {
        // ���������, ���� ������ E ������ � ������ ������� �� ���������
        if (Input.GetKeyDown(KeyCode.E) && IsPlayerLookingAtGenerator())
        {
            InsertCanister(gameObject); // ��������� �������� � ���������
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

    // ����� ��� ������� �������� � ���������
    public void InsertCanister(GameObject gameObject)
    {
        // ���������, ��� ���� �������� ��� �������
        if (canisters.Count > 0)
        {
            GameObject canister = canisters[0]; // ����� ������ ��������� ��������

            if (!insertedCanisters.Contains(canister))
            {
                insertedCanisters.Add(canister); // ��������� �������� � ������ �����������
                canisters.Remove(canister); // ������� �������� �� ������ ���� �������

                Debug.Log("�������� ���������. ������� ����������: " + insertedCanisters.Count);

                // ���� ���������� ����������� ������� �������� ������������ ��������
                if (insertedCanisters.Count == requiredCanisters)
                {
                    Debug.Log("��������� ��������� ���������! ���������...");
                    ActivateGenerator(); // �������� ����� ��������� ����������
                }
            }
            else
            {
                Debug.LogWarning("�������� ��� ��������� � ���������!");
            }
        }
        else
        {
            Debug.LogWarning("��� ��������� ������� ��� �������!");
        }
    }

    // ����� ��� ��������� ���������� � ������� �����
    private void ActivateGenerator()
    {
        // ��������� ����������
        Debug.Log("��������� �����������!");

        // ���������, ��� ������ �� ���� �����������
        if (lift != null)
        {
            lift.SetActive(true); // ���������� ����
            Debug.Log("���� �����������!");
        }
        else
        {
            Debug.LogError("������ �� ���� �� ����������� � ����������!");
            return;
        }

        // ��������� ������� �� ����� ������� (� ��������� �����)
        LoadNextLevel();
    }

    // ����� ��� �������� ��������� �����
    private void LoadNextLevel()
    {
        // ����� ����� �������� ��� ��� �������� ��������� �����
    }
}