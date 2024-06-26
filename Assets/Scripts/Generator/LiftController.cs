using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class LiftController : MonoBehaviour
{

    public GameObject settingsPanel; // ������ �� ������ �������� � ����
    private bool generatorActivated = false;

    void OnTriggerEnter(Collider other)
    {
        if (generatorActivated && other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            LoadNextLevel();
        }
    }

    // ����� ��� ��������� �����
    public void ActivateLift()
    {
        generatorActivated = true;
        Debug.Log("���� ����������� � ����� � �������������!");
    }

    // ����� ��� �������� ��������� �����
    private void LoadNextLevel()
    {
        Debug.Log("�������� EndGame...");
        UnlockCursor();
        SceneManager.LoadScene("EndGame");
    }
   
    // ����� ��� ������������� � ����������� �������
    private void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
