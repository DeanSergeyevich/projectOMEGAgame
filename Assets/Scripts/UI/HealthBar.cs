using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class HealthBar : MonoBehaviour
{
    public Image bar;
    public float fill;
    public GameObject gameOverScreen;
    

    // Устанавливаем максимальное значение здоровья и текущее значение
    private void Start()
    {
        fill = 100f;
        gameOverScreen.SetActive(false); // Делаем экран окончания игры неактивным при запуске
    }

    // Устанавливаем текущее значение здоровья
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            fill -= 1;
            if (fill <= 0)
            {
                EndGame(); // Вызываем метод завершения игры, если здоровье меньше или равно нулю
            }
        }
        bar.fillAmount = fill/100f; 
    }

    void EndGame()
    {
        // Здесь вы можете добавить дополнительную логику, связанную с завершением игры
        gameOverScreen.SetActive(true); // Показываем экран окончания игры
        Time.timeScale = 0f; // Приостанавливаем время (игру)
    }
}