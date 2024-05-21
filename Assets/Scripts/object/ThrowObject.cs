using UnityEngine;

public class ThrowObject : MonoBehaviour
{
    private GameObject heldObject; // ������ �� ����������� ������, ���� ����
    private GravityGun Mini; // ������ �� ��������� GravityGun.

    private void Start()
    {
        // �������� ��������� GravityGun ��� ������.
        Mini = GetComponent<GravityGun>();
    }


    void Update()
    {
        // �������� ������� �� ������ ������ ����
        if (Input.GetMouseButtonDown(1))
        {
            // ������ ������������ �������
            Rigidbody rb = Mini.heldObject.GetComponent<Rigidbody>(); // ��������� Rigidbody ������������ �������.
            rb.velocity = Camera.main.transform.forward * 10f; // �������� �������� � ����������� ������ �� ������ ����������          
            Mini.heldObject.GetComponent<Rigidbody>().useGravity = true; // ��������� ���������� ��� ������������ �������.
        }
    }

    
}
