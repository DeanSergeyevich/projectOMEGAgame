using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public GameObject keyObject;
    private bool isPickedUp = false;

    void Update()
    {
        // Проверяем, смотрит ли игрок на ключ и находится ли он рядом с ним
        if (!isPickedUp && Input.GetKeyDown(KeyCode.E) && IsPlayerLookingAtKey() && IsPlayerNearKey())
        {
            isPickedUp = true;
            keyObject.SetActive(false);
            Debug.Log("Key picked up!");
        }
    }

    // Метод для проверки, смотрит ли игрок на ключ
    private bool IsPlayerLookingAtKey()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit))
        {
            if (hit.collider.gameObject == keyObject)
            {
                return true;
            }
        }
        return false;
    }

    // Метод для проверки, находится ли игрок рядом с ключом
    private bool IsPlayerNearKey()
    {
        float distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        return distance < 2f; //  расстояние, которое считается "рядом с ключом"
    }

    public bool IsPickedUp()
    {
        return isPickedUp;
    }
}

