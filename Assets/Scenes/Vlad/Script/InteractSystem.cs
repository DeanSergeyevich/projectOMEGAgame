using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractSystem : MonoBehaviour
{
    [SerializeField] private Camera fpsCamera;
    private Ray ray;
    private RaycastHit hit;
    [SerializeField] private float maxDistanceRay;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        Ray();
        DrawRay();
        // Door();
        Meal();
    }

    private void Ray()
    {
        ray = fpsCamera.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
    }

    private void DrawRay()
    {
        if (Physics.Raycast(ray, out hit, maxDistanceRay))
        {
            Debug.DrawRay(ray.origin, ray.direction * maxDistanceRay, Color.blue);
        }

        //отрисовка луча если он не с чем не стлкнулся 
        if (hit.transform == null)
        {
            Debug.DrawRay(ray.origin, ray.direction * maxDistanceRay, Color.red);
        }

    }

    // private void Door()
    // {
    //     if (hit.transform != null && hit.transform.GetComponent<DoorControl>())
    //     {
    //         Debug.DrawRay(ray.origin, ray.direction * maxDistanceRay, Color.green);
    //         if (Input.GetMouseButton(0))
    //         {
    //             hit.transform.GetComponent<DoorControl>().InteractDoor();
    //             hit.transform.GetComponent<DoorControl>().Frozen();
    //         }
    //     }
    // }

    private void Meal()
    {
        if (hit.transform != null && hit.transform.GetComponent<Meal>())
        {
            Debug.DrawRay(ray.origin, ray.direction * maxDistanceRay, Color.green);
            if (Input.GetMouseButtonDown(0))
            {
                hit.transform.GetComponent<Meal>().Eda();
                // Уничтожаем объект, на который указывает hit.transform.
                Destroy(hit.transform.gameObject);
            }
        }
    }
}
