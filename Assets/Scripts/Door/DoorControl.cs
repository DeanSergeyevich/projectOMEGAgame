using UnityEngine;

public class DoorControl : MonoBehaviour
{
    private new HingeJoint hingeJoint;
    private JointMotor initialMotor;
    private Rigidbody rb;

    public float openSpeed = 100.0f;
    public float rotationSensitivity = 2.0f;
    public Camera playerCamera;
    private bool isFrozen = false;

    private void Start()
    {
        hingeJoint = GetComponent<HingeJoint>();
        rb = GetComponent<Rigidbody>();
        initialMotor = hingeJoint.motor;
    }

    public void InteractDoor()
    {
        float mouseX = Input.GetAxis("Mouse X");

        JointMotor motor = hingeJoint.motor;
        motor.targetVelocity = -mouseX * openSpeed * rotationSensitivity;
        hingeJoint.motor = motor;

        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Выполняем Raycast с максимальным расстоянием maxRaycastDistance
        if (Physics.Raycast(ray, out hit))
        {
            GameObject hitObject = hit.collider.gameObject;

            // Проверяем тег объекта, с которым столкнулся луч
            if (hitObject.CompareTag("FreezeCamera"))
            {
                isFrozen = !isFrozen;
                playerCamera.GetComponent<CameraMovement>().enabled = !isFrozen;
            }
            else if (!hitObject.CompareTag("BoxCollider"))
            {
                // Если объект не имеет тега "BoxCollider", применяем управление дверью
                hingeJoint.motor = motor;
            }
            else
            {
                // Если объект имеет тег "BoxCollider", не применяем управление дверью
                hingeJoint.motor = initialMotor;
            }
        }
        else
        {
            hingeJoint.motor = initialMotor;
        }
    }

    public void Frozen()
    {
        isFrozen = false;
        playerCamera.GetComponent<CameraMovement>().enabled = true;
    }
}