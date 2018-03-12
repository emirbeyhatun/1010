using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuState : StateBase {

  
    public override void OnStart()
    {
       
        GameManager.UiManager.menuPanel.SetActive(true);
           GameManager.UiManager.UpdateMenuStateUi();
       
       
    }

    public override void OnUpdate()
    {
      

    }

    public override void OnEnd()
    {
       
        GameObject panels = GameObject.FindGameObjectWithTag("Panel");
        for (int i = 0; i < panels.transform.childCount; i++)
        {
             panels.transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
