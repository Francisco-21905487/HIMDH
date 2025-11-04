using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private Image currentHealthBar;
    [SerializeField] private Image totalHealthBar;
    [SerializeField] private int value;


    private void Start()
    {
        totalHealthBar.fillAmount = playerHealth.currentHealth / value;
    }

    private void Update()
    {
        currentHealthBar.fillAmount = playerHealth.currentHealth / value;
    }
}
