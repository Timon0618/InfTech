using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameControls : MonoBehaviour
{
    [SerializeField] private GameObject _resultCanvas;
    [SerializeField] private GameObject _menuCanvas;
    [SerializeField] private TMP_Text _RightTxt;
    [SerializeField] private TMP_Text _WrongTxt;
    [SerializeField] private TMP_Text _MarkTxt;
    [SerializeField] private TMP_Text _QuestionNumTxt;

    public void FillResults()
    {
        gameObject.SetActive(false);
        _resultCanvas.SetActive(true);

        int questionsCount = PlayerData.RightAnswers + PlayerData.WrongAnswers;
        int mark = Mathf.Clamp(Mathf.RoundToInt(5 / ((float) questionsCount / PlayerData.RightAnswers)), 1, 5);

        _RightTxt.text = "Верно: " + PlayerData.RightAnswers;
        _WrongTxt.text = "Ошибок: " + PlayerData.WrongAnswers;
        _MarkTxt.text = "Оценка: " + mark;
    }

    public void UpdateQstnTxt(int questionsCount)
    {
        _QuestionNumTxt.text = $"Вопрос {PlayerData.CurrentQuestion + 1}/{questionsCount}";
    }

    public void BackBtnClick()
    {
        gameObject.SetActive(false);
        _menuCanvas.SetActive(true);
    }
}
