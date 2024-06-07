using UnityEngine;
using UnityEngine.AI;

public class NPCDetectPlayer : MonoBehaviour
{
    public Transform player; // ������ �� ��������� ������
    public int damage = 5; // ���������� �����, ���������� ������
    public float detectionRadius = 10f; // ������ ����������� ������
    public float chaseRadius = 15f; // ������ ������������� ������
    public float attackRadius = 1.5f; // ������ �����
    public float attackInterval = 2f; // �������� ����� �������

    private float lastAttackTime; // ����� ��������� �����
    private NavMeshAgent agent; // ������ �� ��������� NavMeshAgent
    private NPCPatrol patrolScript; // ������ �� ������ ��������������
    private bool isChasingPlayer = false; // ���� ������������� ������
    public Animator animator; // ������ �� ��������

    void Start()
    {
        agent = GetComponent<NavMeshAgent>(); // �������� ������ �� ��������� NavMeshAgent
        patrolScript = GetComponent<NPCPatrol>(); // �������� ������ �� ������ ��������������
        animator = GetComponent<Animator>(); // �������� ������ �� ��������
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
            animator.SetBool("isWalking", true); // ��������� �������� ������
        }
        else // ���� ����� ��� ���� �������������, ������������ � ��������������
        {
            patrolScript.enabled = true;
            isChasingPlayer = false;
            animator.SetBool("isWalking", false); // ������������� �������� ������
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
        animator.SetBool("isWalking", true); // ��������� �������� ������
    }

    void AttackPlayer()
    {
        animator.SetTrigger("attack"); // ��������� �������� �����
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