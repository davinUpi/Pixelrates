
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName ="Player Data")]
public class PlayerData : ScriptableObject
{
    [SerializeField] private float maxHealth = 3;
    [SerializeField] private float minHealth = 0;
    [SerializeField] private bool resetToMax = true;

    public event Action<float> LatestHealthValue;
    public event Action<float> LatestHealthPercentage;
    public event Action<int> LatestScore;

    private float _currentHealth;
    public float CurrentHealth
    {
        private set
        {
            if(_currentHealth != value)
            {
                _currentHealth = Mathf.Clamp(value, minHealth, maxHealth);

                LatestHealthValue?.Invoke(_currentHealth);
                LatestHealthPercentage?.Invoke((_currentHealth - minHealth) /( maxHealth - minHealth));
                Debug.Log(_currentHealth);

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
                LatestScore?.Invoke(_currentScore);
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
        ResertScore();
    }

}

