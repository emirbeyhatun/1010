using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreSubscriber : AchievementSubscriber {


    public override void OnAction(Stats stats)
    {
        if (GameManager.StatsManager.stats.highScore > stats.highScore)
        {

            Debug.Log("Thatsss right ");
        }
    }

   
}
