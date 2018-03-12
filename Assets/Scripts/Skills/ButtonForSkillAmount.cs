using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonForSkillAmount : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private int AmountPrice;
    [SerializeField]
    private skills skill;
    private int Amount=0;
    [SerializeField]
    private Text amountTxt;
    private void Start()
    {
        
            if (SkillsManager.Instance.unlockedSkills.Contains(skill))
            {
                for (int i = 0; i < GameManager.StatsManager.skills.Count; i++)
            {
                if (GameManager.StatsManager.skills[i].Name == skill)
                {
                    Amount = GameManager.StatsManager.skills[i].Amount;
                    amountTxt.text = Amount.ToString();
                }
                     

            }
        }
        
    }
    private void Update()
    {
        for (int i = 0; i < GameManager.StatsManager.skills.Count; i++)
        {
            
                
            
           
            if (SkillsManager.Instance.unlockedSkills.Contains(skill))
            {
                
                GetComponent<Image>().enabled = true;
                transform.GetChild(0).gameObject.SetActive(true);


            }
            else
            {
                GetComponent<Image>().enabled = false;
                transform.GetChild(0).gameObject.SetActive(false);

            }
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (GameManager.StatsManager.stats.gold > AmountPrice)
        {
            GameManager.StatsManager.stats.gold -= AmountPrice;
           

            for (int i = 0; i < GameManager.StatsManager.skills.Count; i++)
            {
                if (GameManager.StatsManager.skills[i].Name==skill)
                {

                    GameManager.StatsManager.skills[i].Amount++;
                      Amount=GameManager.StatsManager.skills[i].Amount;
                    amountTxt.text = Amount.ToString();
                }
            }


        }
        
    }

    
}
