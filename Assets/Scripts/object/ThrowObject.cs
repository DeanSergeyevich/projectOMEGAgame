using UnityEngine;

public class ThrowObject : MonoBehaviour
{
    private GameObject heldObject; // Ссылка на поднимаемый объект, если есть
    private GravityGun Mini; // Ссылка на компонент GravityGun.

    private void Start()
    {
        // Получаем компонент GravityGun при старте.
        Mini = GetComponent<GravityGun>();
    }


    void Update()
    {
        // Проверка нажатия на правую кнопку мыши
        if (Input.GetMouseButtonDown(1))
        {
            // Бросок поднимаемого объекта
            Rigidbody rb = Mini.heldObject.GetComponent<Rigidbody>(); // Получение Rigidbody поднимаемого объекта.
            rb.velocity = Camera.main.transform.forward * 10f; // Измените скорость и направление броска по вашему усмотрению          
            Mini.heldObject.GetComponent<Rigidbody>().useGravity = true; // Включение гравитации для поднимаемого объекта.
        }
    }

    
}
