using System.Collections;
using UnityEngine;

public class fleshlight : MonoBehaviour
{
    public Light spotL; // Ссылка на компонент света фонарика
    public Transform playerCamera; // Ссылка на позицию камеры игрока
    public float delay = 0.1f; // Задержка между обновлениями позиции и вращения фонарика
    public float smoothFactor = 5f; // Параметр для определения, насколько плавным будет движение

    // Ссылка на скрипт управления батареей
    public BatteryController batteryController;

    private void Start()
    {
        StartCoroutine(RepeatCameraMovement()); // Запускаем корутину для обновления позиции и вращения фонарика
    }

    // Корутина для повторяющегося обновления позиции и вращения фонарика
    private IEnumerator RepeatCameraMovement()
    {
        while (true)
        {
            // Сохраняем текущую позицию и вращение камеры
            Vector3 currentCameraPosition = playerCamera.position;
            Quaternion currentCameraRotation = playerCamera.rotation;

            yield return new WaitForSeconds(delay);

            // Запускаем корутину для плавного движения фонарика к позиции и вращению камеры
            StartCoroutine(MoveSmoothly(currentCameraPosition, currentCameraRotation));
        }
    }

    // Плавное движение фонарика к позиции и вращению камеры игрока
    private IEnumerator MoveSmoothly(Vector3 targetPosition, Quaternion targetRotation)
    {
        float elapsedTime = 0f;
        Vector3 startingPosition = transform.position;
        Quaternion startingRotation = transform.rotation;

        while (elapsedTime < delay)
        {
            // Линейная интерполяция позиции фонарика
            transform.position = Vector3.Lerp(startingPosition, targetPosition, elapsedTime / delay);
            // Сферическая интерполяция вращения фонарика
            transform.rotation = Quaternion.Slerp(startingRotation, targetRotation, elapsedTime / delay);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Гарантируем, что финальное положение и вращение точно установлены
        transform.position = targetPosition;
        transform.rotation = targetRotation;
    }

    private void Update()
    {
        // Если нажата клавиша F и батарея не пуста
        if (Input.GetKeyDown(KeyCode.F) && batteryController.IsBatteryNotEmpty())
        {
            spotL.enabled = !spotL.enabled; // Включаем или выключаем фонарик
        }

        // Плавно возвращаем фонарик к позиции игрока
        transform.rotation = Quaternion.Slerp(transform.rotation, playerCamera.rotation, Time.deltaTime * smoothFactor);
    }
}
