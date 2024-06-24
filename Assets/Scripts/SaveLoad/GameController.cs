using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public SaveLoadManager saveLoadManager; // ������ �� ��������� ���������� ����������� � ���������

    void Start()
    {
        // ������ �������� ���� ��� ������
        saveLoadManager.LoadGame();
    }

    void Update()
    {
        // ������� ��������� ���� ��� ������� ������� K
        if (Input.GetKeyDown(KeyCode.K))
        {
            saveLoadManager.SaveGame();
        }

        // ������� ��������� ���� ��� ������� ������� L
        if (Input.GetKeyDown(KeyCode.L))
        {
            saveLoadManager.LoadGame();
        }
    }
}