using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Movement : MonoBehaviour
{
    [Header("Mouse")]
    [SerializeField] Transform playerCamera; // Камера игрока.
    [SerializeField] bool cursorLock = true; // Флаг блокировки курсора.

    [Header("Movement")]
    [SerializeField][Range(0.0f, 0.5f)] float moveSmoothTime = 0.3f; // Время сглаживания движения
    [SerializeField] float Speed = 3.0f; // Базовая скорость перемещения
    [SerializeField] float gravity = -30f; // Гравитация
    [SerializeField] float jumpHeight = 6f; // Высота прыжка
    [SerializeField] float runSpeed = 4f; // Скорость бега
    [SerializeField] Transform groundCheck;  // Точка для проверки земли
    [SerializeField] LayerMask ground; // Слой земли
    public AudioSource footstepsAudioSource; // Аудио для воспроизведения шагов
    public AudioSource runningAudioSource;// Аудио для воспроизведения Бега
    




    [Header("Crouch")]
    public float standingHeight = 2f; // Высота в стоячем положении
    public float crouchingHeight = 1f; // Высота в приседающем положении
    private bool isCrouching = false; // Флаг приседания
    Animator animator;  // Аниматор для управления анимациями.




    [Header("Stamina")]
    private StaminaEnergy stamina; // Ссылка на компонент StaminaEnergy.

    float velocityY; // Скорость падения.
    bool isGrounded; // Флаг нахождения на земле.
    bool isRunning; // Флаг бега.



    CharacterController controller; // Ссылка на CharacterController
    Vector2 currentDir; // Текущее направление движения
    Vector2 currentDirVelocity; // Скорость сглаживания направления движения
    Vector3 velocity; // Текущая скорость движения

    // Инициализация компонентов и параметров при старте
    void Start()
    {
        controller = GetComponent<CharacterController>();

        // Инициализация переменных при старте объекта, если это необходимо.
        stamina = GetComponent<StaminaEnergy>(); // Пример инициализации stamina, если он находится на том же объекте.

        if (cursorLock)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = true;
        }

        animator = GetComponent<Animator>(); // Получение компонента Animator.

    }

    // Обновление состояния персонажа
    void Update()
    {
        //UpdateMouse();
        UpdateMove(); // Обновление движения
        Run(); // Управление бегом
        Crouch(); // Управление приседанием
    }

   
    void UpdateMove()
    {
        
        // Проверка на землю
        bool isGrounded = Physics.CheckSphere(groundCheck.position, 0.2f, ground);

        // Получение ввода для направления движения 
        Vector2 targetDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        targetDir.Normalize();

        // Сглаживание направления движения
        currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentDirVelocity, moveSmoothTime);

        // Применение гравитации
        velocityY += gravity * 2f * Time.deltaTime;

        // Определение текущей скорости перемещения (бег/ходьба)
        float currentSpeed = isRunning ? runSpeed : Speed;

        // Вычисление вектора движения
        Vector3 moveDirection = transform.forward * currentDir.y + transform.right * currentDir.x;
        velocity = moveDirection * currentSpeed + Vector3.up * velocityY;

        // Применение движение к контроллеру персонажа
        controller.Move(velocity * Time.deltaTime);

        // Прыжок
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            velocityY = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Установка скорости падения при падении с высоты
        if (isGrounded! && controller.velocity.y < -1f)
        {
            velocityY = -8f * Time.deltaTime;
        }

        // Воспроизведение звуков шагов при ходьбе
        if (isGrounded && currentDir.magnitude > 0.1f && !isRunning && !footstepsAudioSource.isPlaying)
        {
            footstepsAudioSource.Play();
        }
        // Воспроизведение звуков бега при беге
        else if (isGrounded && currentDir.magnitude > 0.1f && isRunning && !runningAudioSource.isPlaying)
        {
            runningAudioSource.Play();
        }

        // Остановка звуков шагов при прекращении движения
        if (currentDir.magnitude < 0.1f && footstepsAudioSource.isPlaying)
        {
            footstepsAudioSource.Stop();
        }
        // Остановка звуков бега при прекращении бега
        if (!isRunning && runningAudioSource.isPlaying)
        {
            runningAudioSource.Stop();
        }

    }

    // Управление бегом
   void  Run()
    {
        // Получите ввод с клавиатуры для бега
        bool runInput = Input.GetKey(KeyCode.LeftShift);

        // Если игрок начинает бежать и выносливость больше 0, уменьшаем выносливость.
        if (runInput && !isRunning && stamina.playerStamina > 0 && !isCrouching)
        {
            isRunning = true;
            stamina.playerStamina -= 0.5f;
         
        }
        // Если игрок перестает бежать или выносливость равна 0, останавливаем бег.
        else if ((!runInput || stamina.playerStamina <= 0 || isCrouching) && isRunning)
        {
            isRunning = false;
            
        }

        // Если isRunning равно true, отнимаем определенное значение из playerStamina в staminaEnergy.
        if (isRunning)
        {
            stamina.playerStamina -= 0.1f;
            Speed = 2f; // Делает в итоге скорость 2
        }

        // Восстановелние выносливости и скорости после бега 
        if (!runInput && !isRunning && stamina.playerStamina < stamina.maxStamina)
        {
            stamina.playerStamina += 0.1f;
            Speed = 3f; // При восстановлении  выравнивается нормальная скорость 5f
        }

        // Восстановление выносливости при приседании.
        if (runInput && isCrouching && stamina.playerStamina < stamina.maxStamina)
        {
            stamina.playerStamina +=  0.1f;
        }

        // Ограничьте выносливость максимальным значением и минимальным значением 0.
        stamina.playerStamina = Mathf.Clamp(stamina.playerStamina, 0f, stamina.maxStamina);

      
    }

    // Управление приседанием.
    void Crouch()
    {
        // Переключение приседание при нажатии клавиши
        if (Input.GetKeyDown(KeyCode.C))
        {
            isCrouching = !isCrouching;
            animator.SetBool("IsCrouching", isCrouching);
            Speed = 2f;
        }
        else if (!isCrouching)
        {
            Speed = 3f;
        }


    }

    // Метод для постепенного затухания звука.
    IEnumerator FadeOutSound(AudioSource audioSource, float fadeDuration)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / fadeDuration;
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }
  
}