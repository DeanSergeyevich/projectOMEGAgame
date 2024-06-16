using UnityEngine;

public class CanisterController : MonoBehaviour
{
    private static bool isAnyCanisterTaken = false; // Переменная для отслеживания, взята ли хотя бы одна канистра
    public GameObject generator; // Ссылка на объект генератора

    private void Update()
    {
        // Проверяем, если кнопка E нажата и курсор наведен на канистру
        if (Input.GetKeyDown(KeyCode.E) && IsPlayerLookingAt() && !isAnyCanisterTaken)
        {
            TakeCanister(); // Берем канистру
        }
    }

    // Метод для проверки, смотрит ли игрок на канистру
    private bool IsPlayerLookingAt()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Проверяем, попал ли луч в коллайдер канистры и она не взята ли она уже игроком
        if (Physics.Raycast(ray, out hit) && hit.collider == GetComponent<Collider>() && !isAnyCanisterTaken)
        {
            return true;
        }
        return false;
    }

    // Метод для взятия канистры игроком
    private void TakeCanister()
    {
        isAnyCanisterTaken = true; // Устанавливаем флаг, что хотя бы одна канистра взята
        Debug.Log("Канистра взята!");

        // Отключаем объект канистры или скрываем его (в данном случае отключаем активацию объекта)
        gameObject.SetActive(false);

        // Уведомляем генератор о вставке канистры
        if (generator != null)
        {
            generator.GetComponent<GeneratorController>().InsertCanister(gameObject); // Передаем объект канистры в метод InsertCanister
        }
        else
        {
            Debug.LogError("Ссылка на генератор не установлена у канистры!");
        }
    }

    // Метод для возвращения состояния взятия канистры
    public static void ResetCanisterState()
    {
        isAnyCanisterTaken = false; // Сбрасываем флаг при необходимости (например, при перезапуске уровня)
    }
}