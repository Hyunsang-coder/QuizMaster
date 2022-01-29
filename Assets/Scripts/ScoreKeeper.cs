using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    int correctAnswers = 0;
    int questionsSeen = 0;
    float score = 0;

    public int GetCorrectAnswers()
    {
        return correctAnswers;
    }

    public void IncrementCorrectAnswers()
    {
        correctAnswers++; 
    }

    public int GetSeenQuestions()
    {
        return questionsSeen;
    }

    public void IncrementSeenQuestions()
    {
        questionsSeen++;
    }

    public int CalculateScore()
    {
        // questionsSeen이 int이면 0을 return. float로 변환 후 소수점 값을 int로 재변환 
        return Mathf.RoundToInt(correctAnswers / (float)questionsSeen * 100);
    }

}
