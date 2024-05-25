using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLvl : MonoBehaviour
{
    public GameObject load_lvl;
    public KeyCode enterloadlvl = KeyCode.E; //  лавиша дл€ загрузки нового уровн€

    public LayerMask loadLvl; // —лой дл€ проверки взаимодействи€ с выходом на уровень
    public float interactionDistance = 2f; // ƒистанци€ взаимодействи€ с переходом на уровень
    public Camera playerCamera; //  амера игрока

   
    void Update()
    {
        if (Input.GetKeyDown(enterloadlvl) && CanEnterLvl())
        {
            SceneManager.LoadScene("lvl2");
        }
    }


    private bool CanEnterLvl()
    {
        // —оздаем луч из центра камеры игрока в направлении, куда смотрит камера
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        // ѕровер€ем, попал ли луч в зону взаимодействи€ с переходом
        if (Physics.Raycast(ray, out hit, interactionDistance, loadLvl))
        {
            Debug.Log("Raycast hit: " + hit.collider.name);
            return true;
        }

        Debug.Log("Raycast не затронул ни один объект в слое взаимодействи€.");
        return false;
    }
}
