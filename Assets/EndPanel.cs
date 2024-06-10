using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndPanel : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private PlayerData playerData;

    [SerializeField] private TextMeshProUGUI scoreText;
    void Start()
    {
        scoreText.text = playerData.CurrentScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
