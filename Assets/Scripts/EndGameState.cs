using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameState : StateBase {

    
    public override void OnStart()
    {
       
        GameManager.UiManager.endGamePanel.SetActive(true);
        GameManager.StatsManager.stats.numberOfGames++;
        GameManager.UiManager.UpdateEndGameStateUi();
        GameManager.StatsManager.stats.gold+= GameManager.StatsManager.currentScore/2;
        GameManager.UiManager.goldGainAmount.text = (GameManager.StatsManager.currentScore / 2).ToString();
    }

    public override void OnUpdate()
    {
       
    }
    public override void OnEnd()
    {
      
        GameManager.UiManager.endGamePanel.SetActive(false);
       
    }

}
