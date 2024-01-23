using UnityEngine;

public class ThrowObject : MonoBehaviour
{
    private GameObject heldObject; // ������ �� ����������� ������, ���� ����
    private GravityGun Mini;

    private void Start()
    {
        Mini = GetComponent<GravityGun>();
    }


    void Update()
    {
        // �������� ������� �� ������ ������ ����
        if (Input.GetMouseButtonDown(1)) // && pickedObject != null)
        {
            // ������ ������������ �������
            Rigidbody rb = Mini.heldObject.GetComponent<Rigidbody>();
            // rb.isKinematic = false;
            // pickedObject.GetComponent<Rigidbody>().isKinematic = true;
            rb.velocity = Camera.main.transform.forward * 10f; // �������� �������� � ����������� ������ �� ������ ����������
            //pickedObject = null;
            Mini.heldObject.GetComponent<Rigidbody>().useGravity = true;
        }
    }

    
}
