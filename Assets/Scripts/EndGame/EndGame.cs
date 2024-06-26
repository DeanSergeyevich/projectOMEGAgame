using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    public GameObject endgame;

    void Start()
    {
        // ������������� � ����������� ������� ��� �������� ����� ����� ����
        UnlockCursor();
    }

    // ����� ��� ������ �� ����
    public void ExitInMenu()
    {
        // ����� � ����
        Debug.Log("����� � ����");
        SceneManager.LoadScene("Menu");
        UnlockCursor();
        
    }

    // ����� ��� ������������� � ����������� �������
    private void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
