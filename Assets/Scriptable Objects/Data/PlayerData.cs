using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName ="Player Data")]
public class PlayerData : ScriptableObject
{
    [SerializeField] private float maxHealth = 3;
    [SerializeField] private float minHealth = 0;
    [SerializeField] private bool resetToMax = true;

    [Header("Event channels")]
    [SerializeField] private FloatEventChannel HealthPercentageChannel;

    private float _currentHealth;
    public float CurrentHealth
    {
        private set
        {
            if(_currentHealth != value)
            {
                _currentHealth = Mathf.Clamp(value, minHealth, maxHealth);
                PublishHealthPercentage((_currentHealth - minHealth )/ (maxHealth - minHealth));
            }
        }
        
        get => _currentHealth;
    }

    public void AddHealth(float delta)
    {
        CurrentHealth += delta;
    }

    public void ReduceHealth(float delta)
    {
        AddHealth(-delta);
    }

    public void ResetHealth()
    {
        CurrentHealth = resetToMax ? maxHealth : minHealth;
    }

    private void OnEnable()
    {
        ResetHealth();
    }

    #region event channel stuff

    private void PublishHealthPercentage(float percentage)
    {
        if(HealthPercentageChannel != null)
            HealthPercentageChannel.Invoke(percentage);
        Debug.Log(percentage);
    }

    #endregion
}

