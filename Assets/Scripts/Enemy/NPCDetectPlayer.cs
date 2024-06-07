using UnityEngine;
using UnityEngine.AI;

public class NPCDetectPlayer : MonoBehaviour
{
    public Transform player; // Ссылка на трансформ игрока
    public int damage = 5; // Количество урона, наносимого игроку
    public float detectionRadius = 10f; // Радиус обнаружения игрока
    public float chaseRadius = 15f; // Радиус преследования игрока
    public float attackRadius = 1.5f; // Радиус атаки
    public float attackInterval = 2f; // Интервал между атаками

    private float lastAttackTime; // Время последней атаки
    private NavMeshAgent agent; // Ссылка на компонент NavMeshAgent
    private NPCPatrol patrolScript; // Ссылка на скрипт патрулирования
    private bool isChasingPlayer = false; // Флаг преследования игрока
    public Animator animator; // Ссылка на аниматор

    void Start()
    {
        agent = GetComponent<NavMeshAgent>(); // Получаем ссылку на компонент NavMeshAgent
        patrolScript = GetComponent<NPCPatrol>(); // Получаем ссылку на скрипт патрулирования
        animator = GetComponent<Animator>(); // Получаем ссылку на аниматор
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(player.position, transform.position); // Вычисляем расстояние до игрока

        if (distanceToPlayer < detectionRadius) // Если игрок в радиусе обнаружения, начинаем преследование
        {
            ChasePlayer();
        }
        else if (distanceToPlayer < chaseRadius) // Если игрок в радиусе преследования, следуем за ним
        {
            agent.destination = player.position;
            animator.SetBool("isWalking", true); // Запускаем анимацию ходьбы
        }
        else // Если игрок вне зоны преследования, возвращаемся к патрулированию
        {
            patrolScript.enabled = true;
            isChasingPlayer = false;
            animator.SetBool("isWalking", false); // Останавливаем анимацию ходьбы
        }

        if (isChasingPlayer && distanceToPlayer <= attackRadius && Time.time - lastAttackTime >= attackInterval)
        {
            AttackPlayer(); // Атакуем игрока, если он в радиусе атаки и прошло достаточно времени с последней атаки
            lastAttackTime = Time.time; // Обновляем время последней атаки
        }
    }

    void ChasePlayer()
    {
        patrolScript.enabled = false; // Отключаем патрулирование
        agent.destination = player.position; // Устанавливаем игрока как цель
        isChasingPlayer = true; // Устанавливаем флаг преследования
        animator.SetBool("isWalking", true); // Запускаем анимацию ходьбы
    }

    void AttackPlayer()
    {
        animator.SetTrigger("attack"); // Запускаем анимацию атаки
        HealthBar healthBar = player.GetComponent<HealthBar>(); // Получаем компонент здоровья игрока
        if (healthBar != null) // Если компонент здоровья найден, наносим урон
        {
            Debug.Log("Игрок в радиусе атаки. Наносим урон."); // Сообщение для отладки
            healthBar.ChangeHealth(-damage); // Уменьшаем здоровье игрока
        }
        else
        {
            Debug.Log("Компонент HealthBar не найден у игрока."); // Сообщение для отладки
        }
    }
}