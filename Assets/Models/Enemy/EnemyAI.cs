using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform player;  // Ссылка на игрока
    public Transform[] points;  // Массив точек для патрулирования
    public float detectionRadius = 10f;  // Радиус обнаружения игрока
    public float attackRange = 2f;  // Радиус атаки игрока

    public NavMeshAgent agent;
    public Animator animator;
    private int destPoint = 0;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        if (agent == null)
        {
            Debug.LogError("NavMeshAgent не найден на " + gameObject.name);
            enabled = false;  // Отключаем скрипт, если NavMeshAgent отсутствует
            return;
        }

        if (animator == null)
        {
            Debug.LogError("Animator не найден на " + gameObject.name);
            enabled = false;  // Отключаем скрипт, если Animator отсутствует
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
            // Начинаем атаку
            agent.isStopped = true;  // Останавливаем агент
            animator.SetBool("isAttacking", true);
            animator.SetBool("isWalking", false);
            AttackPlayer();  // Вызываем метод атаки
        }
        else if (distanceToPlayer <= detectionRadius)
        {
            // Начинаем преследование игрока
            agent.isStopped = false;
            agent.destination = player.position;  // Устанавливаем целью игрока
            animator.SetBool("isWalking", true);
            animator.SetBool("isAttacking", false);
        }
        else
        {
            // Возвращаемся к патрулированию
            agent.isStopped = false;
            animator.SetBool("isWalking", true);
            animator.SetBool("isAttacking", false);

            // Переходим к следующей точке патрулирования, если достигли текущей
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
        // Логика атаки
        Debug.Log("Атакуем игрока!");
    }
}
