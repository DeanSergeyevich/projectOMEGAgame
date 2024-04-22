using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BatteryController : MonoBehaviour
{
    // ��������� ����� �������
    public float startingBattery = 100f;

    // �������� ���������� ������ � ������� ��� ���������� ��������
    public float drainRate = 0.5f;

    // ������ �� ��������� ����� ��������
    public Light flashlight;

    // ������ �� ��������� ����������� ��� ����������� ������ ������
    public Image batteryIndicator;

    // ������� ������� ������
    private float currentBattery;

    private void Start()
    {
        currentBattery = startingBattery;
        UpdateBatteryUI();
        // ������ ���������� ������ �������
        InvokeRepeating("DrainBattery", 1f, 1f);
    }

    private void UpdateBatteryUI()
    {
        // ���������� fillAmount �� ������ �������� ������ ������ �������
        float fillAmount = currentBattery / startingBattery;

        // ������������� fillAmount ��� ����������� ������ ������
        if (batteryIndicator != null)
        {
            batteryIndicator.fillAmount = fillAmount;
        }
    }

    private void DrainBattery()
    {
        // ���� ������� ������� � ������� ������ ������ 0, ��������� ����� �������
        if (flashlight.enabled && currentBattery > 0f)
        {
            currentBattery -= drainRate;
            UpdateBatteryUI();

            // ���������, ������ �� ����� ����
            if (currentBattery <= 0f)
            {
                // ��������� ������
                flashlight.enabled = false;
                // ������� ��������� �� ����
                Debug.Log("Battery drained! Flashlight turned off.");
            }
        }
    }

    // ����� ��� ��������� ��������
    public void TurnOnFlashlight()
    {
        // �������� ������� ������ ���� �� �������� � � ��� ���� ����� � ���������
        if (!flashlight.enabled && currentBattery > 0f)
        {
            flashlight.enabled = true;
        }
    }

    // ����� ��� ��������, ����� �� �������
    public bool IsBatteryNotEmpty()
    {
        return currentBattery > 0f;
    }

    // ����� ��� ����������� �������
    public void RechargeBattery(float amount)
    {
        currentBattery = Mathf.Min(currentBattery + amount, startingBattery);
        UpdateBatteryUI();
    }
}
