using UnityEngine;

public class RotateObjectScript : MonoBehaviour
{
    private GameObject heldObject;
    private bool isRightMouseButtonDown = false;
    private Vector3 initialCameraPosition;
    private Quaternion initialCameraRotation;
    private bool isGamePaused = false; // ����, ����� ����������, ���������� �� ����

    void Update()
    {
        // �������� ����������� ������ ������ ����
        if (Input.GetMouseButton(1) && heldObject != null)
        {
            if (!isRightMouseButtonDown)
            {
                // ���� ������ ������ ��� ���� ������, ��������� ��������� ��������� � ���������� ������
                initialCameraPosition = Camera.main.transform.position;
                initialCameraRotation = Camera.main.transform.rotation;

                // ���� � ��� ���� heldObject, ���������� ��� ����� �������
                if (heldObject != null)
                {
                    heldObject.GetComponent<Rigidbody>().useGravity = false;
                    Vector3 playerPosition = Camera.main.transform.position + Camera.main.transform.forward * 2.0f;
                    heldObject.transform.position = playerPosition;
                }

                // ������������ ����
                Time.timeScale = isGamePaused ? 1f : 0f;
                isGamePaused = !isGamePaused;
            }

            isRightMouseButtonDown = true;

            if (heldObject != null)
            {
                // ������� ������ ������, �� ����� �� ��������� ������
                float rotationSpeed = 5f;
                float mouseX = Input.GetAxis("Mouse X");
                float mouseY = Input.GetAxis("Mouse Y");

                heldObject.transform.Rotate(Vector3.up * -mouseX * rotationSpeed, Space.World);
                heldObject.transform.Rotate(Vector3.left * -mouseY * rotationSpeed, Space.World);
            }
        }
        else if(isRightMouseButtonDown)
        {
            // ���� ������ ��������, ���������� ���� � ������������ ����
            isRightMouseButtonDown = false;
            Time.timeScale = 1f;
            isGamePaused = false;
           // heldObject = null;
        }
    }

    // ����� ��� ��������� heldObject! ����� (GravityGun)
    public void SetHeldObject(GameObject newHeldObject)
    {
        heldObject = newHeldObject;
    }
}
