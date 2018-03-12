using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemHelper : MonoBehaviour{
    [HideInInspector]
    public  GameObject slot;
    
    [HideInInspector]
    public Transform startParent;
    [HideInInspector]
    public Transform startTransform;
    

    private void Start()
    {

       
        startTransform = transform;
        if(transform.parent.parent!=null)
        startParent = transform.parent.parent;
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        slot = collision.gameObject;
        


    }
    private void OnTriggerExit2D(Collider2D collision)
    {

        slot = null;
        

    }

    


}
