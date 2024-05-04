using UnityEngine;

public class UseChecolda : MonoBehaviour
{
    public GameObject[] checoldas; // Массив щеколд
    public Camera playerCamera;
    public LayerMask interactChecolda;
    public float interactionDistance = 2f;
    public float maxAngle = 30f; // Максимальный угол между направлением взгляда и направлением к щеколде

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            foreach (GameObject checolda in checoldas)
            {
                if (CanOpenChecolda(checolda))
                {
                    Animator animator = checolda.GetComponent<Animator>();
                    if (animator != null)
                    {
                        bool isChecoldaUsed = animator.GetBool("isChecoldaUsed");
                        animator.SetBool("isChecoldaUsed", !isChecoldaUsed);
                    }
                }
            }
        }
    }

    public bool CanOpenChecolda(GameObject checolda)
    {
        if (playerCamera == null || checolda == null) return false;

        float distanceToChecolda = Vector3.Distance(playerCamera.transform.position, checolda.transform.position);
        if (distanceToChecolda > interactionDistance) return false;

        Vector3 directionToChecolda = checolda.transform.position - playerCamera.transform.position;
        float angle = Vector3.Angle(playerCamera.transform.forward, directionToChecolda);
        if (angle > maxAngle) return false;

        return true;
    }
}
