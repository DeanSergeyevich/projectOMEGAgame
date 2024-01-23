using UnityEngine;

public class GravityGun : MonoBehaviour
{
    public GameObject heldObject;
    private Transform attractor; // Пустой объект, к которому притягиваются предметы
    public float pickupDistance = 1.0f; // Расстояние для подхвата объекта

    private bool isRightMouseButtonDown = false; // переменная для отслеживания состояния зажатой ПКМ

    // переменная для хранения начальной позиции камеры и начальной ориентации камеры
    private Vector3 initialCameraPosition;
    private Quaternion initialCameraRotation;


    void Start()
    {
        // Найдите и назначьте пустой объект "GravityAttractor"
        attractor = GameObject.Find("GravityAttractor").transform;

        // Сохраняем начальную позицию и ориентацию камеры
        initialCameraPosition = Camera.main.transform.position;
        initialCameraRotation = Camera.main.transform.rotation;
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (heldObject == null)
            {
                // Попытка подхватить объект
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, pickupDistance)) // Указываем pickupDistance как максимальное расстояние
                {
                    if (hit.collider.GetComponent<Rigidbody>() != null)
                    {
                        // Проверяем, не является ли объект дверной петлей
                        if (!hit.collider.GetComponent<HingeJoint>())
                        {
                            heldObject = hit.collider.gameObject;

                            // Отключаем гравитацию у подхваченного объекта
                            heldObject.GetComponent<Rigidbody>().useGravity = false;
                        }
                    }
                }
            }
            else
            {
                // Отпускаем поднимаемый объект
                heldObject.GetComponent<Rigidbody>().useGravity = true;
                heldObject = null;
            }

        }

        // Проверка нажатия на колёсико мыши
        if (Input.GetMouseButtonDown(2) && heldObject != null)
        {
            // Бросок поднимаемого объекта
            Rigidbody rb = heldObject.GetComponent<Rigidbody>();
            rb.velocity = Camera.main.transform.forward * 10f; // Измените скорость и направление броска по вашему усмотрению
            heldObject.GetComponent<Rigidbody>().useGravity = true;
            heldObject = null; // Убираем ссылку на поднимаемый объект, так как он был брошен
        }

        if (heldObject != null)
        {
            // Притягивание объекта к пустому объекту "GravityAttractor"
            Vector3 targetPosition = attractor.position;
            heldObject.transform.position = Vector3.Lerp(heldObject.transform.position, targetPosition, Time.deltaTime * 10f);
        }

        // Проверка нажатия на ПКМ
        if (Input.GetMouseButtonDown(1))
        {
            isRightMouseButtonDown = true;
            initialCameraPosition = Camera.main.transform.position;
            initialCameraRotation = Camera.main.transform.rotation;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            isRightMouseButtonDown = false;
        }

        if (heldObject != null)
        {
            if (isRightMouseButtonDown)
            {
                // Замораживаем положение и ориентацию камеры
                Camera.main.transform.position = initialCameraPosition;
                Camera.main.transform.rotation = initialCameraRotation;

                // Выравниваем позицию объекта по центру экрана перед игроком
                Vector3 playerPosition = Camera.main.transform.position + Camera.main.transform.forward * 2.0f;
                heldObject.transform.position = playerPosition;

                // Вращение объекта (перемещение объекта в пространстве)
                float rotationSpeed = 5f; // Настройте скорость вращения
                float mouseX = Input.GetAxis("Mouse X");
                float mouseY = Input.GetAxis("Mouse Y");

                // Поворачиваем объект вокруг осей Y и X
                heldObject.transform.Rotate(Vector3.up * mouseX * rotationSpeed, Space.World);
                heldObject.transform.Rotate(Vector3.left * mouseY * rotationSpeed, Space.World);
            }
        }
    }
}
