using UnityEngine;

public class RotateObjectScript : MonoBehaviour
{
    private GameObject heldObject; // Ссылка на объект, который будет вращаться
    private bool isRightMouseButtonDown = false; // Флаг для отслеживания состояния правой кнопки мыши
    private Vector3 initialCameraPosition; // Начальная позиция камеры для сохранения
    private Quaternion initialCameraRotation; // Начальная ротация камеры для сохранения
    private bool isGamePaused = false; // Флаг для отслеживания состояния паузы игры

    void Update()
    {
        // Проверка, нажата ли правая кнопка мыши и установлен ли объект для вращения
        if (Input.GetMouseButton(1) && heldObject != null)
        {
            if (!isRightMouseButtonDown)
            {
                // Если правая кнопка мыши была нажата впервые, сохраняем начальные позиции и ротацию камеры
                initialCameraPosition = Camera.main.transform.position;
                initialCameraRotation = Camera.main.transform.rotation;

                // Если у нас есть heldObject, отключаем для него гравитацию и устанавливаем его перед камерой
                if (heldObject != null)
                {
                    heldObject.GetComponent<Rigidbody>().useGravity = false;
                    Vector3 playerPosition = Camera.main.transform.position + Camera.main.transform.forward * 2.0f;
                    heldObject.transform.position = playerPosition;
                }

                // Переключение состояния паузы игры
                Time.timeScale = isGamePaused ? 1f : 0f;
                isGamePaused = !isGamePaused;
            }

            isRightMouseButtonDown = true;

            if (heldObject != null)
            {
                // Вращаем объект в зависимости от движения мыши
                float rotationSpeed = 5f;
                float mouseX = Input.GetAxis("Mouse X");
                float mouseY = Input.GetAxis("Mouse Y");

                heldObject.transform.Rotate(Vector3.up * -mouseX * rotationSpeed, Space.World);
                heldObject.transform.Rotate(Vector3.left * -mouseY * rotationSpeed, Space.World);
            }
        }
        else if(isRightMouseButtonDown)
        {
            // Если правая кнопка мыши была отпущена, снимаем паузу и сбрасываем флаг
            isRightMouseButtonDown = false;
            Time.timeScale = 1f;
            isGamePaused = false;
          
        }
    }

    // Метод для установки нового объекта, который будет удерживаться и вращаться
    public void SetHeldObject(GameObject newHeldObject)
    {
        heldObject = newHeldObject;
    }
}
