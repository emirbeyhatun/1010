using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ButtonControl : MonoBehaviour,IPointerClickHandler {

    public States goToState;
   
    public void OnPointerClick(PointerEventData eventData)
    {

        
            GameManager.StateManager.SetState(goToState);
        
       
        
    }

  
}
