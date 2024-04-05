using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class HealthBar : MonoBehaviour
{
    public Image bar;
    public float fill;
    public GameObject gameOverScreen;
    

    // ������������� ������������ �������� �������� � ������� ��������
    private void Start()
    {
        fill = 100f;
        gameOverScreen.SetActive(false); // ������ ����� ��������� ���� ���������� ��� �������
    }

    // ������������� ������� �������� ��������
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            fill -= 1;
            if (fill <= 0)
            {
                EndGame(); // �������� ����� ���������� ����, ���� �������� ������ ��� ����� ����
            }
        }
        bar.fillAmount = fill/100f; 
    }

    void EndGame()
    {
        // ����� �� ������ �������� �������������� ������, ��������� � ����������� ����
        gameOverScreen.SetActive(true); // ���������� ����� ��������� ����
        Time.timeScale = 0f; // ���������������� ����� (����)
    }
}