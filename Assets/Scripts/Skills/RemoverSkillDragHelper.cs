using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RemoverSkillDragHelper : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler
    {

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

            AnchorSet(0, 0, 1, 1);

            startAnchorMax = rectTransform.anchorMax;
            startAnchorMin = rectTransform.anchorMin;


            GetChildren();
            #endregion

            blockCount = GetComponentsInChildren<ItemHelper>().Length;


        InvokeRepeating("ChangeColor", 0, 0.25f);
    }
    
    private void ChangeColor()
    {
        for (int i = 0; i < childObjects.Count; i++)
        {
            
            if (childObjects[i].transform.childCount > 0)
            {
                childObjects[i].transform.GetChild(0).GetComponent<Image>().color = GameManager.ColorManager.TurnRandomColorFromTheme();
               
            }
        }
    }
    private void GetChildren()
        {

            #region GetChildren


            for (int i = 0; i < transform.GetChild(0).childCount; i++)
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
        private void AnchorSet(float anchorMinX, float anchorMaxX, float anchorMinY, float anchorMaxY)
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

                    if (childObjects[i].transform.childCount > 0)
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
                AnchorSet(anchorMinX, anchorMaxX, anchorMinY, anchorMaxY);
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

                    if (childObjects[i].transform.childCount > 0)
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
                    if (childObjects[i].transform.childCount > 0)
                    {

                        // get childs container class(holds valuable info for itself)
                        ItemHelper blockHandler = childObjects[i].transform.GetChild(0).GetComponent<ItemHelper>();


                        //if it has collided with a slot and if slot hasn't got any child then increase the count
                        if (blockHandler.slot != null )
                        {
                        
                        count = count + 1;

                        }

                    }





                }
            #endregion
           

                #region FitChildToSlot
                for (int i = 0; i < childObjects.Count; i++)
                {
                    if (childObjects[i].transform.childCount > 0)
                    {
                        // get childs container class(holds valuable info for itself)
                        ItemHelper blockHandler = childObjects[i].transform.GetChild(0).GetComponent<ItemHelper>();

                        if (count == blockCount)
                        {
                            
                            

                           


                            //disable collider so nothing bad happens later 
                            

                            if (blockHandler.slot.transform.childCount > 0)
                            {
                            GameObject Remove = (blockHandler.slot.transform.GetChild(0).gameObject);
                            Remove.transform.SetParent(Pool.transform);
                            Destroy(Remove);

                            GameManager.StatsManager.OnScore(1);//for the destroyed ones

                             }
                        GameManager.StatsManager.OnScore(1);//for the empty ones
                        blockHandler.gameObject.GetComponent<Collider2D>().enabled = false;

                        transform.SetParent(Pool.transform);
                        CancelInvoke();



                        }



                        


                    }



                }
                #endregion


            }

           
            //if we are not fit, we need to go back to where we come
            transform.position = startPos;

            transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);

            //lets disable touch
            canvasGroup.blocksRaycasts = true;


            GameManager.AchievementPublisher.RunSubscribers();
            //if there is not enough place in the matrix game ends if there is then game continues
            GameManager.SpawnManager.SpaceControl();
           

            if (transform.parent == Pool.transform)
            {
                Destroy(gameObject);
            }


        }

        

    }
