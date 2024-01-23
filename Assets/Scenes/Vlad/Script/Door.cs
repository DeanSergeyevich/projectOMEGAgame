using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private new HingeJoint hingeJoint;
    private JointMotor initialMotor;

    public float openSpeed = 100.0f;
    public float rotationSensitivity = 2.0f;
    public Camera playerCamera;
    public float interactionDistance = 3.0f; // Максимальное расстояние для взаимодействия с дверью
    private bool isInteracting = false; // Переменная для отслеживания состояния взаимодействия

    // Start is called before the first frame update
    void Start()
    {
        hingeJoint = GetComponent<HingeJoint>();
        initialMotor = hingeJoint.motor;
    }

    // Update is called once per frame
    void Update()
    {
        InteractDoor();
    }

    public void CheckInteractionDistance()
    {
        // Позиция начала луча (в данном случае, позиция игрока)
        Vector3 rayOrigin = playerCamera.transform.position;

        // Направление луча (например, вперед от игрока)
        Vector3 rayDirection = playerCamera.transform.forward;

        // Создаем луч
        Ray ray = new Ray(rayOrigin, rayDirection);

        // Переменная для хранения информации о столкновении с объектом
        RaycastHit hit;

        // Проводим луч и проверяем, есть ли столкновение с объектом
        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            // Проверяем, столкнулись ли с дверью
            if (hit.collider.gameObject == gameObject && Input.GetMouseButtonDown(0))
            {
                // Взаимодействие с дверью, так как игрок находится в нужной близости
                isInteracting = true;
            }
        }
    }

    public void InteractDoor()
    {
        // Если уже взаимодействуем с дверью, игнорируем выполнение
        if (isInteracting)
        {
            float mouseX = Input.GetAxis("Mouse X");

            JointMotor motor = hingeJoint.motor;
            motor.targetVelocity = -mouseX * openSpeed * rotationSensitivity;
            hingeJoint.motor = motor;

            playerCamera.GetComponent<CameraMovement>().enabled = false;

            if (Input.GetMouseButtonUp(0))
            {
                // Завершаем взаимодействие
                isInteracting = false;
                playerCamera.GetComponent<CameraMovement>().enabled = true;
            }
        }
        else
        {
            // Если еще не начали взаимодействие, проверяем расстояние
            CheckInteractionDistance();
        }
    }
}
