using UnityEngine;

public class ThrowObject : MonoBehaviour
{
    private GameObject heldObject; // Ссылка на поднимаемый объект, если есть
    private GravityGun Mini;

    private void Start()
    {
        Mini = GetComponent<GravityGun>();
    }


    void Update()
    {
        // Проверка нажатия на правую кнопку мыши
        if (Input.GetMouseButtonDown(1)) // && pickedObject != null)
        {
            // Бросок поднимаемого объекта
            Rigidbody rb = Mini.heldObject.GetComponent<Rigidbody>();
            // rb.isKinematic = false;
            // pickedObject.GetComponent<Rigidbody>().isKinematic = true;
            rb.velocity = Camera.main.transform.forward * 10f; // Измените скорость и направление броска по вашему усмотрению
            //pickedObject = null;
            Mini.heldObject.GetComponent<Rigidbody>().useGravity = true;
        }
    }

    
}
