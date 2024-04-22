using UnityEngine;

public class BatteryPickup : MonoBehaviour
{
    // ������� ������ ���� ��������� ��� ��������
    public float rechargeAmount = 100f;

    // ������ �� ��������� ���������� ��������
    public BatteryController batteryController;

    private void Update()
    {
        // ���� ������ ������� "E" � ����� ��������� � ������� ���������, ����������� ������� � ���������� ������-���������
        if (Input.GetKeyDown(KeyCode.E) && IsPlayerInRange())
        {
            batteryController.RechargeBattery(rechargeAmount);
            Destroy(gameObject);
        }
    }

    private bool IsPlayerInRange()
    {
        // ���������, ���� �� ������ Player � ������� �������� ���������
        Collider[] colliders = Physics.OverlapSphere(transform.position, 1f);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                return true;
            }
        }
        return false;
    }
}
