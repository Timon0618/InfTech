using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuControls : MonoBehaviour
{
    [SerializeField] private GameObject _gameCanvas;
    [SerializeField] private Slider _musicSlider;
    public void PlayBtnClick()
    {
        gameObject.SetActive(false);
        _gameCanvas.SetActive(true);
    }

    public void ExitBtnClick()
    {
        Application.Quit();
    }
}