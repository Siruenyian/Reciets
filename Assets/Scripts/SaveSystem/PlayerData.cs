using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    // Dictionary to hold levels and corresponding scores
    public Dictionary<int, float> LevelsAndScores { get; private set; }

    // Player information


    // Constructor
    public PlayerData()
    {
        LevelsAndScores = new Dictionary<int, float>();

    }

    // Method to add or update level score
    public void SetScore(int level, float score)
    {
        LevelsAndScores[level] = score;
    }

    // Method to get score for a specific level
    public float GetScore(int level)
    {
        return LevelsAndScores.ContainsKey(level) ? LevelsAndScores[level] : 0;
    }
}

