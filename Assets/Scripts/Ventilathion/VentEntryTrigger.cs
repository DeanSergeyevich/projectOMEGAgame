using UnityEngine;

public class VentEntryTrigger : MonoBehaviour
{
    public Transform ventCameraPosition; // ������� ������ � ����������
    public GameObject playerBody; // ���� ������
    public Animator playerAnimator; // �������� ���� ������
    public KeyCode enterVentKey = KeyCode.E; // ������� ��� ����� � ����������

    public LayerMask interactionLayer; // ���� ��� �������� �������������� � �����������
    public float interactionDistance = 2f; // ��������� �������������� � �����������
    public Camera playerCamera; // ������ ������

    private bool isTransitioning = false; // ����, �����������, ���� �� � ������ ������ ������� ����������� � ����������
    private float animationDuration = 3f; // ����������������� �������� ����������� ������

    private void Update()
    {
        // ��������� ������� ������ "E" � ��������� �� �� � ���� �������������� � ����������� � ������� �� �� �� ���
        if (Input.GetKeyDown(enterVentKey) && !isTransitioning && CanEnterVent())
        {
            // ������� ����������� ������ � ����������, ���� ���������� � ������ ������ �� �������
            SmoothCameraTransition();
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

    private void SmoothCameraTransition()
    {
        isTransitioning = true; // ������������� ���� � true, ����� ������������� ��������� ������ ��������

        // ��������� �������� �������� ����������� ������ � ����������
        // ��� ����� ���� ����������� ���������� ���������, ��������, � ������� ��������� ��� ����

        // ����� ���������� �������� �������� ����� ������������ ���� ������
        Invoke("TeleportPlayerBody", animationDuration);
    }

    private void TeleportPlayerBody()
    {
        // ������������� ���� ������ � ������� ������ � ����������
        playerBody.transform.position = ventCameraPosition.position;
        playerBody.transform.rotation = ventCameraPosition.rotation;

        // ���������� �������� ���������� ���� ������
        playerAnimator.SetBool("IsCrouching", true);

        isTransitioning = false; // ���������� ����, ����� ��������� ����� ���� � ����������
    }
}
