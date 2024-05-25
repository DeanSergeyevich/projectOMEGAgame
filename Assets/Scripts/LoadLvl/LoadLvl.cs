using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLvl : MonoBehaviour
{
    public GameObject load_lvl;
    public KeyCode enterloadlvl = KeyCode.E; // ������� ��� �������� ������ ������

    public LayerMask loadLvl; // ���� ��� �������� �������������� � ������� �� �������
    public float interactionDistance = 2f; // ��������� �������������� � ��������� �� �������
    public Camera playerCamera; // ������ ������

   
    void Update()
    {
        if (Input.GetKeyDown(enterloadlvl) && CanEnterLvl())
        {
            SceneManager.LoadScene("lvl2");
        }
    }


    private bool CanEnterLvl()
    {
        // ������� ��� �� ������ ������ ������ � �����������, ���� ������� ������
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        // ���������, ����� �� ��� � ���� �������������� � ���������
        if (Physics.Raycast(ray, out hit, interactionDistance, loadLvl))
        {
            Debug.Log("Raycast hit: " + hit.collider.name);
            return true;
        }

        Debug.Log("Raycast �� �������� �� ���� ������ � ���� ��������������.");
        return false;
    }
}
