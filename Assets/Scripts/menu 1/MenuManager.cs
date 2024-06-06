using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject settingsPanel; // ������ �� ������ �������� � ����

    // ����� ��� ������ ����
    public void PlayGame() 
    {
        // �������� ����� "Game"
        SceneManager.LoadScene("Game");
    }

    // ����� ��� ������ �� ����
    public void ExitGame() 
    {
        // ���������� ����������
        Application.Quit();

        Debug.Log("����� �� ����");
    }

    // ����� ��� �������� ������ ��������
    public void OpenSettings()
    {
        // ��������� ������ ��������
        settingsPanel.SetActive(true);

        Debug.Log("��������� �������");
    }

    // ����� ��� �������� ������ ��������
    public void Exit() 
    {
        // ����������� ������ ��������
        settingsPanel.SetActive(false);
    }


}
