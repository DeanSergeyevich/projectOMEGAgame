using UnityEngine;

public class VentEntryTrigger : MonoBehaviour
{
    public Transform ventCameraPosition; // Позиция камеры в вентиляции
    public GameObject playerBody; // Тело игрока
    public Animator playerAnimator; // Аниматор тела игрока
    public KeyCode enterVentKey = KeyCode.E; // Клавиша для входа в вентиляцию

    public LayerMask interactionLayer; // Слой для проверки взаимодействия с вентиляцией
    public float interactionDistance = 2f; // Дистанция взаимодействия с вентиляцией
    public Camera playerCamera; // Камера игрока

    private bool isTransitioning = false; // Флаг, указывающий, идет ли в данный момент процесс перемещения в вентиляцию
    private float animationDuration = 3f; // Продолжительность анимации перемещения камеры

    private void Update()
    {
        // Проверяем нажатие кнопки "E" и находимся ли мы в зоне взаимодействия с вентиляцией и смотрим ли мы на нее
        if (Input.GetKeyDown(enterVentKey) && !isTransitioning && CanEnterVent())
        {
            // Плавное перемещение камеры в вентиляцию, если вентиляция в данный момент не активна
            SmoothCameraTransition();
        }
    }

    private bool CanEnterVent()
    {
        // Создаем луч из центра камеры игрока в направлении, куда смотрит камера
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        // Проверяем, попал ли луч в зону взаимодействия с вентиляцией
        if (Physics.Raycast(ray, out hit, interactionDistance, interactionLayer))
        {
            return true;
        }

        return false;
    }

    private void SmoothCameraTransition()
    {
        isTransitioning = true; // Устанавливаем флаг в true, чтобы предотвратить повторный запуск процесса

        // Запускаем анимацию плавного перемещения камеры в вентиляцию
        // Это может быть реализовано различными способами, например, с помощью аниматора или кода

        // После завершения анимации вызываем метод телепортации тела игрока
        Invoke("TeleportPlayerBody", animationDuration);
    }

    private void TeleportPlayerBody()
    {
        // Телепортируем тело игрока к позиции камеры в вентиляции
        playerBody.transform.position = ventCameraPosition.position;
        playerBody.transform.rotation = ventCameraPosition.rotation;

        // Активируем анимацию приседания тела игрока
        playerAnimator.SetBool("IsCrouching", true);

        isTransitioning = false; // Сбрасываем флаг, чтобы разрешить новый вход в вентиляцию
    }
}
