using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCPatrol : MonoBehaviour
{
    public Transform[] points; // Массив точек для патрулирования
    private int destPoint = 0; // Индекс текущей точки назначения
    private NavMeshAgent agent; // Ссылка на компонент NavMeshAgent

    void Start()
    {
        agent = GetComponent<NavMeshAgent>(); // Получаем ссылку на компонент NavMeshAgent

        // Отключаем автоматическую остановку при достижении точки
        agent.autoBraking = false;

        GotoNextPoint(); // Начинаем с первой точки
    }

    void GotoNextPoint()
    {
        if (points.Length == 0)
            return; // Выход из метода, если точки не заданы

        // Устанавливаем случайную точку как цель
        destPoint = Random.Range(0, points.Length);
        agent.destination = points[destPoint].position;
    }

    void Update()
    {
        // Если агент достиг текущей точки, выбираем следующую
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
            GotoNextPoint();
    }
}