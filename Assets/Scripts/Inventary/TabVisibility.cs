using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabVisibility : MonoBehaviour
{
    public GameObject UIInventoryTaskPlayer; // ������, ���������� ��������� ���������
    public Animator animator; // �������� ��� ���������� ��������� ���������
    private bool isPanelVisible = false; // ����, ������������, ����� �� ���������
    public InventoryUI inventoryUI; // ������ �� ��������� ���������
    public Inventory inventory; // ������ �� ���������
    public float pauseDelay = 0.1f; // �������� ����� ������, ����� ��������� ����� ����������

    // Start ���������� ����� ������ ������
    void Start()
    {
        // ���������, ��� ��������� Animator ������� ���������
        if (UIInventoryTaskPlayer != null)
        {
            animator = UIInventoryTaskPlayer.GetComponent<Animator>();
        }

        if (animator == null)
        {
            Debug.LogError("Animator component not found on UIInventoryTaskPlayer.");
        }
    }

    // Update ���������� ���� ��� �� ����
    void Update()
    {
        // ���������, ���� �� ������ ������� TAB
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            // �������� ����� ��� ������������ ��������� ���������
            ToggleInventoryVisibility();
        }
    }

    // ����� ��� ������������ ��������� ���������
    public void ToggleInventoryVisibility()
    {
        // ����������� �������� ����� ���������
        isPanelVisible = !isPanelVisible;
        Debug.Log("Toggling Inventory Visibility. New state: " + isPanelVisible);

        // ���� ��������� ������ ����� �������
        if (isPanelVisible)
        {
            Debug.Log("Opening Inventory");
            animator.SetBool("isVisible", true); // ��������� �������� ��������
            ShowCursor(); // ���������� ������
            UpdateInventoryUI(); // ��������� ��������� ���������
            StartCoroutine(PauseAfterDelay(pauseDelay)); // �������� �������� ��� ����� � ���������
        }
        // ���� ��������� ������ ����� ���������
        else
        {
            Debug.Log("Closing Inventory");
            animator.SetBool("isVisible", false); // ��������� �������� ��������
            ResumeGame(); // ������������ ����� � ����
            HideCursor(); // �������� ������
        }
    }

    // ����� ��� ���������� ���������� ���������
    private void UpdateInventoryUI()
    {
        if (inventoryUI != null && inventory != null)
        {
            // ��������� ��������� ���������, ��������� ��� ������� ������ ���������
            inventoryUI.UpdateInventoryUI(inventory.GetItemsList());
        }
    }

    // ����� ��� ��������� ������� � ���� ����� ��������
    private IEnumerator PauseAfterDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay); // ���� ��������� ���������� ������ ��������� �������
        PauseGame(); // ������������� �����
    }

    // ����� ��� ������������� ������� � ���� ����� ��������
    private IEnumerator ResumeAfterDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay); // ���� ��������� ���������� ������ ��������� �������
        ResumeGame(); // ������������ �����
    }

    // ����� ��� ��������� ������� � ����
    private void PauseGame()
    {
        Time.timeScale = 0f; // ������������� �����
    }

    // ����� ��� ������������� ������� � ����
    private void ResumeGame()
    {
        Time.timeScale = 1f; // ������������ �����
    }

    // ����� ��� ����������� �������
    private void ShowCursor()
    {
        Cursor.lockState = CursorLockMode.None; // ������������ ������
        Cursor.visible = true; // ������ ������ �������
    }

    // ����� ��� ������� �������
    private void HideCursor()
    {
        Cursor.lockState = CursorLockMode.Locked; // ��������� ������
        Cursor.visible = false; // ������ ������ ���������
    }
}