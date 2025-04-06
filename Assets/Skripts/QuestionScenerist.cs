using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestionScenerist : MonoBehaviour
{
    [SerializeField] private Image _corrImage;
    [SerializeField] private Color _rightColor, _wrongColor;
    [SerializeField] private Slider _ProgressSlider;
    [SerializeField] private GameControls _gameControls;
    [SerializeField] private Transform _buttonsParent;
    [SerializeField] private TMP_Text _questionTxt;
    [SerializeField] private Sprite[] CorrSprites;
    [SerializeField] private QuestionData[] QuestionDatas;
    private GameObject[] AnswerButtons = new GameObject[4];

    private void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            AnswerButtons[i] = _buttonsParent.GetChild(i).gameObject;
        }
        FillQstnData();
        _ProgressSlider.maxValue = QuestionDatas.Length;
    }

    private void FillQstnData()
    {
        _ProgressSlider.value = PlayerData.CurrentQuestion;

        _gameControls.UpdateQstnTxt(QuestionDatas.Length);

        QuestionData currentData = QuestionDatas[PlayerData.CurrentQuestion];

        _questionTxt.text = currentData.Question;

        int[] Answers = new int[4];
        for (int i = 0; i < 4; i++)
        {
            Answers[i] = int.MaxValue;
        }
        Answers[UnityEngine.Random.Range(0, 4)] = currentData.RightAnswer;
        for (int i = 0; i < 4; i++)
        {
            if (Answers[i] == int.MaxValue)
            {
                Answers[i] = UnityEngine.Random.Range(currentData.RandomRange.x, currentData.RandomRange.y);
            }
            Debug.Log(Answers[i]);
        }
        Answers = SetUnique(Answers, currentData.RightAnswer);
        for (int i = 0; i < 4; i++)
        {
            AnswerButtons[i].GetComponentInChildren<TMP_Text>().text = Answers[i].ToString();
        }

    }

    private int[] SetUnique(int[] array, int rightAnswer)
    {
        Array.Sort(array);
        bool rightIsFound = false;
        for (int i = 0; i < 4; i++)
        {
            if (array[i] != rightAnswer || rightIsFound)
            {
                array[i] += i;
            }
            else
            {
                rightIsFound = true;
            }
        }
        return array;
    }

    public void CheckAnswer(GameObject thisBtn)
    {
        StartCoroutine(CorrectnessTimer(thisBtn));
    }

    private IEnumerator CorrectnessTimer(GameObject thisBtn)
    {
        
        QuestionData currentData = QuestionDatas[PlayerData.CurrentQuestion];
        if (thisBtn.GetComponentInChildren<TMP_Text>().text == currentData.RightAnswer.ToString())
        {
            PlayerData.RightAnswers++;
            _corrImage.sprite = CorrSprites[1];
            _corrImage.color = _rightColor;
        }
        else
        {
            PlayerData.WrongAnswers++;
            _corrImage.sprite = CorrSprites[2];
            _corrImage.color = _wrongColor;
        }

        yield return new WaitForSeconds(1);

        if (QuestionDatas.Length - 1 > PlayerData.CurrentQuestion)
        {
            PlayerData.CurrentQuestion++;
            FillQstnData();
        }
        else
        {
            _gameControls.FillResults();
        }

        _corrImage.sprite = CorrSprites[0];
        _corrImage.color = Color.white;
    }
}

[Serializable]
public struct QuestionData
{
    public string Question;
    public int RightAnswer;
    public Vector2Int RandomRange;
}