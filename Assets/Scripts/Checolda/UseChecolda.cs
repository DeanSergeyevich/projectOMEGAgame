using UnityEngine;

public class UseChecolda : MonoBehaviour
{
    public Camera playerCamera; // Ссылка на камеру игрока
    public LayerMask interactChecolda; // Маска слоя для взаимодействия с щеколдой
    public float interactionDistance = 2f; // Расстояние для взаимодействия с объектами щеколды
    public float maxAngle = 30f; // Максимальный угол между направлением взгляда и направлением к щеколде

    void Update()
    {
        // Проверяем нажатие клавиши E
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;

            // Производим рейкаст из камеры игрока вперед
            if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, interactionDistance, interactChecolda))
            {
                GameObject checolda = hit.collider.gameObject;
                Animator animator = checolda.GetComponent<Animator>();

                // Если объект имеет компонент анимации и щеколда может быть открыта, меняем состояние аниматора
                if (animator != null && CanOpenChecolda(checolda))
                {
                    bool isChecoldaUsed = animator.GetBool("isChecoldaUsed");
                    animator.SetBool("isChecoldaUsed", !isChecoldaUsed);
                }
            }
        }
    }

    // Метод для проверки, может ли щеколда быть открыта
    public bool CanOpenChecolda(GameObject checolda)
    {
        if (playerCamera == null || checolda == null) return false;

        // Проверяем расстояние до щеколды
        float distanceToChecolda = Vector3.Distance(playerCamera.transform.position, checolda.transform.position);
        if (distanceToChecolda > interactionDistance) return false;

        // Проверяем угол между направлением взгляда игрока и направлением к щеколде
        Vector3 directionToChecolda = checolda.transform.position - playerCamera.transform.position;
        float angle = Vector3.Angle(playerCamera.transform.forward, directionToChecolda);
        if (angle > maxAngle) return false;

        return true;
    }
}