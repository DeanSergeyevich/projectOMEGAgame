using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Transform[] patrolPoints; // ������ ����� ��������������
    private int currentPatrolIndex = 0; // ������ ������� ����� ��������������
    private NavMeshAgent navMeshAgent; // ��������� NavMeshAgent ��� ���������� ���������
    public Transform player; // ������ �� ������
    public HealthBar healthBar; // ������ �� ��������� HealthBar ������

    public float detectionRadius = 10f; // ������ ����������� ������
    public float attackRange = 2f; // ������ ����� ������
    public float attackCooldown = 2f; // ������� �����
    public float fieldOfViewAngle = 110f; // ���� ������ �����

    private bool isPlayerDetected = false; // ���� ����������� ������
    private float lastAttackTime = 0f; // ����� ��������� �����

    void Start()
    {
        // ��������� ���������� NavMeshAgent
        navMeshAgent = GetComponent<NavMeshAgent>();

        if (navMeshAgent == null)
        {
            Debug.LogError("��������� NavMeshAgent �����������");
            return;
        }

        // ����� ������ �� ���� "Player"
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // ��������� ������ �� ��������� ����� NavMesh
        if (NavMesh.SamplePosition(transform.position, out NavMeshHit hit, 1.0f, NavMesh.AllAreas))
        {
            navMeshAgent.Warp(hit.position);
            Debug.Log("NavMeshAgent ������� �������� �� NavMesh � �������: " + hit.position);
            GoToNextPatrolPoint(); // ������ ��������������
        }
        else
        {
            Debug.LogError("NavMeshAgent �� ��������� �� NavMesh ��� ���������� �� ������ NavMesh. ������� �������: " + transform.position);
        }
    }

    void Update()
    {
        // ��������, ��������� �� ����� �� NavMesh
        if (navMeshAgent == null || !navMeshAgent.isOnNavMesh) return;

        if (isPlayerDetected)
        {
            ChasePlayer(); // ������������� ������

            // ����� ������ ��� ����������� �� ���������� �����
            if (Vector3.Distance(transform.position, player.position) <= attackRange)
            {
                Attack();
            }
        }
        else
        {
            Patrol(); // ��������������
            DetectPlayer(); // ����������� ������
        }
    }

    void Patrol()
    {
        // ��������, ��������� �� ����� �� NavMesh
        if (navMeshAgent == null || !navMeshAgent.isOnNavMesh) return;

        // ������� � ��������� ����� �������������� ��� ���������� �������
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.5f)
        {
            GoToNextPatrolPoint();
        }
    }

    void GoToNextPatrolPoint()
    {
        if (patrolPoints.Length == 0) return; // �������� ������� ����� ��������������

        // ��������� ��������� ����� ��������������
        navMeshAgent.destination = patrolPoints[currentPatrolIndex].position;
        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length; // ������� � ��������� ����� � �����
    }

    void ChasePlayer()
    {
        // ��������, ��������� �� ����� �� NavMesh
        if (navMeshAgent == null || !navMeshAgent.isOnNavMesh) return;

        // ��������� ������ � �������� ���� ��� �������������
        navMeshAgent.destination = player.position;
    }

    void Attack()
    {
        // �������� �������� �����
        if (Time.time - lastAttackTime > attackCooldown)
        {
            // ���������� �������� ������, ���� ���� ������ �� HealthBar
            if (healthBar != null)
            {
                healthBar.ChangeHealth(-10); // ���������� �������� �� 10 ������
                Debug.Log("����� ������");
            }
            lastAttackTime = Time.time; // ���������� ������� ��������� �����
        }
    }

    void DetectPlayer()
    {
        // ����������� �� ����� � ������
        Vector3 directionToPlayer = player.position - transform.position;
        float angle = Vector3.Angle(directionToPlayer, transform.forward);

        // ��������, ��������� �� ����� � �������� ���� ������
        if (angle < fieldOfViewAngle * 0.5f)
        {
            // ��������, ��������� �� ����� � �������� ������� �����������
            if (Vector3.Distance(transform.position, player.position) <= detectionRadius)
            {
                RaycastHit hit;
                // �������� ������� ������ ��������� �� ������
                if (Physics.Raycast(transform.position + Vector3.up, directionToPlayer.normalized, out hit, detectionRadius))
                {
                    if (hit.collider.CompareTag("Player"))
                    {
                        isPlayerDetected = true; // ����������� ������
                        Debug.Log("����� ���������");
                    }
                }
            }
        }
    }

    void OnDrawGizmos()
    {
        // ����������� ������� ����������� � ��������� Unity
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}