using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSlide : MonoBehaviour, IPointerClickHandler
{
    bool isUp = false;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private GameObject background;
    [SerializeField]
    private GameObject menuTitle;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isUp)
        {

            GoUp();

        }
        else
        {
            GoDown();

        }
    }

    public void GoUp()
    {
        isUp = true;
        animator.SetTrigger("Up");
        animator.ResetTrigger("Down");
        background.SetActive(true);
        menuTitle.SetActive(true);
    }
    public void GoDown()
    {
        isUp = false;
        animator.SetTrigger("Down");
        animator.ResetTrigger("Up");
        background.SetActive(false);
        menuTitle.SetActive(false);
    }
   
}
