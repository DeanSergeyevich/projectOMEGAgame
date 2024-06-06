using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Transform[] patrolPoints; // Массив точек патрулирования
    private int currentPatrolIndex = 0; // Индекс текущей точки патрулирования
    private NavMeshAgent navMeshAgent; // Компонент NavMeshAgent для управления движением
    public Transform player; // Ссылка на игрока
    public HealthBar healthBar; // Ссылка на компонент HealthBar игрока

    public float detectionRadius = 10f; // Радиус обнаружения игрока
    public float attackRange = 2f; // Радиус атаки игрока
    public float attackCooldown = 2f; // Кулдаун атаки
    public float fieldOfViewAngle = 110f; // Угол обзора врага

    private bool isPlayerDetected = false; // Флаг обнаружения игрока
    private float lastAttackTime = 0f; // Время последней атаки

    void Start()
    {
        // Получение компонента NavMeshAgent
        navMeshAgent = GetComponent<NavMeshAgent>();

        if (navMeshAgent == null)
        {
            Debug.LogError("Компонент NavMeshAgent отсутствует");
            return;
        }

        // Поиск игрока по тегу "Player"
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Установка агента на ближайшую точку NavMesh
        if (NavMesh.SamplePosition(transform.position, out NavMeshHit hit, 1.0f, NavMesh.AllAreas))
        {
            navMeshAgent.Warp(hit.position);
            Debug.Log("NavMeshAgent успешно размещен на NavMesh в позиции: " + hit.position);
            GoToNextPatrolPoint(); // Начало патрулирования
        }
        else
        {
            Debug.LogError("NavMeshAgent не находится на NavMesh или поблизости не найден NavMesh. Текущая позиция: " + transform.position);
        }
    }

    void Update()
    {
        // Проверка, находится ли агент на NavMesh
        if (navMeshAgent == null || !navMeshAgent.isOnNavMesh) return;

        if (isPlayerDetected)
        {
            ChasePlayer(); // Преследование игрока

            // Атака игрока при приближении на расстояние атаки
            if (Vector3.Distance(transform.position, player.position) <= attackRange)
            {
                Attack();
            }
        }
        else
        {
            Patrol(); // Патрулирование
            DetectPlayer(); // Обнаружение игрока
        }
    }

    void Patrol()
    {
        // Проверка, находится ли агент на NavMesh
        if (navMeshAgent == null || !navMeshAgent.isOnNavMesh) return;

        // Переход к следующей точке патрулирования при достижении текущей
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.5f)
        {
            GoToNextPatrolPoint();
        }
    }

    void GoToNextPatrolPoint()
    {
        if (patrolPoints.Length == 0) return; // Проверка наличия точек патрулирования

        // Установка следующей точки патрулирования
        navMeshAgent.destination = patrolPoints[currentPatrolIndex].position;
        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length; // Переход к следующей точке в цикле
    }

    void ChasePlayer()
    {
        // Проверка, находится ли агент на NavMesh
        if (navMeshAgent == null || !navMeshAgent.isOnNavMesh) return;

        // Установка игрока в качестве цели для преследования
        navMeshAgent.destination = player.position;
    }

    void Attack()
    {
        // Проверка кулдауна атаки
        if (Time.time - lastAttackTime > attackCooldown)
        {
            // Уменьшение здоровья игрока, если есть ссылка на HealthBar
            if (healthBar != null)
            {
                healthBar.ChangeHealth(-10); // Уменьшение здоровья на 10 единиц
                Debug.Log("Атака игрока");
            }
            lastAttackTime = Time.time; // Обновление времени последней атаки
        }
    }

    void DetectPlayer()
    {
        // Направление от врага к игроку
        Vector3 directionToPlayer = player.position - transform.position;
        float angle = Vector3.Angle(directionToPlayer, transform.forward);

        // Проверка, находится ли игрок в пределах угла обзора
        if (angle < fieldOfViewAngle * 0.5f)
        {
            // Проверка, находится ли игрок в пределах радиуса обнаружения
            if (Vector3.Distance(transform.position, player.position) <= detectionRadius)
            {
                RaycastHit hit;
                // Проверка наличия прямой видимости до игрока
                if (Physics.Raycast(transform.position + Vector3.up, directionToPlayer.normalized, out hit, detectionRadius))
                {
                    if (hit.collider.CompareTag("Player"))
                    {
                        isPlayerDetected = true; // Обнаружение игрока
                        Debug.Log("Игрок обнаружен");
                    }
                }
            }
        }
    }

    void OnDrawGizmos()
    {
        // Отображение радиуса обнаружения в редакторе Unity
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}