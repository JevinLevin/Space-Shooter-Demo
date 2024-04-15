using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI scoreText;
    
    public static Score Instance;

    private int score;
    public int PlayerScore
    {
        get => score;
        set
        {
            score = value;
            UpdateUI();
        }
    }

    private void Awake()
    {
        Instance = this;
    }

    private void UpdateUI()
    {
        scoreText.text = score.ToString();
    }
    
    
}
