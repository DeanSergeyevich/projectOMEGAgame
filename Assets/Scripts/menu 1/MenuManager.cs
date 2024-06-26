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

    // ����� ��� �������� ��������
    public void loadEducation()
    {
        // �������� ����� "EducationScene"
        SceneManager.LoadScene("EducationScene");
    }

    // ����� ��� ������ �� ����
    public void ExitGame() 
    {
        // ���������� ����������
        Application.Quit();

        Debug.Log("����� �� ����");
    }

    

    // ����� ��� �������� ������ ��������
    public void Exit() 
    {
        // ����������� ������ ��������
        settingsPanel.SetActive(false);
    }

    public void ExitInMenu()
    {
        // ����� � ����
        SceneManager.LoadScene("Menu");
        Debug.Log("����� � ����");
    }

    

 

}
