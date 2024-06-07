using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCPatrol : MonoBehaviour
{
    public Transform[] points; // ������ ����� ��� ��������������
    private int destPoint = 0; // ������ ������� ����� ����������
    private NavMeshAgent agent; // ������ �� ��������� NavMeshAgent

    void Start()
    {
        agent = GetComponent<NavMeshAgent>(); // �������� ������ �� ��������� NavMeshAgent

        // ��������� �������������� ��������� ��� ���������� �����
        agent.autoBraking = false;

        GotoNextPoint(); // �������� � ������ �����
    }

    void GotoNextPoint()
    {
        if (points.Length == 0)
            return; // ����� �� ������, ���� ����� �� ������

        // ������������� ��������� ����� ��� ����
        destPoint = Random.Range(0, points.Length);
        agent.destination = points[destPoint].position;
    }

    void Update()
    {
        // ���� ����� ������ ������� �����, �������� ���������
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
            GotoNextPoint();
    }
}