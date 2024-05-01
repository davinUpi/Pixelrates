using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [Header("Main Data SO")]
    [SerializeField] private PlayerData playerData;

    private TextMeshProUGUI _textMeshPro;
    private void Awake()
    {
        _textMeshPro = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        playerData.LatestScore += UpdateScore;
    }

    public void UpdateScore(int value) =>
        _textMeshPro.text = $"Score: {value}";

    private void OnDestroy()
    {
        playerData.LatestScore -= UpdateScore;
    }

}
