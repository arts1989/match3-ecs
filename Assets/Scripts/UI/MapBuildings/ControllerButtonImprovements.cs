using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ControllerButtonImprovements : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _textElement; // не нужен
    [SerializeField]
    private Button[] _buttons; // кнопки в окне 

    [SerializeField]
    private Question[] _questions; // вопросы

    private int _currentQuestionIndex = 0; //текущий указатель вопросов

    public Question GetCurrentQuestion() //Получить текущий вопрос
    {
        var question = _questions[_currentQuestionIndex];
        return question;
    }

    public void Start()
    {
        InitializeButtons(); // Инициализация кнопок
        PresentCurrentQuestion(); // Представьте текущий вопрос
    }

    private void PresentCurrentQuestion() // Представьте текущий вопрос
    {
        var question = GetCurrentQuestion(); //Получить текущий вопрос
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

    private void InitializeButtons() // Инициализация кнопок
    {
        for (int i = 0; i < _buttons.Length; i++)
        {
            Button button = _buttons[i];

            int buttonIndex = i;
            button.onClick.AddListener(() => ShowResponse(buttonIndex));
        }
    }

    private void ShowResponse(int buttonIndex) // Показать ответ
    {
        var question = GetCurrentQuestion();
        _textElement.text = question.Responses[buttonIndex];

        StartCoroutine(MoveToNextQuestionAfterDelay());
    }

    private IEnumerator MoveToNextQuestionAfterDelay() // Перейти к следующему вопросу после задержки
    {
        yield return new WaitForSeconds(3f);
        _currentQuestionIndex++;
        PresentCurrentQuestion(); // Представьте текущий вопрос
    }
}

[Serializable]
public class Question
{
    public string QuestionText; // текст вопроса - 0000
    public string[] Answers;  // ответы
    public string[] Responses; // ответы222
}

