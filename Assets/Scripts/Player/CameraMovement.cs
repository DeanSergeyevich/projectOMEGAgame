using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CameraMovement : MonoBehaviour
{
    public Transform playerBody; // Ссылка на трансформ игрока.
    public Slider slider; // Слайдер для настройки чувствительности мыши.
    [SerializeField][Range(0.0f, 500f)] public float mouseSensitivity = 100f; // Чувствительность мыши.

    private float cameraCap = 0f; // Ограничение по углу наклона камеры.

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Блокировка курсора в центре экрана.
        slider.value = mouseSensitivity; // Установка начального значения слайдера.
    } 

    void Update()
    {
        // Получение ввода мыши.
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Ограничение по углу наклона камеры.
        cameraCap -= mouseY;
        cameraCap = Mathf.Clamp(cameraCap, -90f, 90f);

        // Поворот камеры и игрока.
        transform.localRotation = Quaternion.Euler(cameraCap, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }

    // Метод для изменения чувствительности мыши.
    public void Sensety(float slider)
    {
        mouseSensitivity = slider;
    }
}
