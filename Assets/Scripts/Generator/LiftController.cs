using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LiftController : MonoBehaviour
{
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
        Debug.Log("�������� ���������� ������...");
        SceneManager.LoadScene("Game");
    }
}
