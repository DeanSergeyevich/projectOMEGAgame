using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCDetectPlayer : MonoBehaviour
{
    public Transform player; // ������ �� ��������� ������
    public int damage = 15; // ����������� ���������� �����, ���������� ������
    public float detectionRadius = 10f; // ������ ����������� ������
    public float chaseRadius = 15f; // ������ ������������� ������
    public float attackRadius = 1.5f; // ������ �����
    public float attackInterval = 1f; // �������� ����� �������

    private float lastAttackTime; // ����� ��������� �����
    private NavMeshAgent agent; // ������ �� ��������� NavMeshAgent
    private NPCPatrol patrolScript; // ������ �� ������ ��������������
    private bool isChasingPlayer = false; // ���� ������������� ������

    void Start()
    {
        agent = GetComponent<NavMeshAgent>(); // �������� ������ �� ��������� NavMeshAgent
        patrolScript = GetComponent<NPCPatrol>(); // �������� ������ �� ������ ��������������
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(player.position, transform.position); // ��������� ���������� �� ������

        if (distanceToPlayer < detectionRadius) // ���� ����� � ������� �����������, �������� �������������
        {
            ChasePlayer();
        }
        else if (distanceToPlayer < chaseRadius) // ���� ����� � ������� �������������, ������� �� ���
        {
            agent.destination = player.position;
        }
        else // ���� ����� ��� ���� �������������, ������������ � ��������������
        {
            patrolScript.enabled = true;
            isChasingPlayer = false;
        }

        if (isChasingPlayer && distanceToPlayer <= attackRadius && Time.time - lastAttackTime >= attackInterval)
        {
            AttackPlayer(); // ������� ������, ���� �� � ������� ����� � ������ ���������� ������� � ��������� �����
            lastAttackTime = Time.time; // ��������� ����� ��������� �����
        }
    }

    void ChasePlayer()
    {
        patrolScript.enabled = false; // ��������� ��������������
        agent.destination = player.position; // ������������� ������ ��� ����
        isChasingPlayer = true; // ������������� ���� �������������
    }

    void AttackPlayer()
    {
        HealthBar healthBar = player.GetComponent<HealthBar>(); // �������� ��������� �������� ������
        if (healthBar != null) // ���� ��������� �������� ������, ������� ����
        {
            Debug.Log("����� � ������� �����. ������� ����."); // ��������� ��� �������
            healthBar.ChangeHealth(-damage); // ��������� �������� ������
        }
        else
        {
            Debug.Log("��������� HealthBar �� ������ � ������."); // ��������� ��� �������
        }
    }
}