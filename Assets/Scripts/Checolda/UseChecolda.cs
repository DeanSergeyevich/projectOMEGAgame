using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseChecolda : MonoBehaviour
{

    public GameObject checolda;
    public bool isChecoldaUsed = false;
    public Animator Checolda;
    public Camera playerCamera; // Камера игрока
    public LayerMask interactChecolda;
    public float interactionDistance = 2f;
    public float maxAngle = 30f; // Максимальный угол между направлением взгляда и направлением к щеколде

    private void Start()
    {
        Checolda = GetComponent<Animator>();
        if(Checolda == null) 
        {
            Debug.LogError("Animator не найден!");
        }
        
    }


    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E) && CanOpenChecolda())
        {
            bool isChecoldaUsed = Checolda.GetBool("isChecoldaUsed");
            Checolda.SetBool("isChecoldaUsed", !isChecoldaUsed);
        }


    }

    public bool CanOpenChecolda()
    {
        if (playerCamera == null) return false;

        // Проверяем расстояние между игроком и щеколдой
        float distanceToChecolda = Vector3.Distance(playerCamera.transform.position, checolda.transform.position);
        if (distanceToChecolda > interactionDistance) return false;

        // Проверяем направление взгляда камеры к щеколде
        Vector3 directionToChecolda = checolda.transform.position - playerCamera.transform.position;
        float angle = Vector3.Angle(playerCamera.transform.forward, directionToChecolda);
        if (angle > maxAngle) return false;

        return true;
    }
}
