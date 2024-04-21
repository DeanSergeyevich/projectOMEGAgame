using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class VentEntryTrigger : MonoBehaviour
{
    private Transform ventCameraPosition; // ѕозици€ камеры в вентил€ции
    public GameObject playerBody; // “ело игрока
    public Animator playerAnimator; // јниматор тела игрока
    public KeyCode enterVentKey = KeyCode.E; //  лавиша дл€ входа в вентил€цию

    public LayerMask interactionLayer; // —лой дл€ проверки взаимодействи€ с вентил€цией
    public float interactionDistance = 2f; // ƒистанци€ взаимодействи€ с вентил€цией
    public Camera playerCamera; //  амера игрока

    public Transform[] ventEntryPoints; // ћассив точек входа в вентил€цию
    public UnityEvent OnEnterVent; // —обытие дл€ сигнала о входе в вентил€цию

    private bool isTransitioning = false; // ‘лаг, указывающий, идет ли в данный момент процесс перемещени€ в вентил€цию

    private void Update()
    {
        // ѕровер€ем нажатие кнопки "E" и находимс€ ли мы в зоне взаимодействи€ с вентил€цией и смотрим ли мы на нее
        if (Input.GetKeyDown(enterVentKey) && !isTransitioning && CanEnterVent())
        {
            // Ќайдем точку входа в вентил€цию, на которую смотрит игрок
            Transform nearestEntryPoint = FindNearestEntryPoint();

            if (nearestEntryPoint != null)
            {
                // ѕеремещение камеры в вентил€цию
                StartCoroutine(TransitionToVent(nearestEntryPoint));
            }
        }
    }

    private bool CanEnterVent()
    {
        // —оздаем луч из центра камеры игрока в направлении, куда смотрит камера
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        // ѕровер€ем, попал ли луч в зону взаимодействи€ с вентил€цией
        if (Physics.Raycast(ray, out hit, interactionDistance, interactionLayer))
        {
            return true;
        }

        return false;
    }

    private IEnumerator TransitionToVent(Transform entryPoint)
    {
        isTransitioning = true; // ”станавливаем флаг в true, чтобы предотвратить повторный запуск процесса

        // —охран€ем позицию камеры в вентил€ции
        ventCameraPosition = entryPoint;

        // ѕеремещаем игрока к выбранной точке входа в вентил€цию
        CharacterController playerController = playerBody.GetComponent<CharacterController>();
        if (playerController != null)
        {
            playerController.enabled = false; // ќтключаем CharacterController перед перемещением
            playerBody.transform.position = ventCameraPosition.position;
            playerBody.transform.rotation = ventCameraPosition.rotation;
            playerController.enabled = true; // ¬ключаем CharacterController после перемещени€
        }

        // јктивируем анимацию приседани€ тела игрока
        playerAnimator.SetBool("IsCrouching", true);

        // ќтправл€ем сигнал о входе в вентил€цию
        OnEnterVent.Invoke();

        yield return null;

        isTransitioning = false; // —брасываем флаг, чтобы разрешить новый вход в вентил€цию
    }

    private Transform FindNearestEntryPoint()
    {
        Transform nearestEntryPoint = null;
        float highestDotProduct = -Mathf.Infinity;
        Vector3 playerDirection = playerCamera.transform.forward;

        foreach (Transform entryPoint in ventEntryPoints)
        {
            Vector3 directionToEntryPoint = (entryPoint.position - playerCamera.transform.position).normalized;
            float dotProduct = Vector3.Dot(playerDirection, directionToEntryPoint);

            // ѕровер€ем, находитс€ ли точка входа в вентил€цию в поле зрени€ игрока
            if (dotProduct > highestDotProduct)
            {
                highestDotProduct = dotProduct;
                nearestEntryPoint = entryPoint;
            }
        }

        return nearestEntryPoint;
    }
}
