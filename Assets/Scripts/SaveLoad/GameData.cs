using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    // ���������������� ����
    public float mouseSensitivity;
    // �������� ��������
    public float speed;
    // ������ ������
    public float jumpHeight;
    // �������� ����
    public float runSpeed;
    // ������� ������������ ������
    public float playerStamina;
    // ������������ ������������ ������
    public float maxStamina;
    // ������� ������� ������ �������
    public float currentBattery;
    // ������� ������� ��������
    public float health;
    // ������� ������� ������
    public float playerHunger;
    // ������� ������
    public Vector3 playerPosition;
    // ������� ������
    public Vector3 playerRotation;
    // ������� ������
    public Vector3 cameraPosition;
    public Vector3 cameraRotation;
    public Vector3 playerViewDirection;

    // ������ �����
    public Vector3 enemyPosition;
    public Vector3 enemyRotation;

    public GeneratorData[] generators; // ������ ������ � �����������
    public bool[] canisters; // ������ ������ � ��������� ������� (����� ��� ���)
}

// ����� ��� �������� ������ � �����������
[System.Serializable]
public class GeneratorData
{
    public int insertedCanistersCount; // ���������� ����������� �������
}
