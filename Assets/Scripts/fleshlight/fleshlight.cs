using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fleshlight : MonoBehaviour
{
    public Light spotL;
    public Transform playerCamera;
    public float delay = 0.1f;
    public float smoothFactor = 5f;  // Параметр для определения, насколько плавным будет движение

    private void Start()
    {
        StartCoroutine(RepeatCameraMovement());
    }

    private IEnumerator RepeatCameraMovement()
    {
        while (true)
        {
            Vector3 currentCameraPosition = playerCamera.position;
            Quaternion currentCameraRotation = playerCamera.rotation;

            yield return new WaitForSeconds(delay);

            // Используем интерполяцию для плавного движения
            StartCoroutine(MoveSmoothly(currentCameraPosition, currentCameraRotation));
        }
    }

    private IEnumerator MoveSmoothly(Vector3 targetPosition, Quaternion targetRotation)
    {
        float elapsedTime = 0f;
        Vector3 startingPosition = transform.position;
        Quaternion startingRotation = transform.rotation;

        while (elapsedTime < delay)
        {
            transform.position = Vector3.Lerp(startingPosition, targetPosition, elapsedTime / delay);
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
        if (Input.GetKeyDown(KeyCode.F))
        {
            spotL.enabled = !spotL.enabled;  // Используем оператор инверсии
        }

        // Используем Quaternion.Slerp для плавного возвращения к взгляду игрока
        transform.rotation = Quaternion.Slerp(transform.rotation, playerCamera.rotation, Time.deltaTime * smoothFactor);
    }
}
