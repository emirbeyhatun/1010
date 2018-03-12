using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayState : StateBase {
    private float gameStartTime;

    
    public override void OnStart()
    {
        GameManager.GameManagerInstance.slotParent.SetActive(true);
       
       GameManager.SpawnManager.SpaceControl();//spawn first time
        GameManager.UiManager.playPanel.SetActive(true);//previously we disabled this we need to activate this to see scores 

        GameManager.StatsManager.Awake();//clear stats in scriptable object
      

       
        GameManager.UiManager.ResetUiText();//reset ui texts
        gameStartTime = Time.time; //we mark out start time 
       
        GameManager.GameManagerInstance.ClearMatrix();//we need too get our matrix ready for new play

        
        GameManager.UiManager.playPanel.GetComponentInChildren<ButtonSlide>().GoDown();
        for (int i = 0; i < GameManager.UiManager.rePlay.Length; i++)
        {
            GameManager.UiManager.rePlay[i].GetComponent<ButtonControl>().goToState = States.PlayState;
        }
        GameManager.UiManager.ClearStats();//reset curent score and play time 
        


    }

    public override void OnUpdate()
    {




        SkillsManager.Instance.SetBtn();
        GameManager.UiManager.UpdatePlayStateUi();//update ui texts in play state
        SaveLoad.Save(GameManager.StatsManager.stats);//lets save after every point to make sure we dont lose data

       
    }

    public override void OnEnd()
    {
       
        GameManager.SpawnManager.ResetSpawnSlots();

        GameManager.StatsManager.timeSpent = Time.time - gameStartTime;//we substrat our start time from our end time so we get play time
        GameManager.StatsManager.stats.totalTimeSpent += Time.time - gameStartTime;//we sum every play time  since the first time we play to the last to get the total play time
        GameManager.UiManager.playPanel.SetActive(false);//we disabled this to not see play state scores
          
        SaveLoad.Save(GameManager.StatsManager.stats);

        GameManager.GameManagerInstance.slotParent.SetActive(false);
    }

}
