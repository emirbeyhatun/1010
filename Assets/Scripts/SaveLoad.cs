using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System.Linq;

public class SaveLoad : MonoBehaviour {


    public static void Save(Stats stats)
    {
        BinaryFormatter br = new BinaryFormatter();

        //you can write what ever you want after and before point, like : player.fl or whatever you want
        FileStream stream = new FileStream(Application.persistentDataPath + "/player.sav", FileMode.Create);

        SaveData data = new SaveData(stats);
        br.Serialize(stream, data);
        stream.Close();

    }


    public static SaveData Load()
    {

        if (File.Exists(Application.persistentDataPath + "/player.sav"))
        {
            BinaryFormatter br = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/player.sav", FileMode.Open);
            SaveData data = br.Deserialize(stream) as SaveData;

            stream.Close();
            return data;
        }

        return null;


    }



}
[Serializable]
public class SaveData
{
    public int highScore=0;
    public float totalTimeSpent=0;
    public int numberOfGames=0;
    public int gold = 0;
    public List<skills> unlockedSkills = new List<skills>();
    public skills leftSkill=skills.None;
    public skills rightSkill=skills.None;

    public SaveData(Stats stats)
    {
        highScore = stats.highScore;
        totalTimeSpent = stats.totalTimeSpent;

        numberOfGames=stats.numberOfGames;
        gold = stats.gold;
        unlockedSkills = stats.unlockedSkills.ToList();
        leftSkill = stats.leftSkill;
        rightSkill = stats.rightSkill;
    }
}