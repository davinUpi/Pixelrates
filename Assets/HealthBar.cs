using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [Header("Main Data SO")]
    [SerializeField] private PlayerData playerData;

    [Header("Fill Bar")]
    [SerializeField] private Image healthBarFilling;

    void Start()
    {
        playerData.LatestHealthPercentage += UpdateHealthBarFilling;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateHealthBarFilling(float value) => healthBarFilling.fillAmount = value;

    private void OnDestroy()
    {
        playerData.LatestHealthPercentage -= UpdateHealthBarFilling;
    }
}
