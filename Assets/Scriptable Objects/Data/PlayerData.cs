using System;
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
    [SerializeField] private BoolEventChannel PlayerLiveStatusChannel;
    [SerializeField] private IntEventChannel ScoreUpdateChannel;

    private float _currentHealth;
    public float CurrentHealth
    {
        private set
        {
            if(_currentHealth != value)
            {
                _currentHealth = Mathf.Clamp(value, minHealth, maxHealth);
                PublishHealthPercentage((_currentHealth - minHealth )/ (maxHealth - minHealth));

                PublishPlayerLiveStatus(_currentHealth > minHealth);

            }
        }
        
        get => _currentHealth;
    }

    private int _currentScore;
    public int CurrentScore
    {
        private set
        {
            if(_currentScore != value)
            {
                _currentScore = value;
                PublishScoreUpdate(_currentScore);

                Debug.Log(_currentScore);
            }
        }

        get =>  _currentScore;
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

    public void AddScore(int value)
    {
        CurrentScore += Math.Abs(value);
    }

    public void ResertScore()
    {
        CurrentScore = 0;
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
    }

    private void PublishPlayerLiveStatus(bool status)
    {
        if(PlayerLiveStatusChannel != null) 
            PlayerLiveStatusChannel.Invoke(status);
    }

    private void PublishScoreUpdate(int value)
    {
        if (ScoreUpdateChannel != null) 
            ScoreUpdateChannel.Invoke(_currentScore);
    }

    #endregion
}

