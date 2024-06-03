using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Transform[] patrolPoints; // Точки патрулирования
    private int currentPatrolIndex = 0; // Индекс текущей точки патрулирования
    private NavMeshAgent navMeshAgent; // Компонент для навигации
    private Transform player; // Ссылка на игрока
    private bool isPlayerDetected = false; // Флаг обнаружения игрока

    public float detectionRadius = 10f; // Радиус обнаружения игрока
    public float attackRange = 2f; // Радиус атаки игрока
    public float attackCooldown = 2f; // Кулдаун атаки

    private float lastAttackTime = 0f; // Время последней атаки

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform; // Поиск игрока по тегу
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
            // Реализуйте здесь логику атаки, например, нанесение урона игроку
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
