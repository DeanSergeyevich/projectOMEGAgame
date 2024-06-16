using UnityEngine;
using System.Collections.Generic;

public class GeneratorController : MonoBehaviour
{
    public int requiredCanisters = 4;
    public List<GameObject> canisters = new List<GameObject>(); // Список всех канистр в сцене
    private List<GameObject> insertedCanisters = new List<GameObject>(); // Список вставленных канистр
    public GameObject lift;

    private void Start()
    {
        // Находим все канистры в сцене и добавляем их в список
        GameObject[] allCanisters = GameObject.FindGameObjectsWithTag("Canister");
        foreach (GameObject canister in allCanisters)
        {
            canisters.Add(canister);
        }
    }

    private void Update()
    {
        // Проверяем, если кнопка E нажата и курсор наведен на генератор
        if (Input.GetKeyDown(KeyCode.E) && IsPlayerLookingAtGenerator())
        {
            InsertCanister(gameObject); // Вставляем канистру в генератор
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

    // Метод для вставки канистры в генератор
    public void InsertCanister(GameObject gameObject)
    {
        // Проверяем, что есть канистры для вставки
        if (canisters.Count > 0)
        {
            GameObject canister = canisters[0]; // Берем первую доступную канистру

            if (!insertedCanisters.Contains(canister))
            {
                insertedCanisters.Add(canister); // Добавляем канистру в список вставленных
                canisters.Remove(canister); // Удаляем канистру из списка всех канистр

                Debug.Log("Канистра вставлена. Текущее количество: " + insertedCanisters.Count);

                // Если количество вставленных канистр достигло необходимого значения
                if (insertedCanisters.Count == requiredCanisters)
                {
                    Debug.Log("Генератор полностью заправлен! Запускаем...");
                    ActivateGenerator(); // Вызываем метод активации генератора
                }
            }
            else
            {
                Debug.LogWarning("Канистра уже вставлена в генератор!");
            }
        }
        else
        {
            Debug.LogWarning("Нет доступных канистр для вставки!");
        }
    }

    // Метод для активации генератора и запуска лифта
    private void ActivateGenerator()
    {
        // Активация генератора
        Debug.Log("Генератор активирован!");

        // Проверяем, что ссылка на лифт установлена
        if (lift != null)
        {
            lift.SetActive(true); // Активируем лифт
            Debug.Log("Лифт активирован!");
        }
        else
        {
            Debug.LogError("Ссылка на лифт не установлена у генератора!");
            return;
        }

        // Выполняем переход на новый уровень (в следующую сцену)
        LoadNextLevel();
    }

    // Метод для загрузки следующей сцены
    private void LoadNextLevel()
    {
        // Здесь нужно добавить код для загрузки следующей сцены
    }
}