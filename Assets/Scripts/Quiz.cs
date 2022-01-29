using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using Random = UnityEngine.Random;

public class Quiz : MonoBehaviour
{
    [Header("Question")]
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] List<QuestionSO> questionList = new List<QuestionSO>();
    QuestionSO currentQuestion;

    [Header("Answer")]
    [SerializeField] GameObject[] answerButtons;
    [SerializeField] int correctAnswerIndex;
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;

    [Header("Timer")]
    [SerializeField] Image timerImage;
    Timer timer;
    bool hasAnsweredEarly;

    [Header("Scoring")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;


    void Start()
    {
        timer = FindObjectOfType<Timer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }


    void Update()
    {
        timerImage.fillAmount = timer.fillFraction;
        if (timer.loadNextQuestion)
        {
            hasAnsweredEarly = false;
            GetNextQuestion();
            timer.loadNextQuestion = false;
        }
        else if (!hasAnsweredEarly && !timer.isAnsweringQuestion)
        {
            return;
            //DisplayAnswer(-1);
        }
    }

    public void OnAnswerSelected(int index)
    {
        hasAnsweredEarly = true;
        DisplayAnswer(index);

        SetButtonState(false);
        timer.CancelTimer();
        scoreText.text = "Score: " + scoreKeeper.CalculateScore() + "%";
    }

    private void DisplayAnswer(int index)
    {
        Image buttonImage;
        correctAnswerIndex = currentQuestion.GetCorrectAnswer();
        if (index == correctAnswerIndex)
        {
            questionText.text = "Correct!";
            buttonImage = answerButtons[index].GetComponentInChildren<Image>();
            buttonImage.sprite = correctAnswerSprite;
            scoreKeeper.IncrementCorrectAnswers();
        }
        else
        {
            questionText.text = "The correct answer is:\n" + (correctAnswerIndex + 1);
            buttonImage = answerButtons[correctAnswerIndex].GetComponentInChildren<Image>();
            buttonImage.sprite = correctAnswerSprite;
        }
    }

    private void GetRandomQuestion()
    {
        int randomIndex = Random.Range(0, questionList.Count);
        currentQuestion = questionList[randomIndex];
        if (questionList.Contains(currentQuestion))
        {
            questionList.Remove(currentQuestion);
        }
    }

    void GetNextQuestion()
    {
        if (questionList.Count > 0)
        {
            GetRandomQuestion();
            SetButtonState(true);
            SetDefaultButtonSprites();
            DisplayQuestion();
            scoreKeeper.IncrementSeenQuestions();
        }
        else return;
    }

    private void SetDefaultButtonSprites()
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Image buttonImage;
            buttonImage = answerButtons[i].GetComponentInChildren<Image>();
            buttonImage.sprite = defaultAnswerSprite;
        }
    }

    void DisplayQuestion()
    {
        questionText.text = currentQuestion.GetQeustion();

        for (int i = 0; i < answerButtons.Length; i++)
        {
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = currentQuestion.GetAnswer(i);
        }
    }

    void SetButtonState(bool state)
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Button button = answerButtons[i].GetComponent<Button>();
            button.interactable = state;
        }
    }

}
