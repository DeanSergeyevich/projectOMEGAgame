using UnityEngine;

public class GravityGun : MonoBehaviour
{
    public GameObject heldObject; // Ссылка на удерживаемый объект
    private Transform attractor; // Ссылка на объект-притягиватель
    public float pickupDistance = 1.0f; // Расстояние для захвата объекта

    
    private Vector3 initialCameraPosition; // Начальная позиция камеры
    private Quaternion initialCameraRotation; // Начальная ротация камеры

    // Ссылка на скрипт для вращения объектов
    private RotateObjectScript rotateObjectScript;


    void Start()
    {
        // Найти объект-притягиватель в сцене
        attractor = GameObject.Find("GravityAttractor").transform;

        // Сохранить начальную позицию и ротацию камеры
        initialCameraPosition = Camera.main.transform.position;
        initialCameraRotation = Camera.main.transform.rotation;

        // Получить компонент RotateObjectScript на том же объекте
        rotateObjectScript = GetComponent<RotateObjectScript>();
    }

    public void Update()
    {
        // Проверка нажатия на левую кнопку мыши
        if (Input.GetMouseButtonDown(0))
        {
            if (heldObject == null)
            {
                // Создание луча из камеры в направлении курсора
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                // Проверка попадания луча в объект
                if (Physics.Raycast(ray, out hit, pickupDistance))
                {
                    // Проверка наличия Rigidbody и отсутствия HingeJoint
                    if (hit.collider.GetComponent<Rigidbody>() != null && !hit.collider.GetComponent<HingeJoint>())
                    {
                        // Установить удерживаемый объект и отключить гравитацию для него
                        heldObject = hit.collider.gameObject;
                        heldObject.GetComponent<Rigidbody>().useGravity = false;

                        // Установить новый удерживаемый объект для RotateObjectScript
                        rotateObjectScript.SetHeldObject(heldObject);
                    }
                }
            }
            else
            {
                // Включить гравитацию для удерживаемого объекта и сбросить ссылку на него
                heldObject.GetComponent<Rigidbody>().useGravity = true;
                heldObject = null;

                // Сбросить удерживаемый объект для RotateObjectScript (освободить объект)
                rotateObjectScript.SetHeldObject(null);
            }
            
        }

        // Проверка нажатия на среднюю кнопку мыши и наличие удерживаемого объекта
        if (Input.GetMouseButtonDown(2) && heldObject != null)
        {
            // Придать объекту импульс вперед и включить для него гравитацию
            Rigidbody rb = heldObject.GetComponent<Rigidbody>();
            rb.velocity = Camera.main.transform.forward * 10f;
            heldObject.GetComponent<Rigidbody>().useGravity = true;
            heldObject = null;

            // Сбросить удерживаемый объект для RotateObjectScript (освободить объект)
            rotateObjectScript.SetHeldObject(null);
        }

        // Если есть удерживаемый объект, плавно перемещать его к притягивателю
        if (heldObject != null)
        {
            Vector3 targetPosition = attractor.position;
            heldObject.transform.position = Vector3.Lerp(heldObject.transform.position, targetPosition, Time.deltaTime * 10f);
        }

    }

}
