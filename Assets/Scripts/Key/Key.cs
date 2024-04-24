using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public GameObject keyObject;
    private bool isPickedUp = false;

    void Update()
    {
        // ���������, ������� �� ����� �� ���� � ��������� �� �� ����� � ���
        if (!isPickedUp && Input.GetKeyDown(KeyCode.E) && IsPlayerLookingAtKey() && IsPlayerNearKey())
        {
            isPickedUp = true;
            keyObject.SetActive(false);
            Debug.Log("Key picked up!");
        }
    }

    // ����� ��� ��������, ������� �� ����� �� ����
    private bool IsPlayerLookingAtKey()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit))
        {
            if (hit.collider.gameObject == keyObject)
            {
                return true;
            }
        }
        return false;
    }

    // ����� ��� ��������, ��������� �� ����� ����� � ������
    private bool IsPlayerNearKey()
    {
        float distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        return distance < 2f; //  ����������, ������� ��������� "����� � ������"
    }

    public bool IsPickedUp()
    {
        return isPickedUp;
    }
}

