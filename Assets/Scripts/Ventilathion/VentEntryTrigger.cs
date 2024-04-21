using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class VentEntryTrigger : MonoBehaviour
{
    private Transform ventCameraPosition; // ������� ������ � ����������
    public GameObject playerBody; // ���� ������
    public Animator playerAnimator; // �������� ���� ������
    public KeyCode enterVentKey = KeyCode.E; // ������� ��� ����� � ����������

    public LayerMask interactionLayer; // ���� ��� �������� �������������� � �����������
    public float interactionDistance = 2f; // ��������� �������������� � �����������
    public Camera playerCamera; // ������ ������

    public Transform[] ventEntryPoints; // ������ ����� ����� � ����������
    public UnityEvent OnEnterVent; // ������� ��� ������� � ����� � ����������

    private bool isTransitioning = false; // ����, �����������, ���� �� � ������ ������ ������� ����������� � ����������

    private void Update()
    {
        // ��������� ������� ������ "E" � ��������� �� �� � ���� �������������� � ����������� � ������� �� �� �� ���
        if (Input.GetKeyDown(enterVentKey) && !isTransitioning && CanEnterVent())
        {
            // ������ ����� ����� � ����������, �� ������� ������� �����
            Transform nearestEntryPoint = FindNearestEntryPoint();

            if (nearestEntryPoint != null)
            {
                // ����������� ������ � ����������
                StartCoroutine(TransitionToVent(nearestEntryPoint));
            }
        }
    }

    private bool CanEnterVent()
    {
        // ������� ��� �� ������ ������ ������ � �����������, ���� ������� ������
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        // ���������, ����� �� ��� � ���� �������������� � �����������
        if (Physics.Raycast(ray, out hit, interactionDistance, interactionLayer))
        {
            return true;
        }

        return false;
    }

    private IEnumerator TransitionToVent(Transform entryPoint)
    {
        isTransitioning = true; // ������������� ���� � true, ����� ������������� ��������� ������ ��������

        // ��������� ������� ������ � ����������
        ventCameraPosition = entryPoint;

        // ���������� ������ � ��������� ����� ����� � ����������
        CharacterController playerController = playerBody.GetComponent<CharacterController>();
        if (playerController != null)
        {
            playerController.enabled = false; // ��������� CharacterController ����� ������������
            playerBody.transform.position = ventCameraPosition.position;
            playerBody.transform.rotation = ventCameraPosition.rotation;
            playerController.enabled = true; // �������� CharacterController ����� �����������
        }

        // ���������� �������� ���������� ���� ������
        playerAnimator.SetBool("IsCrouching", true);

        // ���������� ������ � ����� � ����������
        OnEnterVent.Invoke();

        yield return null;

        isTransitioning = false; // ���������� ����, ����� ��������� ����� ���� � ����������
    }

    private Transform FindNearestEntryPoint()
    {
        Transform nearestEntryPoint = null;
        float highestDotProduct = -Mathf.Infinity;
        Vector3 playerDirection = playerCamera.transform.forward;

        foreach (Transform entryPoint in ventEntryPoints)
        {
            Vector3 directionToEntryPoint = (entryPoint.position - playerCamera.transform.position).normalized;
            float dotProduct = Vector3.Dot(playerDirection, directionToEntryPoint);

            // ���������, ��������� �� ����� ����� � ���������� � ���� ������ ������
            if (dotProduct > highestDotProduct)
            {
                highestDotProduct = dotProduct;
                nearestEntryPoint = entryPoint;
            }
        }

        return nearestEntryPoint;
    }
}
