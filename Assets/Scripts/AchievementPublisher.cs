using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementPublisher : MonoBehaviour {

    Dictionary<string, AchievementSubscriber> subs = new Dictionary<string, AchievementSubscriber>();
    [Header("High Score Achievements")]
    [SerializeField]
    private List<Stats> highScoreList = new List<Stats>();
    private void Awake()
    {
        RegisterSubscriber("highScore", new HighScoreSubscriber());
    }

    public void RegisterSubscriber(string str,AchievementSubscriber sub)
    {
        subs.Add(str, sub);
    }

    public void RunSubscribers()
    {
        foreach (KeyValuePair<string,AchievementSubscriber> item in subs)
        {
            switch (item.Key)
            {
                case "highScore":
                    foreach (Stats highScoreItem in highScoreList)
                    {
                        item.Value.OnAction(highScoreItem);
                    }
                    
                    break;
                default:
                    break;
            }
           
        }
    }
}
