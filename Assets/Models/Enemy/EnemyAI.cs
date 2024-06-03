using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform player;  // ������ �� ������
    public Transform[] points;  // ������ ����� ��� ��������������
    public float detectionRadius = 10f;  // ������ ����������� ������
    public float attackRange = 2f;  // ������ ����� ������

    public NavMeshAgent agent;
    public Animator animator;
    private int destPoint = 0;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        if (agent == null)
        {
            Debug.LogError("NavMeshAgent �� ������ �� " + gameObject.name);
            enabled = false;  // ��������� ������, ���� NavMeshAgent �����������
            return;
        }

        if (animator == null)
        {
            Debug.LogError("Animator �� ������ �� " + gameObject.name);
            enabled = false;  // ��������� ������, ���� Animator �����������
            return;
        }

        GotoNextPoint();
    }

    void Update()
    {
        if (agent == null || animator == null)
            return;

        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        if (distanceToPlayer <= attackRange)
        {
            // �������� �����
            agent.isStopped = true;  // ������������� �����
            animator.SetBool("isAttacking", true);
            animator.SetBool("isWalking", false);
            AttackPlayer();  // �������� ����� �����
        }
        else if (distanceToPlayer <= detectionRadius)
        {
            // �������� ������������� ������
            agent.isStopped = false;
            agent.destination = player.position;  // ������������� ����� ������
            animator.SetBool("isWalking", true);
            animator.SetBool("isAttacking", false);
        }
        else
        {
            // ������������ � ��������������
            agent.isStopped = false;
            animator.SetBool("isWalking", true);
            animator.SetBool("isAttacking", false);

            // ��������� � ��������� ����� ��������������, ���� �������� �������
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
                GotoNextPoint();
        }
    }

    void GotoNextPoint()
    {
        if (points.Length == 0)
            return;

        agent.destination = points[destPoint].position;
        destPoint = (destPoint + 1) % points.Length;
    }

    void AttackPlayer()
    {
        // ������ �����
        Debug.Log("������� ������!");
    }
}
