using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System;

public class HealthBar : MonoBehaviour
{
    public Image bar; // Ссылка на изображение полосы здоровья
    public float fill; // Заполнение полосы здоровья

    // Инициализация объекта при запуске сцены
    private void Start()
    {
        fill = 100f; // Установка начального значения заполнения
    }

    // Метод для изменения уровня здоровья
    public void ChangeHealth(float amount)
    {
        fill += amount; // Изменение заполнения
        fill = Mathf.Clamp(fill, 0, 100); // Ограничение значения между 0 и 100
        bar.fillAmount = fill / 100f; // Обновление отображения полосы здоровья

        if (fill <= 0)
        {
            RestartLevel(); // Вызов метода перезапуска уровня при достижении нулевого заполнения
        }
    }

    // Метод для перезапуска уровня
    void RestartLevel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex); // Перезагрузка текущей сцены
    }
}