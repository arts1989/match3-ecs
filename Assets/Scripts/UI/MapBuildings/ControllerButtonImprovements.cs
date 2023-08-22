using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ControllerButtonImprovements : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _textElement;
    [SerializeField]
    private Button[] _buttons;

    [SerializeField]
    private Question[] _questions;

    private int _currentQuestionIndex = 0;

    public Question GetCurrentQuestion()
    {
        var question = _questions[_currentQuestionIndex];
        return question;
    }

    public void Start()
    {
        InitializeButtons();
        PresentCurrentQuestion();
    }

    private void PresentCurrentQuestion()
    {
        var question = GetCurrentQuestion();
        _textElement.text = question.QuestionText;

        for (int i = 0; i < _buttons.Length; i++)
        {
            if (i >= question.Answers.Length)
            {
                _buttons[i].gameObject.SetActive(false);
                continue;
            }

            string ansverText = question.Answers[i];

            _buttons[i].gameObject.SetActive(true);
            _buttons[i].GetComponentInChildren<TMP_Text>().text = ansverText;
        }
    }

    private void InitializeButtons()
    {
        for (int i = 0; i < _buttons.Length; i++)
        {
            Button button = _buttons[i];

            int buttonIndex = i;
            button.onClick.AddListener(() => ShowResponse(buttonIndex));
        }
    }

    private void ShowResponse(int buttonIndex)
    {
        var question = GetCurrentQuestion();
        _textElement.text = question.Responses[buttonIndex];

        StartCoroutine(MoveToNextQuestionAfterDelay());
    }

    private IEnumerator MoveToNextQuestionAfterDelay()
    {
        yield return new WaitForSeconds(3f);
        _currentQuestionIndex++;
        PresentCurrentQuestion();
    }
}

[Serializable]
public class Question
{
    public string QuestionText;
    public string[] Answers;
    public string[] Responses;
}

