using UnityEngine;

public class RotateObjectScript : MonoBehaviour
{
    private GameObject heldObject;
    private bool isRightMouseButtonDown = false;
    private Vector3 initialCameraPosition;
    private Quaternion initialCameraRotation;
    private bool isGamePaused = false; // Флаг, чтобы определить, заморожена ли игра

    void Update()
    {
        // Проверка удерживания правой кнопки мыши
        if (Input.GetMouseButton(1) && heldObject != null)
        {
            if (!isRightMouseButtonDown)
            {
                // Если кнопка только что была нажата, сохраняем начальное положение и ориентацию камеры
                initialCameraPosition = Camera.main.transform.position;
                initialCameraRotation = Camera.main.transform.rotation;

                // Если у нас есть heldObject, центрируем его перед камерой
                if (heldObject != null)
                {
                    heldObject.GetComponent<Rigidbody>().useGravity = false;
                    Vector3 playerPosition = Camera.main.transform.position + Camera.main.transform.forward * 2.0f;
                    heldObject.transform.position = playerPosition;
                }

                // Замораживаем игру
                Time.timeScale = isGamePaused ? 1f : 0f;
                isGamePaused = !isGamePaused;
            }

            isRightMouseButtonDown = true;

            if (heldObject != null)
            {
                // Вращаем только объект, не влияя на положение камеры
                float rotationSpeed = 5f;
                float mouseX = Input.GetAxis("Mouse X");
                float mouseY = Input.GetAxis("Mouse Y");

                heldObject.transform.Rotate(Vector3.up * mouseX * rotationSpeed, Space.World);
                heldObject.transform.Rotate(Vector3.left * mouseY * rotationSpeed, Space.World);
            }
        }
        else
        {
            // Если кнопка отпущена, сбрасываем флаг и возобновляем игру
            isRightMouseButtonDown = false;
            Time.timeScale = 1f;
            isGamePaused = false;
           // heldObject = null;
        }
    }

    // Метод для установки heldObject извне (GravityGun)
    public void SetHeldObject(GameObject newHeldObject)
    {
        heldObject = newHeldObject;
    }
}
