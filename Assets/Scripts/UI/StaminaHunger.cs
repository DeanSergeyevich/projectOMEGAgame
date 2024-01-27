using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaHunger : MonoBehaviour
{
    public float hungerIncreaseRate = 1.0f; // Скорость увеличения голода в единицах в секунду.
    private float playerHungerLastFrame; // Добавляем переменную для хранения значения голода на предыдущем кадре.


    [Header("Stamina Parameters")]
    public float playerHunger = 100.0f;
    [SerializeField] private float maxHunger = 100.0f;

    [Header("Stamina UI Elements")]
    [SerializeField] private Image hungerProgressUI = null;

    // Создаем событие, которое будет отправляться при увеличении голода.
    public delegate void HungerIncreased(float hungerIncreaseAmount);
    public static event HungerIncreased OnHungerIncreased;

    void Update()
    {
        IncreaseHunger();
        UpdateStamina();
    }

    void IncreaseHunger()
    {
        playerHunger += hungerIncreaseRate * Time.deltaTime;
        playerHunger = Mathf.Clamp(playerHunger, 0.0f, maxHunger);

        // Отправляем событие, передавая сколько голода было увеличено.
        if (OnHungerIncreased != null)
        {
            OnHungerIncreased(playerHunger - playerHungerLastFrame);
        }

        playerHungerLastFrame = playerHunger;
    }

    void UpdateStamina()
    {
        hungerProgressUI.fillAmount = playerHunger / maxHunger;
    }
}
