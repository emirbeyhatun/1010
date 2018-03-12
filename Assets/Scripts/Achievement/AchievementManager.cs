using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour {

    public GameObject achievementPrefab;
	void Start () {
        //CreateAchievement("General");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CreateAchievement(string category,string title,string description,int point)
    {
        GameObject achievement = (GameObject)Instantiate(achievementPrefab);
        SetAchievementInfo(category, achievement,title,description,point);
    }
    public void SetAchievementInfo(string category,GameObject achievement, string title, string description, int point)
    {
        achievement.transform.SetParent(GameObject.Find(category).transform);
        achievement.transform.localScale = new Vector3(1, 1, 1);
    }
}
