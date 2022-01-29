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
        // questionsSeen�� int�̸� 0�� return. float�� ��ȯ �� �Ҽ��� ���� int�� �纯ȯ 
        return Mathf.RoundToInt(correctAnswers / (float)questionsSeen * 100);
    }

}
