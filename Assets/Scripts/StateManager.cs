using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum States
{
    PlayState,
    MenuState,
    EndGameState,
    PlayStateRockSolid,
    PlayStateBomb

}
public class StateManager : MonoBehaviour {

    private StateBase currentState;
    public StateBase State
    {
        get { return currentState; }
    }
    private States stateName;
    public States StateName
    {
        get { return stateName ; }
    }

    public Dictionary<States,StateBase> states  = new Dictionary<States, StateBase>();
    
    private void Awake()
    {
        states.Add(States.PlayState, new PlayState());
        states.Add(States.MenuState, new MenuState());
        states.Add(States.EndGameState, new EndGameState());
        states.Add(States.PlayStateRockSolid, new PlayStateRockSolid());
        states.Add(States.PlayStateBomb, new PlayStateBomb());
        
    }

    void Start()
    {
        SetState(States.MenuState);
    }
    void Update()
    {
       
        if (currentState != null)
        {
           
            currentState.OnUpdate();
        }
    }
    
    
    public void SetState(States newState)
    {
       
        if (currentState != null)
        {
            currentState.OnEnd();
        }
        
        currentState = states[newState];
        stateName = newState;
        if (currentState != null)
        {
            currentState.OnStart();
           
        }
        
            
       
    }
   
}
