using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour {
    [Header("Play State Ui")]
    [SerializeField]
    private Text playStateHighScore;
    [SerializeField]
    private Text playStateCurrentScore;
    public GameObject playPanel;

    [Header("End State Ui")]
    [SerializeField]
    private Text endGameStateHighScore;
    [SerializeField]
    private Text endGameStateCurrentScore;
    [SerializeField]
    private Text endGameStateTimeSpent;
    [SerializeField]
    private Text endGameStateTotalTimeSpent;
    [SerializeField]
    private  Text endGameStateNumberOfGames;
    public GameObject endGamePanel;

    [Header("Menu State Ui")]
    [SerializeField]
    private Text menuStateHighScore;
    public GameObject menuPanel;

    [Header("Shop State Ui")]
    [SerializeField]
    
    public GameObject shopPanel;

    [Header(" Abilities State Ui")]
    [SerializeField]
    private Text AbilitiesStateCoins;
    public GameObject AbilitiesPanel;

    [Header("Destroy Ability State Ui")]
    [SerializeField]
    private Text destroyAbilityStateCoins;
    
    public GameObject destroyAbilityPAnel;
    [Header("Theme  State Ui")]
    [SerializeField]
    public GameObject ThemePanel;

    [Header("Mode Select  State Ui")]
    [SerializeField]
    public GameObject ModeSelectPanel;
    [Header("Replay buttons   Ui")]
    
    public GameObject[] rePlay;
    [Header("BackGround   Ui")]
    
    public GameObject BackGround;

    [Header("Set Abilities")]
    public GameObject SetAbilitiesPanel;

    [Header("Ability Amount Abilities")]
    public GameObject AbilityAmount;
    [SerializeField]
    private Text AbilitiesAmountCoins;
    [Header("Gold Gain  ")]
    public Text goldGainAmount;

    public void ClearStats()
    {
        GameManager.StatsManager.ResetScore();

    }
    public void ResetUiText()
    {

        playStateHighScore.text=GameManager.StatsManager.stats.highScore.ToString();
        endGameStateHighScore.text = GameManager.StatsManager.stats.highScore.ToString();
        menuStateHighScore.text = GameManager.StatsManager.stats.highScore.ToString();
        playStateCurrentScore.text = "0";
        endGameStateCurrentScore.text = "0";
        endGameStateTimeSpent.text = "0";
        endGameStateTotalTimeSpent.text = GameManager.StatsManager.stats.totalTimeSpent.ToString();
        endGameStateNumberOfGames.text = GameManager.StatsManager.stats.numberOfGames.ToString();
    }

   

    private string SecondsToTime(float seconds)
    {
        
            TimeSpan time = TimeSpan.FromSeconds(seconds);

           
            string stringTime = string.Format("{0:D2}:{1:D2}:{2:D2}", time.Hours, time.Minutes, time.Seconds);

            return stringTime;
        
    }

  

    public void UpdatePlayStateUi()
    {
        // highScore[0].text = GameManager.StatsManager.stats.highScore.ToString();
       playStateHighScore.text = GameManager.StatsManager.stats.highScore.ToString();
        playStateCurrentScore.text = GameManager.StatsManager.currentScore.ToString();
        
       

    }
    public void UpdateEndGameStateUi()
    {
        endGameStateHighScore.text = GameManager.StatsManager.stats.highScore.ToString();
        endGameStateCurrentScore.text = GameManager.StatsManager.currentScore.ToString();

        endGameStateTimeSpent.text = SecondsToTime(GameManager.StatsManager.timeSpent);
        endGameStateTotalTimeSpent.text = SecondsToTime(GameManager.StatsManager.stats.totalTimeSpent);
        endGameStateNumberOfGames.text = GameManager.StatsManager.stats.numberOfGames.ToString();



    }
    public void UpdateMenuStateUi()
    {
        menuStateHighScore.text = GameManager.StatsManager.stats.highScore.ToString();
       
    }
    public void UpdateAbilitiesStateUi()
    {
        AbilitiesStateCoins.text = GameManager.StatsManager.stats.gold.ToString();
        destroyAbilityStateCoins.text = GameManager.StatsManager.stats.gold.ToString();
        AbilitiesAmountCoins.text = GameManager.StatsManager.stats.gold.ToString();
    }


}
