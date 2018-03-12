using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragItemHandler : MonoBehaviour,IDragHandler,IBeginDragHandler,IEndDragHandler,IPointerDownHandler ,IPointerUpHandler{
   
    private Vector3 startPos;
    private GameObject Pool;
    private List<GameObject> childObjects = new List<GameObject>();
    private Vector2 startAnchorMax;
    private Vector2 startAnchorMin;

    private int startSortOrder;
    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
   
    [SerializeField]
    private float anchorMinY = 0.5f;
    [SerializeField]
    private float anchorMaxY = 1.5f;
    [SerializeField]
    private float anchorMinX = 0.5f;
    [SerializeField]
    private float anchorMaxX = 1.5f;
    [SerializeField]
    private float scaleMultiplayerOnClick = 1.5f;
    private bool fitFlag=false;
    float blockCount;




    private void Start()
    {
       
        #region Assignments
        Pool = GameObject.FindGameObjectWithTag("Pool");
        //set our canvas group we need it to enable disable touch
        canvasGroup = GetComponent<CanvasGroup>();

        //RectTransform for anchor settings
        rectTransform = transform.GetComponent<RectTransform>();

        //set our start anchor settings 

        AnchorSet(0,0,1, 1);
       
        startAnchorMax = rectTransform.anchorMax;
        startAnchorMin = rectTransform.anchorMin;

        
        GetChildren();
        #endregion

         blockCount = GetComponentsInChildren<ItemHelper>().Length;
        


    }
    private void GetChildren()
    {
        
        #region GetChildren
        
         
        for (int i = 0; i < transform.GetChild(0). childCount ; i++)
            {
                if (transform.GetChild(0).GetChild(i).gameObject != null && transform.GetChild(0).name != "TouchArea")
                {
                    //find our children and assign them to list
                    childObjects.Add(transform.GetChild(0).GetChild(i).gameObject);

                }



            } 
        #endregion
    }

    private void AnchorReset()
    {
        //after dragging set our initial anchor settings back
        rectTransform.anchorMin = startAnchorMin;
        rectTransform.anchorMax = startAnchorMax;
    }
    private void AnchorSet(float anchorMinX,float anchorMaxX,float anchorMinY, float anchorMaxY)
    {
        rectTransform.anchorMin = new Vector2(anchorMinX, anchorMinY);
        rectTransform.anchorMax = new Vector2(anchorMaxX, anchorMaxY);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        startPos = transform.position;
          if (childObjects.Count > 0)
        {
            #region SetInitialSettings
            for (int i = 0; i < childObjects.Count; i++)
            {

                if (childObjects[i].transform.childCount>0)
                {
                    ItemHelper blockHandler = childObjects[i].transform.GetChild(0).GetComponent<ItemHelper>();
                    //set child's start position so we can return here if we dont put it in the matrix
                    blockHandler.startTransform.position = childObjects[i].transform.GetChild(0).position;

                  //  blockHandler.startTransform.SetParent(childObjects[i].transform.parent.parent);
                   
                    
                    startSortOrder = childObjects[i].transform.GetChild(0).GetComponent<Canvas>().sortingOrder;
                    //This increases our child's Sorting order so that way our child's image will go  above the ones in the scene when we start dragging
                    childObjects[i].transform.GetChild(0).GetComponent<Canvas>().sortingOrder = 7; 
                }


            }

            #endregion


            #region TouchEffects
            //scale child blocks up so player understands that he is dragging the blocks
            //transform.localScale = new Vector3(transform.localScale.x* scaleMultiplayerOnClick, transform.localScale.y * scaleMultiplayerOnClick, transform.localScale.z * scaleMultiplayerOnClick);
            transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);

            //this is important for detail of anchor animation 
            transform.position = Input.mousePosition;
            //anchor animation(not really an animation but kind of feels like that to human eye )
            AnchorSet(anchorMinX,anchorMaxX,anchorMinY,anchorMaxY);
            //enable touch
            canvasGroup.blocksRaycasts = false;
            transform.position = Input.mousePosition;
            #endregion

        }

       
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //disable touch after releasing 
        canvasGroup.blocksRaycasts = true;

        //scale it back to our initial scale

        transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);

        //set our initial anchor settings back
        AnchorReset();
        //place it back
        transform.position = startPos;



        #region ResetSortOrder
        if (childObjects.Count > 0)
        {

            for (int i = 0; i < childObjects.Count; i++)
            {

                if (childObjects[i].transform.childCount>0)
                {

                    childObjects[i].transform.GetChild(0).GetComponent<Canvas>().sortingOrder = startSortOrder; 
                }

            }

        }
        #endregion

    }
    public void OnBeginDrag(PointerEventData eventData)
    {
       
        //this is important for detail of anchor animation  . It will align itself with the onPointerDown one , otherwise it wont be in the right position
        transform.position = Input.mousePosition;
        AnchorSet(anchorMinX, anchorMaxX, anchorMinY, anchorMaxY);
        //enable touch
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //always follows our touch
        transform.position = Input.mousePosition;
    }

   

    public void OnEndDrag(PointerEventData eventData)
    {
        AnchorReset();

        //this counts  empty rows/columns our children collide with
        int count = 0;

        
        if (childObjects.Count > 0)
        {
            #region CountEmptySlots
            for (int i = 0; i < childObjects.Count; i++)
            {
                if (childObjects[i].transform.childCount>0)
                {

                    // get childs container class(holds valuable info for itself)
                    ItemHelper blockHandler = childObjects[i].transform.GetChild(0).GetComponent<ItemHelper>();


                    //if it has collided with a slot and if slot hasn't got any child then increase the count
                    if (blockHandler.slot != null && blockHandler.slot.transform.childCount <= 0)
                    {

                        count = count + 1;

                    } 
                    
                }





            }
            #endregion


            #region FitChildToSlot
            for (int i = 0; i < childObjects.Count; i++)
            {
                if (childObjects[i].transform.childCount>0)
                {
                    // get childs container class(holds valuable info for itself)
                    ItemHelper blockHandler = childObjects[i].transform.GetChild(0).GetComponent<ItemHelper>();
                  
                    if (count == blockCount)
                    {
                        fitFlag = true;
                        //lets set our children's parent to their collided slot
                        blockHandler.transform.SetParent(blockHandler.slot.transform);
                        //we set our children's scale to fit the slot's scale
                        blockHandler.transform.localScale = blockHandler.slot.transform.localScale;
                       
                        if (blockHandler.tag=="Rock")
                        { //set the size of Rock's  text
                            blockHandler.transform.GetChild(0).localScale = new Vector3(3, 3, 3);
                        }
                       

                        //disable collider so nothing bad happens later 
                        blockHandler.gameObject.GetComponent<Collider2D>().enabled = false;
                        GameManager.StatsManager.OnScore(1);
                        transform.SetParent(Pool.transform);
                        


                    }


                   
                    //and set our sorting back to initial value so whoever comes later will be on top of the screen
                    blockHandler.gameObject.GetComponent<Canvas>().sortingOrder = startSortOrder;


                }

                

            } 
            #endregion


        }

        //and we check if Row or Column full , if so we should delete that line to gain points and place that blocks in the slots to the start parent which is a pool object's child right now
        GameManager.GameManagerInstance.RowColumnControl();
        //if we are not fit, we need to go back to where we come
        transform.position = startPos;

        transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);

        //lets disable touch
        canvasGroup.blocksRaycasts= true;

      
        GameManager.AchievementPublisher.RunSubscribers();
        //if there is not enough place in the matrix game ends if there is then game continues
        GameManager.SpawnManager.SpaceControl();
        if (fitFlag)
        {
            SpawnBomb();
        }
       
        if (transform.parent == Pool.transform)
        {
            Destroy(gameObject);
        }
       

    }

    private void SpawnBomb()
    {


          List<GameObject> fullSlots = new List<GameObject>();
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                if (GameManager.StateManager.StateName == States.PlayStateBomb && GameManager.GameManagerInstance.MatrixArray[i, j].transform.childCount>0)
                {
                    if (GameManager.GameManagerInstance.MatrixArray[i, j].transform.GetChild(0).tag == "Bomb")
                    {
                       
                        //decrease bomb timer
                        GameManager.GameManagerInstance.MatrixArray[i, j].transform.GetChild(0).GetComponent<Durability>().DecreaseBombDurability();
                        

                    }

                }



                if (GameManager.GameManagerInstance.MatrixArray[i, j].transform.childCount > 0)
                {
                    fullSlots.Add(GameManager.GameManagerInstance.MatrixArray[i, j]);




                }




            }
            



        }
        int rand = RandomWithProbability.GetRandomValue(new RandomWithProbability.RandomSelection(0, 0, 0.8f), new RandomWithProbability.RandomSelection(1, 1, 0.2f));

        if (rand == 1)
        {

            switch (GameManager.StateManager.StateName)
            {
                case States.PlayStateBomb:
                    // get childs container class(holds valuable info for itself)
                    int rand2 = UnityEngine.Random.Range(0, fullSlots.Count - 1);

                    Transform parent = fullSlots[rand2].transform;
                    Destroy(parent.GetChild(0).gameObject);

                    Debug.Log("Bomb");
                    //lets set our children's parent to their collided slot
                    GameObject clone = (GameObject)Instantiate(GameManager.GameManagerInstance.Bomb, parent.transform);
                    clone.GetComponent<Durability>().durability = UnityEngine.Random.Range(10, 17);
                    clone.GetComponent<Durability>().Start();
                    clone.transform.SetParent(parent);
                    //we set our children's scale to fit the slot's scale
                    clone.transform.localScale = parent.transform.localScale;
                    clone.transform.GetChild(0).localScale = new Vector3(3, 3, 3);
                    return;
                    
            }
        }

    }
    
}
