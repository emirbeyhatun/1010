using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Durability : MonoBehaviour {
    [SerializeField]
    private Text durabilityText;
    [HideInInspector]
    public int durability;
    //public int Durability
    //{
    //    set { durability = value; }
    //    get { return durability; }
    //}
    private int durabilityHolder;
    public void Start()
    {
        durabilityHolder = durability;
        durabilityText.text = durability.ToString();
    }
    public void DecreaseRockDurability(/*int amount*/)
    {
        durability -= 1/*amount*/;
        durabilityText.text = durability.ToString();

        if (durability <= 0)
        {
            GameManager.StatsManager.OnScore(durabilityHolder);
            Destroy(gameObject);
        }
    }
    public void DecreaseBombDurability(/*int amount*/)
    {
        durability -= 1/*amount*/;
        durabilityText.text = durability.ToString();

        if (durability <= 0)
        {

              GameManager.StateManager.SetState(States.EndGameState);
            
        }
    }
}
