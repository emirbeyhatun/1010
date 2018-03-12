using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public enum BackgroundColor
{
    White,
    Black,
    Gray
}
public class ButtonThemeChange : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
     BlockTheme theme;
    [SerializeField]
    BackgroundColor backGroundColor;
    [SerializeField]
    GameObject Background;
    [SerializeField]
    GameObject Slider;


    public void OnPointerClick(PointerEventData eventData)
    {
        GameManager.SpawnManager.theme = theme;
        switch (backGroundColor)
        {
            case BackgroundColor.White:
                Background.GetComponent<Image>().color = Color.white;
                Slider.GetComponent<Image>().color = Color.white;
                break;
            case BackgroundColor.Black:
                Background.GetComponent<Image>().color = Color.black;
                Slider.GetComponent<Image>().color = new Color32(73, 73, 73, 255);
                break;
            case BackgroundColor.Gray:
                Background.GetComponent<Image>().color = Color.gray;
                Slider.GetComponent<Image>().color = Color.gray;
                break;
            default:
                break;
        }
        
    }
}
