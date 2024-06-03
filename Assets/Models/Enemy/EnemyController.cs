using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Transform[] patrolPoints; // ����� ��������������
    private int currentPatrolIndex = 0; // ������ ������� ����� ��������������
    private NavMeshAgent navMeshAgent; // ��������� ��� ���������
    private Transform player; // ������ �� ������
    private bool isPlayerDetected = false; // ���� ����������� ������

    public float detectionRadius = 10f; // ������ ����������� ������
    public float attackRange = 2f; // ������ ����� ������
    public float attackCooldown = 2f; // ������� �����

    private float lastAttackTime = 0f; // ����� ��������� �����

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform; // ����� ������ �� ����
        GoToNextPatrolPoint();
    }

    void Update()
    {
        if (isPlayerDetected)
        {
            ChasePlayer();
            if (Vector3.Distance(transform.position, player.position) <= attackRange)
            {
                Attack();
            }
        }
        else
        {
            Patrol();
        }
    }

    void Patrol()
    {
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.5f)
        {
            GoToNextPatrolPoint();
        }
    }

    void GoToNextPatrolPoint()
    {
        navMeshAgent.destination = patrolPoints[currentPatrolIndex].position;
        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
    }

    void ChasePlayer()
    {
        navMeshAgent.destination = player.position;
    }

    void Attack()
    {
        if (Time.time - lastAttackTime > attackCooldown)
        {
            // ���������� ����� ������ �����, ��������, ��������� ����� ������
            lastAttackTime = Time.time;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerDetected = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerDetected = false;
        }
    }
}
