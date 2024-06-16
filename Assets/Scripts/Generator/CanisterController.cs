using UnityEngine;

public class CanisterController : MonoBehaviour
{
    private static bool isAnyCanisterTaken = false; // ���������� ��� ������������, ����� �� ���� �� ���� ��������
    public GameObject generator; // ������ �� ������ ����������

    private void Update()
    {
        // ���������, ���� ������ E ������ � ������ ������� �� ��������
        if (Input.GetKeyDown(KeyCode.E) && IsPlayerLookingAt() && !isAnyCanisterTaken)
        {
            TakeCanister(); // ����� ��������
        }
    }

    // ����� ��� ��������, ������� �� ����� �� ��������
    private bool IsPlayerLookingAt()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // ���������, ����� �� ��� � ��������� �������� � ��� �� ����� �� ��� ��� �������
        if (Physics.Raycast(ray, out hit) && hit.collider == GetComponent<Collider>() && !isAnyCanisterTaken)
        {
            return true;
        }
        return false;
    }

    // ����� ��� ������ �������� �������
    private void TakeCanister()
    {
        isAnyCanisterTaken = true; // ������������� ����, ��� ���� �� ���� �������� �����
        Debug.Log("�������� �����!");

        // ��������� ������ �������� ��� �������� ��� (� ������ ������ ��������� ��������� �������)
        gameObject.SetActive(false);

        // ���������� ��������� � ������� ��������
        if (generator != null)
        {
            generator.GetComponent<GeneratorController>().InsertCanister(gameObject); // �������� ������ �������� � ����� InsertCanister
        }
        else
        {
            Debug.LogError("������ �� ��������� �� ����������� � ��������!");
        }
    }

    // ����� ��� ����������� ��������� ������ ��������
    public static void ResetCanisterState()
    {
        isAnyCanisterTaken = false; // ���������� ���� ��� ������������� (��������, ��� ����������� ������)
    }
}