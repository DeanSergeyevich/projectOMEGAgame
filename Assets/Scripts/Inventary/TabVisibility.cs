using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabVisibility : MonoBehaviour
{
    public GameObject UIInventoryTaskPlayer;
    private Animator animator;
    private bool isPanelVisible = false;
    private InventoryManager inventoryManager; // Ссылка на скрипт управления инвентарем

    // Start вызывается перед обновлением первого кадра
    void Start()
    {
        animator = UIInventoryTaskPlayer.GetComponent<Animator>();
        inventoryManager = FindObjectOfType<InventoryManager>(); // Получаем ссылку на скрипт управления инвентарем
    }

    // Обновление вызывается один раз для каждого кадра
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            // Переключаем значение isVisible и запускаем соответствующую анимацию
            isPanelVisible = !isPanelVisible;
            animator.SetBool("isVisible", isPanelVisible);
        }
    }

    // Метод для вызова из других скриптов для переключения видимости инвентаря
    public void ToggleInventoryVisibility()
    {
        isPanelVisible = !isPanelVisible;
        animator.SetBool("isVisible", isPanelVisible);
    }
}