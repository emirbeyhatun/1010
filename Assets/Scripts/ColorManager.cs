using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ColorTheme
{
    CLASSIC,
    PASTEL
}
public class ColorManager : MonoBehaviour {

    public ColorTheme activeTheme;

    public Color[] classicTheme;
    public Color[] pastelTheme;

    public Color TurnRandomColorFromTheme()
    {
       
        Color temp;
        switch (activeTheme)
        {
            case ColorTheme.CLASSIC:
                int rand = Random.Range(0, classicTheme.Length);
              
                temp = classicTheme[rand];
                break;
            case ColorTheme.PASTEL:
                int rand2 = Random.Range(0, classicTheme.Length);
                temp = pastelTheme[rand2];
                break;
            default:
                temp = Color.gray;
                break;
        }

        return temp;
    }
}
