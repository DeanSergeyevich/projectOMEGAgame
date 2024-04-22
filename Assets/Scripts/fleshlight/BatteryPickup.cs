using UnityEngine;

public class BatteryPickup : MonoBehaviour
{
    // Сколько заряда дает батарейка при поднятии
    public float rechargeAmount = 100f;

    // Ссылка на компонент управления батареей
    public BatteryController batteryController;

    private void Update()
    {
        // Если нажата клавиша "E" и игрок находится в области батарейки, подзаряжаем батарею и уничтожаем объект-батарейку
        if (Input.GetKeyDown(KeyCode.E) && IsPlayerInRange())
        {
            batteryController.RechargeBattery(rechargeAmount);
            Destroy(gameObject);
        }
    }

    private bool IsPlayerInRange()
    {
        // Проверяем, есть ли объект Player в области триггера батарейки
        Collider[] colliders = Physics.OverlapSphere(transform.position, 1f);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                return true;
            }
        }
        return false;
    }
}
