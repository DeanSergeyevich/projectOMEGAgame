using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GeneratorController : MonoBehaviour
{
    public int requiredCanisters = 4; // Количество требуемых канистр для запуска генератора
    private List<GameObject> insertedCanisters = new List<GameObject>(); // Список вставленных канистр
    public GameObject lift; // Ссылка на лифт

    private GameObject carriedCanister = null; // Текущая поднятая канистра

    void Update()
    {
        // Проверяем, если кнопка E нажата и курсор наведен на генератор
        if (Input.GetKeyDown(KeyCode.E) && IsPlayerLookingAtGenerator() && carriedCanister != null)
        {
            InsertCanister(); // Вставляем канистру в генератор
        }
    }

    // Метод для проверки, смотрит ли игрок на генератор
    private bool IsPlayerLookingAtGenerator()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Проверяем, попал ли луч в коллайдер генератора
        if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Generator"))
        {
            return true;
        }
        return false;
    }

    // Метод для поднятия канистры игроком
    public void PickUpCanister(GameObject canister)
    {
        carriedCanister = canister;
    }

    // Метод для вставки канистры в генератор
    public void InsertCanister()
    {
        if (carriedCanister != null)
        {
            insertedCanisters.Add(carriedCanister); // Добавляем канистру в список вставленных
            carriedCanister = null; // Сбрасываем текущую поднятую канистру

            Debug.Log("Канистра вставлена. Текущее количество: " + insertedCanisters.Count);

            // Сбрасываем состояние канистры
            CanisterController.ResetCanisterState();

            // Если количество вставленных канистр достигло необходимого значения
            if (insertedCanisters.Count == requiredCanisters)
            {
                Debug.Log("Генератор полностью заправлен! Запускаем...");
                ActivateGenerator(); // Вызываем метод активации генератора
            }
        }
    }

    // Метод для активации генератора и запуска лифта
    private void ActivateGenerator()
    {
        Debug.Log("Генератор активирован!");

        // Проверяем, что ссылка на лифт установлена
        if (lift != null)
        {
            lift.GetComponent<LiftController>().ActivateLift(); // Активируем лифт
            Debug.Log("Лифт активирован!");
        }
        else
        {
            Debug.LogError("Ссылка на лифт не установлена у генератора!");
        }
    }

    // Метод для получения количества вставленных канистр
    public int GetInsertedCanistersCount()
    {
        return insertedCanisters.Count;
    }

    // Метод для установки количества вставленных канистр
    public void SetInsertedCanisters(int count)
    {
        insertedCanisters.Clear(); // Очистка текущего списка канистр
        for (int i = 0; i < count; i++)
        {
            // Воссоздайте или активируйте канистры и добавьте их в список insertedCanisters
            GameObject newCanister = new GameObject("Canister" + (i + 1)); // Создайте новый объект канистры или получите существующий
            insertedCanisters.Add(newCanister); // Добавьте его в список
        }

        Debug.Log("Генератор восстановлен с " + count + " канистрами.");
    }

    // Метод для сброса состояния генератора
    public void ResetGenerator()
    {
        insertedCanisters.Clear();
    }
}