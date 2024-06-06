using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCPatrol : MonoBehaviour
{
    // ������ ����� ��������, ������� NPC ����� �������������
    public Transform[] waypoints;
    // ������ ������� ����� ��������
    private int currentWaypointIndex = 0;
    // ������ �� ��������� NavMeshAgent
    public NavMeshAgent agent;

    void Start()
    {
        // �������� ������ �� ��������� NavMeshAgent
        agent = GetComponent<NavMeshAgent>();
        // ��������� �������������� ���������� ����� ������� ��������
        agent.autoBraking = false;
        // ��������� � ��������� ����� ��������
        GotoNextWaypoint();
    }

    void GotoNextWaypoint()
    {
        // ���� ��� ����� ��������, ������� �� ������
        if (waypoints.Length == 0)
            return;

        // ������������� ��������� ����� ����������
        agent.destination = waypoints[currentWaypointIndex].position;
        // ��������� ������ ������� ����� ��������
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
    }

    void Update()
    {
        // ���� NPC ������ ������� ����� ��������, ��������� � ���������
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
            GotoNextWaypoint();
    }
}