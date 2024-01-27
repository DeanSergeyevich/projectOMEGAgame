using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meal : MonoBehaviour
{
    // Объявление переменной для хранения ссылки на объект с компонентом StaminaHunger.
    private StaminaHunger stamina;
    public float eda = 30.0f;

    // Start is called before the first frame update
    void Start()
    {
        // Находим объект с компонентом StaminaHunger по его имени.
        GameObject playerObject = GameObject.Find("Player"); // Замените "Player" на имя своего игрока.

        // Получаем компонент StaminaHunger с найденного объекта.
        stamina = playerObject.GetComponent<StaminaHunger>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Eda()
    {
        if (stamina != null)
        {
            stamina.playerHunger -= eda;
        }
        else
        {
            Debug.LogWarning("Не найден компонент StaminaHunger на объекте Player.");
        }
    }
}
