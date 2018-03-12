using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum skills
{
    None,
    TaserOne,
    TaserTwo,
    SpreadOne,
    SpreadTwo,
    Bomb,
    ReSpawn,
    Drager
}

public class ButtonForSkills : MonoBehaviour,IPointerClickHandler {
   

   
    

    public skills skill;
    public Text amountTxt;
    private void Update()
    {
        for (int i = 0; i < GameManager.StatsManager.skills.Count; i++)
                {
                    if (GameManager.StatsManager.skills[i].Name == skill)
                    {
                        if (GameManager.StatsManager.skills[i].Amount > 0)
                        {
                            
                            
                            amountTxt.text = GameManager.StatsManager.skills[i].Amount.ToString();
                        }
                        
                        
                    }
                }
    }
    public void SetSkills()
    {
        if (CompareTag("Left"))
        {
            skill = SkillsManager.Instance.skillLeft;
            GetComponent<Image>().sprite = SkillsManager.Instance.imageTable[(int)skill];
        }
        if (CompareTag("Right"))
        {
            skill = SkillsManager.Instance.skillRight;
            GetComponent<Image>().sprite = SkillsManager.Instance.imageTable[(int)skill];
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        for (int i = 0; i < GameManager.StatsManager.skills.Count; i++)
        {
            if (GameManager.StatsManager.skills[i].Name == skill)
            {
                if (GameManager.StatsManager.skills[i].Amount > 0)
                {
                    switch (skill)
                    {
                        case skills.TaserOne:

                            SkillsManager.Instance.RDFF(ref GameManager.GameManagerInstance.MatrixArray, 5);

                            break;
                        case skills.TaserTwo:

                            SkillsManager.Instance.RDFF(ref GameManager.GameManagerInstance.MatrixArray, 10);

                            break;
                        case skills.SpreadOne:

                            SkillsManager.Instance.RD(ref GameManager.GameManagerInstance.MatrixArray, 20);

                            break;
                        case skills.SpreadTwo:

                            SkillsManager.Instance.RD(ref GameManager.GameManagerInstance.MatrixArray, 30);

                            break;
                        case skills.Bomb:

                            SkillsManager.Instance.ClearAllMatrix(ref GameManager.GameManagerInstance.MatrixArray);

                            break;
                        case skills.ReSpawn:

                            SkillsManager.Instance.ReloadSpawners();

                            break;
                        case skills.Drager:

                            SkillsManager.Instance.SquareBomb();

                            break;
                        default:
                            break;
                    }
                    
                    GameManager.StatsManager.skills[i].Amount--;
                    
                }


            }
        }
       
    }
}
