using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pause_menu : MonoBehaviour
{
    public static bool GameIsPause = false; // ���� ��� ������������ ��������� �����
    public GameObject pauseMenuUI; // ������ �� UI �����
    public GameObject gameUI; // ������ �� �������� ������� UI

    void Start()
    {
        // ��������� ���������
        Resume();
    }

    // ���������� ���������� ���� ��� �� ����
    void Update()
    {
        // �������� ������� ������� Esc
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // ���� ���� �� �����, ������������ ����, ����� ����������������
            if (GameIsPause)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    // ����� ��� ������������� ����
    void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked; // ���������� �������
        pauseMenuUI.SetActive(false); // ������� ���� �����
        gameUI.SetActive(true); // ����������� ��������� UI
        Time.timeScale = 1f; // ��������� ����������� �������
        GameIsPause = false; // ������ ����� �����
    }

    // ����� ��� ������������ ����
    void Pause()
    {
        Cursor.lockState = CursorLockMode.None; // ������������� �������
        Cursor.visible = true; // ����������� �������
        pauseMenuUI.SetActive(true); // ����������� ���� �����
        gameUI.SetActive(false); // ������� ��������� UI
        Time.timeScale = 0f; // ��������� �������
        GameIsPause = true; // ��������� ����� �����
    }

    // ����� ��� ������ �� ����
    public void QuitGame()
    {

        SceneManager.LoadScene("Menu");
        Debug.Log("quit"); // ����� ��������� � �������
    }
}
