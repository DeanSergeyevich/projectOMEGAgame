using UnityEngine;
using System.Collections.Generic;

public class ObjectWeightManager : MonoBehaviour
{
    [Tooltip("Список префабов, чьи массы будут получены.")]
    public List<GameObject> prefabsToCheck = new List<GameObject>();

    private void Start()
    {
        foreach (GameObject prefab in prefabsToCheck)
        {
            // Проверяем, что префаб не является null
            if (prefab != null)
            {
                // Получаем компонент Rigidbody на префабе
                Rigidbody prefabRigidbody = prefab.GetComponent<Rigidbody>();

                if (prefabRigidbody != null)
                {
                    // Получаем массу объекта из компонента Rigidbody
                    float objectMass = prefabRigidbody.mass;

                    // Используйте значение массы в вашей логике
                    Debug.Log("Масса префаба " + prefab.name + ": " + objectMass);
                }
                else
                {
                    // Если компонент Rigidbody отсутствует на префабе, обработайте это
                    Debug.LogWarning("Компонент Rigidbody отсутствует на префабе " + prefab.name);
                }
            }
            else
            {
                // Если префаб является null, обработайте это
                Debug.LogWarning("Префаб в списке равен null.");
            }
        }
    }

    // Метод для получения массы объекта по его имени
    public float GetObjectMass(GameObject obj)
    {
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            return rb.mass;
        }
        else
        {
            Debug.LogWarning("Компонент Rigidbody не найден на объекте: " + obj.name);
            return 0f;
        }
    }
}
