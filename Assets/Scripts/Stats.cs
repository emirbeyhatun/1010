using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName ="Stats")]
public class Stats : ScriptableObject {

    public int highScore = 0;
    public float totalTimeSpent = 0;
    public int numberOfGames = 0;
    public int gold = 0;
    public List<skills> unlockedSkills= new List<skills>();
    public skills leftSkill=skills.None;
    public skills rightSkill=skills.None;

    public void ClearStats()
    {
        highScore = 0;
        totalTimeSpent = 0;
        numberOfGames = 0;
        unlockedSkills.Clear();


        gold = 0;

    }
}
