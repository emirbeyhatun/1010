using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
public class SaveSkills : MonoBehaviour {
    
    public static void Save(List<Skills> skillsToSave)
    {
        BinaryFormatter br = new BinaryFormatter();


        for (int i = 0; i < skillsToSave.Count; i++)
        {
            //you can write what ever you want after and before point, like : player.fl or whatever you want
            FileStream stream = new FileStream(Application.persistentDataPath + "/skill"+i+".sav", FileMode.Create);

            SaveSkillsData data = new SaveSkillsData(skillsToSave[i]);
            br.Serialize(stream, data);
            stream.Close(); 
        }

    }


    public static List<SaveSkillsData> Load()
    {
        List<SaveSkillsData> loadList = new List<SaveSkillsData>();
        for (int i = 0; i < 20; i++)
        {
            if (File.Exists(Application.persistentDataPath + "/skill" + i + ".sav"))
            {
                BinaryFormatter br = new BinaryFormatter();
                FileStream stream = new FileStream(Application.persistentDataPath + "/skill" + i + ".sav", FileMode.Open);
                SaveSkillsData data = br.Deserialize(stream) as SaveSkillsData;

                stream.Close();
                loadList.Add(data);
            }
            
        }

        return loadList;

        


    }



}
[Serializable]
public class SaveSkillsData
{
    
    public skills Name ;
    public int Amount = 0;

    public SaveSkillsData(Skills skill)
    {
        
        Name = skill.Name;
        Amount = skill.Amount;
    }
}
