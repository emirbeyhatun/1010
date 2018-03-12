using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ButtonPanelControl : MonoBehaviour, IPointerClickHandler
{

  public enum Panels
    {
        Shop,
        Abilities,
        DestroyAbility,
        Theme,
        Menu,
        ModeSelect,
        ResumePlay,
        SetAbilities,
        AbilityAmount


    }
    
    private GameObject panels;
    [SerializeField]
    Panels panelToSet;

    
    private void Start()
    {
       panels=GameObject.FindGameObjectWithTag("Panel");
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        SetPanel(panelToSet);
       
       

    }
    private void DisableAllPanels(GameObject current)
    {
       
        for (int i = 0; i < panels.transform.childCount; i++)
        {
            if(panels.transform.GetChild(i).gameObject!= current)
            panels.transform.GetChild(i).gameObject.SetActive(false);
        }
    }
   
    private void SetPanel(Panels panelToSet)
    {

        
        if(gameObject.CompareTag("Quit")&&( GameManager.StateManager.StateName==States.PlayState|| GameManager.StateManager.StateName == States.PlayStateRockSolid || GameManager.StateManager.StateName == States.PlayStateBomb))
        {
            panelToSet = Panels.ResumePlay;
        }
        else
        {
           // GameManager.UiManager.BackGround.GetComponent<Canvas>().sortingOrder = 10;

        }
        switch (panelToSet)
        {

            case Panels.Shop:

                GameManager.UiManager.shopPanel.SetActive(true);
                
                DisableAllPanels(GameManager.UiManager.shopPanel);
                break;
            case Panels.Abilities:
                GameManager.UiManager.AbilitiesPanel.SetActive(true);
                GameManager.UiManager.UpdateAbilitiesStateUi();
                DisableAllPanels(GameManager.UiManager.AbilitiesPanel);
                break;
            case Panels.DestroyAbility:
                GameManager.UiManager.destroyAbilityPAnel.SetActive(true);
                DisableAllPanels(GameManager.UiManager.destroyAbilityPAnel);
                break;
            case Panels.Theme:
                GameManager.UiManager.ThemePanel.SetActive(true);
                
                DisableAllPanels(GameManager.UiManager.ThemePanel);
                break;
            case Panels.Menu:
                GameManager.UiManager.menuPanel.SetActive(true);

                DisableAllPanels(GameManager.UiManager.menuPanel);
                break;
            case Panels.ModeSelect:
                GameManager.UiManager.ModeSelectPanel.SetActive(true);

                DisableAllPanels(GameManager.UiManager.ModeSelectPanel);
                break;
            case Panels.SetAbilities:
                GameManager.UiManager.SetAbilitiesPanel.SetActive(true);

                DisableAllPanels(GameManager.UiManager.SetAbilitiesPanel);
                break;
            case Panels.AbilityAmount:
                GameManager.UiManager.AbilityAmount.SetActive(true);

                DisableAllPanels(GameManager.UiManager.AbilityAmount);
                break;
            case Panels.ResumePlay:
               
                GameManager.SpawnManager.gameObject.SetActive(true);

               // GameManager.UiManager.BackGround.GetComponent<Canvas>().sortingOrder = -10;


                DisableAllPanels(GameManager.UiManager.ModeSelectPanel);
                GameManager.UiManager.playPanel.SetActive(true);
                DisableAllPanels(GameManager.UiManager.playPanel);
                break;
            default:
                break;
        }
    }
}
