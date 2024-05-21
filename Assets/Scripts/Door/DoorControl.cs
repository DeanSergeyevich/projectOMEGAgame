using UnityEngine;

public class DoorControl : MonoBehaviour
{
    private new HingeJoint hingeJoint; // Ссылка на компонент HingeJoint двери
    private JointMotor initialMotor; // Начальные настройки мотора для сочления двери
    private Rigidbody rb; // Ссылка на компонент Rigidbody двери

    public float openSpeed = 100.0f; // Скорость открытия двери
    public float rotationSensitivity = 2.0f; // Чувствительность к вращению двери
    public Camera playerCamera; // Камера, используемая для рейкастинга
    private bool isFrozen = false; // Флаг, указывающий, заморожена ли камера

    private void Start()
    {
        hingeJoint = GetComponent<HingeJoint>(); // Получение ссылки на компонент HingeJoint
        rb = GetComponent<Rigidbody>(); // Получение ссылки на компонент Rigidbody
        initialMotor = hingeJoint.motor; // Сохранение начальных настроек мотора
    }

    public void InteractDoor()
    {
        float mouseX = Input.GetAxis("Mouse X"); // Получение значения оси X мыши

        JointMotor motor = hingeJoint.motor; // Получение текущего мотора двери
        motor.targetVelocity = -mouseX * openSpeed * rotationSensitivity; // Установка целевой скорости мотора
        hingeJoint.motor = motor; // Применение изменений к мотору

        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition); // Создание луча из позиции мыши
        RaycastHit hit;

        // Выполняем Raycast с максимальным расстоянием maxRaycastDistance
        if (Physics.Raycast(ray, out hit))
        {
            GameObject hitObject = hit.collider.gameObject;

            // Проверяем тег объекта, с которым столкнулся луч
            if (hitObject.CompareTag("FreezeCamera"))
            {
                isFrozen = !isFrozen; // Инвертируем значение флага заморозки
                playerCamera.GetComponent<CameraMovement>().enabled = !isFrozen; // Включаем/выключаем движение камеры
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
            hingeJoint.motor = initialMotor; // Если луч не столкнулся с объектом, восстанавливаем начальный мотор
        }
    }

    public void Frozen()
    {
        isFrozen = false; // Снимаем заморозку камеры
        playerCamera.GetComponent<CameraMovement>().enabled = true; // Включаем движение камеры
    }
}