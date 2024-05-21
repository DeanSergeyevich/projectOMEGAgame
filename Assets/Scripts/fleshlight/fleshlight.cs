using System.Collections;
using UnityEngine;

public class fleshlight : MonoBehaviour
{
    public Light spotL; // ������ �� ��������� ����� ��������
    public Transform playerCamera; // ������ �� ������� ������ ������
    public float delay = 0.1f; // �������� ����� ������������ ������� � �������� ��������
    public float smoothFactor = 5f; // �������� ��� �����������, ��������� ������� ����� ��������

    // ������ �� ������ ���������� ��������
    public BatteryController batteryController;

    private void Start()
    {
        StartCoroutine(RepeatCameraMovement()); // ��������� �������� ��� ���������� ������� � �������� ��������
    }

    // �������� ��� �������������� ���������� ������� � �������� ��������
    private IEnumerator RepeatCameraMovement()
    {
        while (true)
        {
            // ��������� ������� ������� � �������� ������
            Vector3 currentCameraPosition = playerCamera.position;
            Quaternion currentCameraRotation = playerCamera.rotation;

            yield return new WaitForSeconds(delay);

            // ��������� �������� ��� �������� �������� �������� � ������� � �������� ������
            StartCoroutine(MoveSmoothly(currentCameraPosition, currentCameraRotation));
        }
    }

    // ������� �������� �������� � ������� � �������� ������ ������
    private IEnumerator MoveSmoothly(Vector3 targetPosition, Quaternion targetRotation)
    {
        float elapsedTime = 0f;
        Vector3 startingPosition = transform.position;
        Quaternion startingRotation = transform.rotation;

        while (elapsedTime < delay)
        {
            // �������� ������������ ������� ��������
            transform.position = Vector3.Lerp(startingPosition, targetPosition, elapsedTime / delay);
            // ����������� ������������ �������� ��������
            transform.rotation = Quaternion.Slerp(startingRotation, targetRotation, elapsedTime / delay);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // �����������, ��� ��������� ��������� � �������� ����� �����������
        transform.position = targetPosition;
        transform.rotation = targetRotation;
    }

    private void Update()
    {
        // ���� ������ ������� F � ������� �� �����
        if (Input.GetKeyDown(KeyCode.F) && batteryController.IsBatteryNotEmpty())
        {
            spotL.enabled = !spotL.enabled; // �������� ��� ��������� �������
        }

        // ������ ���������� ������� � ������� ������
        transform.rotation = Quaternion.Slerp(transform.rotation, playerCamera.rotation, Time.deltaTime * smoothFactor);
    }
}
