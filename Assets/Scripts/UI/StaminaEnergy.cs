using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaEnergy : MonoBehaviour
{
    [Header("Stamina Parameters")]
    public float playerStamina = 100.0f;
    [SerializeField] internal float maxStamina = 100.0f;
    internal float max = 100f; // дополнительный фиксируемый размер стамины 

    [Header("Stamina UI Elements")]
    [SerializeField] private Image staminaProgressUI = null;

    void OnEnable()
    {
        // Подписываемся на событие из скрипта Hunger.
        StaminaHunger.OnHungerIncreased += HandleHungerIncreased;
    }

    void OnDisable()
    {
        // Отписываемся от события при отключении скрипта.
        StaminaHunger.OnHungerIncreased -= HandleHungerIncreased;
    }

    void HandleHungerIncreased(float hungerIncreaseAmount)
    {
        // Уменьшаем выносливость на основе увеличения голода.
        maxStamina -= hungerIncreaseAmount;
        playerStamina -= hungerIncreaseAmount;

        // Обновляем полосу выносливости.
        UpdateStamina();
    }

    void UpdateStamina()
    {
        staminaProgressUI.fillAmount = playerStamina / max;
    }
}
