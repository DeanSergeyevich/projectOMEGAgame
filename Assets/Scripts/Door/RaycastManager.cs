using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RaycastManager : MonoBehaviour
{

    public float interactionDistance; // ����������, �� ������� ����� ����������� ��������������
    public RaycastHit hit; // ��������� ��� �������� ���������� � ���������� Raycast

    
    void Start()
    {

    }

    void Update()
    {
        RaycastHit(); // ��������� Raycast � ������ �����
    }

    
    public void RaycastHit()
    {
        // ������� ���, ��������� �� ������� ������� � ����������� ������
        Ray ray = new Ray(transform.position, transform.forward); 
        // ���������� ��� � ��������� Unity ��� ������������
        Debug.DrawRay(transform.position, transform.forward * interactionDistance, Color.yellow);

        // ��������� Raycast � ���������, ����� �� ��� � �����-���� ������
        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
           
        }
        
    }
    
}
