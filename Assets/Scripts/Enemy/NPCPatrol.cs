using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCPatrol : MonoBehaviour
{
    // Массив точек маршрута, которые NPC будет патрулировать
    public Transform[] waypoints;
    // Индекс текущей точки маршрута
    private int currentWaypointIndex = 0;
    // Ссылка на компонент NavMeshAgent
    public NavMeshAgent agent;

    void Start()
    {
        // Получаем ссылку на компонент NavMeshAgent
        agent = GetComponent<NavMeshAgent>();
        // Отключаем автоматическое торможение перед точками маршрута
        agent.autoBraking = false;
        // Переходим к следующей точке маршрута
        GotoNextWaypoint();
    }

    void GotoNextWaypoint()
    {
        // Если нет точек маршрута, выходим из метода
        if (waypoints.Length == 0)
            return;

        // Устанавливаем следующую точку назначения
        agent.destination = waypoints[currentWaypointIndex].position;
        // Обновляем индекс текущей точки маршрута
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
    }

    void Update()
    {
        // Если NPC достиг текущей точки маршрута, переходим к следующей
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
            GotoNextWaypoint();
    }
}