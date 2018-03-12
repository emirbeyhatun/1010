using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonSkillUnlock : MonoBehaviour, IPointerClickHandler
{

	public int goldUnlockAmount;
    public skills skillToUnlock;
    
    public GameObject backGround;
    
  

    private void Start()
    {
        for (int i = 0; i < GameManager.StatsManager.skills.Count; i++)
        {
            if( SkillsManager.Instance.unlockedSkills.Contains(skillToUnlock))
            {
                
                    backGround.GetComponent<Image>().color = new Color32(92, 193, 101, 255);
                    gameObject.SetActive(false);
                
                
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(goldUnlockAmount <= GameManager.StatsManager.stats.gold)
        {

            SkillsManager.Instance.unlockedSkills.Add(skillToUnlock);
            GameManager.StatsManager.stats.unlockedSkills = SkillsManager.Instance.unlockedSkills.ToList();
            SaveLoad.Save(GameManager.StatsManager.stats);
            SkillsManager.Instance.SetSkills();
          
            backGround.GetComponent<Image>().color = new Color32(92, 193, 101, 255);

            gameObject.SetActive(false);

        }
        else
        {

        }
    }
}
