using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class StatsManager : MonoBehaviour {
    
    public Stats stats;
    [HideInInspector]
    public int currentScore = 0;
    [HideInInspector]
    public int highScore;
    [HideInInspector]
    public float timeSpent;
    public List<Skills> skills;

    public void Awake()
    {

        SaveLoad.Save(stats);
        SaveSkills.Save(skills);
        GameManager.StatsManager.stats.ClearStats();
            SaveData savedData = SaveLoad.Load();
            highScore = savedData.highScore;
            stats.highScore = savedData.highScore;
            stats.numberOfGames = savedData.numberOfGames;
            stats.totalTimeSpent = savedData.totalTimeSpent;
            stats.gold = savedData.gold;
        stats.unlockedSkills = savedData.unlockedSkills.ToList();
        stats.leftSkill = savedData.leftSkill;
        stats.rightSkill = savedData.rightSkill;
        
        

        List<SaveSkillsData> savedSkillsData = SaveSkills.Load();
        for (int i = 0; i < savedSkillsData.Count; i++)
        {

            skills[i].Name = savedSkillsData[i].Name;
            skills[i].Amount = savedSkillsData[i].Amount;
        }
        

    }
    private void Update()
    {
        SaveLoad.Save(stats);
        SaveSkills.Save(skills);
    }

    public void OnScore(int amount)
    {
        currentScore += amount;
        CheckHighScore();
       
        
    }

    public void CheckHighScore()
    {
        if (highScore < currentScore)
        {
            highScore = currentScore;
            stats.highScore = highScore;
            
        }
    }

    public void ResetScore()
    {
        currentScore = 0;
        timeSpent = 0;
        
    }

    
}
