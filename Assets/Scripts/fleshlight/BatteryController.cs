using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BatteryController : MonoBehaviour
{
    // Начальный заряд батареи
    public float startingBattery = 100f;

    // Скорость уменьшения заряда в секунду при включенном фонарике
    public float drainRate = 0.5f;

    // Ссылка на компонент света фонарика
    public Light flashlight;

    // Ссылка на компонент изображения для отображения уровня заряда
    public Image batteryIndicator;

    // Текущий уровень заряда
    private float currentBattery;

    private void Start()
    {
        currentBattery = startingBattery;
        UpdateBatteryUI();
        // Начать уменьшение заряда батареи
        InvokeRepeating("DrainBattery", 1f, 1f);
    }

    private void UpdateBatteryUI()
    {
        // Определяем fillAmount на основе текущего уровня заряда батареи
        float fillAmount = currentBattery / startingBattery;

        // Устанавливаем fillAmount для отображения уровня заряда
        if (batteryIndicator != null)
        {
            batteryIndicator.fillAmount = fillAmount;
        }
    }

    private void DrainBattery()
    {
        // Если фонарик включен и уровень заряда больше 0, уменьшаем заряд батареи
        if (flashlight.enabled && currentBattery > 0f)
        {
            currentBattery -= drainRate;
            UpdateBatteryUI();

            // Проверяем, достиг ли заряд нуля
            if (currentBattery <= 0f)
            {
                // Отключаем фонарь
                flashlight.enabled = false;
                // Выводим сообщение об этом
                Debug.Log("Battery drained! Flashlight turned off.");
            }
        }
    }

    // Метод для включения фонарика
    public void TurnOnFlashlight()
    {
        // Включаем фонарик только если он выключен и у нас есть заряд в батарейке
        if (!flashlight.enabled && currentBattery > 0f)
        {
            flashlight.enabled = true;
        }
    }

    // Метод для проверки, пуста ли батарея
    public bool IsBatteryNotEmpty()
    {
        return currentBattery > 0f;
    }

    // Метод для перезарядки батареи
    public void RechargeBattery(float amount)
    {
        currentBattery = Mathf.Min(currentBattery + amount, startingBattery);
        UpdateBatteryUI();
    }
}
