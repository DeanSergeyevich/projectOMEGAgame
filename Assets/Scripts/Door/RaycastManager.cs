using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RaycastManager : MonoBehaviour
{

    public float interactionDistance; // Расстояние, на котором будет срабатывать взаимодействие
    public RaycastHit hit; // Структура для хранения информации о результате Raycast

    
    void Start()
    {

    }

    void Update()
    {
        RaycastHit(); // Выполняем Raycast в каждом кадре
    }

    
    public void RaycastHit()
    {
        // Создаем луч, исходящий из позиции объекта в направлении вперед
        Ray ray = new Ray(transform.position, transform.forward); 
        // Отображаем луч в редакторе Unity для визуализации
        Debug.DrawRay(transform.position, transform.forward * interactionDistance, Color.yellow);

        // Выполняем Raycast и проверяем, попал ли луч в какой-либо объект
        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
           
        }
        
    }
    
}
