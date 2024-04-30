using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    private TextMeshProUGUI _textMeshPro;
    private void Awake()
    {
        _textMeshPro = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateScore(int value)
    {
        _textMeshPro.text = $"Score: {value}";
    }
}
