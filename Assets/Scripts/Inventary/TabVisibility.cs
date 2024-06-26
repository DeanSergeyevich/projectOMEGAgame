using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabVisibility : MonoBehaviour
{
    public GameObject UIInventoryTaskPlayer; // Объект, содержащий интерфейс инвентаря
    public Animator animator; // Аниматор для управления анимацией инвентаря
    private bool isPanelVisible = false; // Флаг, показывающий, видим ли инвентарь
    public InventoryUI inventoryUI; // Ссылка на интерфейс инвентаря
    public Inventory inventory; // Ссылка на инвентарь
    public float pauseDelay = 0.1f; // Задержка перед паузой, чтобы интерфейс успел обновиться

    // Start вызывается перед первым кадром
    void Start()
    {
        // Проверяем, что компонент Animator получен корректно
        if (UIInventoryTaskPlayer != null)
        {
            animator = UIInventoryTaskPlayer.GetComponent<Animator>();
        }

        if (animator == null)
        {
            Debug.LogError("Animator component not found on UIInventoryTaskPlayer.");
        }
    }

    // Update вызывается один раз за кадр
    void Update()
    {
        // Проверяем, была ли нажата клавиша TAB
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            // Вызываем метод для переключения видимости инвентаря
            ToggleInventoryVisibility();
        }
    }

    // Метод для переключения видимости инвентаря
    public void ToggleInventoryVisibility()
    {
        // Переключаем значение флага видимости
        isPanelVisible = !isPanelVisible;
        Debug.Log("Toggling Inventory Visibility. New state: " + isPanelVisible);

        // Если инвентарь должен стать видимым
        if (isPanelVisible)
        {
            Debug.Log("Opening Inventory");
            animator.SetBool("isVisible", true); // Запускаем анимацию открытия
            ShowCursor(); // Отображаем курсор
            UpdateInventoryUI(); // Обновляем интерфейс инвентаря
            StartCoroutine(PauseAfterDelay(pauseDelay)); // Начинаем корутину для паузы с задержкой
        }
        // Если инвентарь должен стать невидимым
        else
        {
            Debug.Log("Closing Inventory");
            animator.SetBool("isVisible", false); // Запускаем анимацию закрытия
            ResumeGame(); // Возобновляем время в игре
            HideCursor(); // Скрываем курсор
        }
    }

    // Метод для обновления интерфейса инвентаря
    private void UpdateInventoryUI()
    {
        if (inventoryUI != null && inventory != null)
        {
            // Обновляем интерфейс инвентаря, передавая ему текущий список предметов
            inventoryUI.UpdateInventoryUI(inventory.GetItemsList());
        }
    }

    // Метод для остановки времени в игре после задержки
    private IEnumerator PauseAfterDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay); // Ждем указанное количество секунд реального времени
        PauseGame(); // Останавливаем время
    }

    // Метод для возобновления времени в игре после задержки
    private IEnumerator ResumeAfterDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay); // Ждем указанное количество секунд реального времени
        ResumeGame(); // Возобновляем время
    }

    // Метод для остановки времени в игре
    private void PauseGame()
    {
        Time.timeScale = 0f; // Останавливаем время
    }

    // Метод для возобновления времени в игре
    private void ResumeGame()
    {
        Time.timeScale = 1f; // Возобновляем время
    }

    // Метод для отображения курсора
    private void ShowCursor()
    {
        Cursor.lockState = CursorLockMode.None; // Разблокируем курсор
        Cursor.visible = true; // Делаем курсор видимым
    }

    // Метод для скрытия курсора
    private void HideCursor()
    {
        Cursor.lockState = CursorLockMode.Locked; // Блокируем курсор
        Cursor.visible = false; // Делаем курсор невидимым
    }
}