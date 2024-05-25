using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class HealthBar : MonoBehaviour
{
    public Image bar; // Ссылка на изображение полосы здоровья
    public float fill; // Заполнение полосы здоровья
    public GameObject gameOverScreen; // Ссылка на экран окончания игры

    // Инициализация объекта при запуске сцены
    private void Start()
    {
        fill = 100f; // Установка начального значения заполнения
        gameOverScreen.SetActive(false); // Скрытие экрана окончания игры
    }

    // Обновление состояния объекта каждый кадр
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) // Проверка нажатия клавиши T
        {
            ChangeHealth(-1); // Уменьшение заполнения на единицу
        }
    }

    // Метод для изменения уровня здоровья
    public void ChangeHealth(float amount)
    {
        fill += amount; // Изменение заполнения
        fill = Mathf.Clamp(fill, 0, 100); // Ограничение значения между 0 и 100
        bar.fillAmount = fill / 100f; // Обновление отображения полосы здоровья

        if (fill <= 0)
        {
            EndGame(); // Вызов метода окончания игры при достижении нулевого заполнения
        }
    }

    // Метод для завершения игры
    void EndGame()
    {
        gameOverScreen.SetActive(true); // Отображение экрана окончания игры
        Time.timeScale = 0f; // Остановка времени (пауза)
    }
}