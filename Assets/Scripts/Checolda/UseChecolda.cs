using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseChecolda : MonoBehaviour
{

    public GameObject checolda;
    public bool isChecoldaUsed = false;
    public Animator Checolda;
    public Camera playerCamera; // ������ ������
    public LayerMask interactChecolda;
    public float interactionDistance = 2f;
    public float maxAngle = 30f; // ������������ ���� ����� ������������ ������� � ������������ � �������

    private void Start()
    {
        Checolda = GetComponent<Animator>();
        if(Checolda == null) 
        {
            Debug.LogError("Animator �� ������!");
        }
        
    }


    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E) && CanOpenChecolda())
        {
            bool isChecoldaUsed = Checolda.GetBool("isChecoldaUsed");
            Checolda.SetBool("isChecoldaUsed", !isChecoldaUsed);
        }


    }

    public bool CanOpenChecolda()
    {
        if (playerCamera == null) return false;

        // ��������� ���������� ����� ������� � ��������
        float distanceToChecolda = Vector3.Distance(playerCamera.transform.position, checolda.transform.position);
        if (distanceToChecolda > interactionDistance) return false;

        // ��������� ����������� ������� ������ � �������
        Vector3 directionToChecolda = checolda.transform.position - playerCamera.transform.position;
        float angle = Vector3.Angle(playerCamera.transform.forward, directionToChecolda);
        if (angle > maxAngle) return false;

        return true;
    }
}
