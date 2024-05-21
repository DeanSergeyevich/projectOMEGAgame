using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaEnergy : MonoBehaviour
{
    [Header("Stamina Parameters")]
    public float playerStamina = 100.0f; // Текущий уровень выносливости игрока.
    [SerializeField] internal float maxStamina = 100.0f; // Максимально возможный уровень выносливости.
    internal float max = 100f; // дополнительный фиксируемый размер стамины 

    [Header("Stamina UI Elements")]
    [SerializeField] private Image staminaProgressUI = null; // Ссылка на UI элемент для отображения прогресса выносливости.

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
        maxStamina -= hungerIncreaseAmount; // Уменьшаем максимальную выносливость
        playerStamina -= hungerIncreaseAmount; // Уменьшаем текущую выносливость.

        // Обновляем полосу выносливости.
        UpdateStamina();
    }

    void UpdateStamina()
    {
        // Обновляем заполняемость UI элемента в соответствии с текущим уровнем выносливости.
        staminaProgressUI.fillAmount = playerStamina / max;
    }
}
