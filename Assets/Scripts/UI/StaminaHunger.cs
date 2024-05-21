using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaHunger : MonoBehaviour
{
    public float hungerIncreaseRate = 1.0f; // Скорость увеличения голода в единицах в секунду.
    private float playerHungerLastFrame; // Добавляем переменную для хранения значения голода на предыдущем кадре.


    [Header("Stamina Parameters")]
    public float playerHunger = 100.0f; // Текущий уровень голода игрока.
    [SerializeField] private float maxHunger = 100.0f; // Максимально возможный уровень голода.

    [Header("Stamina UI Elements")]
    [SerializeField] private Image hungerProgressUI = null; // Ссылка на UI элемент для отображения прогресса голода.

    // Создаем событие, которое будет отправляться при увеличении голода.
    public delegate void HungerIncreased(float hungerIncreaseAmount); 
    public static event HungerIncreased OnHungerIncreased;

    void Update()
    {
        IncreaseHunger(); // Увеличиваем голод игрока.
        UpdateStamina(); // Обновляем отображение уровня голода на UI.
    }

    void IncreaseHunger()
    {
        playerHunger += hungerIncreaseRate * Time.deltaTime; // Увеличиваем голод в зависимости от времени, прошедшего с последнего кадра.
        playerHunger = Mathf.Clamp(playerHunger, 0.0f, maxHunger); // Ограничиваем уровень голода в пределах от 0 до максимума.

        // Отправляем событие, передавая сколько голода было увеличено.
        if (OnHungerIncreased != null)
        {
            OnHungerIncreased(playerHunger - playerHungerLastFrame); // Вызываем событие с количеством увеличенного голода.
        }

        playerHungerLastFrame = playerHunger; // Сохраняем текущее значение голода для использования в следующем кадре.
    }

    void UpdateStamina()
    {
        hungerProgressUI.fillAmount = playerHunger / maxHunger; // Обновляем заполняемость UI элемента в соответствии с текущим уровнем голода.
    }
}
