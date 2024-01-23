using UnityEngine;
using System.Collections.Generic;

public class ObjectWeightManager : MonoBehaviour
{
    [Tooltip("������ ��������, ��� ����� ����� ��������.")]
    public List<GameObject> prefabsToCheck = new List<GameObject>();

    private void Start()
    {
        foreach (GameObject prefab in prefabsToCheck)
        {
            // ���������, ��� ������ �� �������� null
            if (prefab != null)
            {
                // �������� ��������� Rigidbody �� �������
                Rigidbody prefabRigidbody = prefab.GetComponent<Rigidbody>();

                if (prefabRigidbody != null)
                {
                    // �������� ����� ������� �� ���������� Rigidbody
                    float objectMass = prefabRigidbody.mass;

                    // ����������� �������� ����� � ����� ������
                    Debug.Log("����� ������� " + prefab.name + ": " + objectMass);
                }
                else
                {
                    // ���� ��������� Rigidbody ����������� �� �������, ����������� ���
                    Debug.LogWarning("��������� Rigidbody ����������� �� ������� " + prefab.name);
                }
            }
            else
            {
                // ���� ������ �������� null, ����������� ���
                Debug.LogWarning("������ � ������ ����� null.");
            }
        }
    }

    // ����� ��� ��������� ����� ������� �� ��� �����
    public float GetObjectMass(GameObject obj)
    {
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            return rb.mass;
        }
        else
        {
            Debug.LogWarning("��������� Rigidbody �� ������ �� �������: " + obj.name);
            return 0f;
        }
    }
}
