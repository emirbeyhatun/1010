using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragSkillHandler : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler
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
    [HideInInspector]
    public bool flag=true;
    
    public skills skill;
    public Transform skillStartParent;


    private void Start()
    {
      
        #region Assignments
        Pool = GameObject.FindGameObjectWithTag("Pool");
        //set our canvas group we need it to enable disable touch
      //  canvasGroup = GetComponent<CanvasGroup>();

        //RectTransform for anchor settings
        rectTransform = transform.GetComponent<RectTransform>();

        //set our start anchor settings 

      //  AnchorSet(0, 0, 1, 1);

        startAnchorMax = rectTransform.anchorMax;
        startAnchorMin = rectTransform.anchorMin;


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
        if (flag)
        {
            startPos = transform.position;

            ItemHelper blockHandler = transform.GetComponent<ItemHelper>();
            //set child's start position so we can return here if we dont put it in the matrix
            blockHandler.startTransform.position = blockHandler.transform.position;

            // startSortOrder = childObjects[i].transform.GetChild(0).GetComponent<Canvas>().sortingOrder;
            //This increases our child's Sorting order so that way our child's image will go  above the ones in the scene when we start dragging
            //  childObjects[i].transform.GetChild(0).GetComponent<Canvas>().sortingOrder = 7;



            #region TouchEffects
            //scale child blocks up so player understands that he is dragging the blocks
            //transform.localScale = new Vector3(transform.localScale.x* scaleMultiplayerOnClick, transform.localScale.y * scaleMultiplayerOnClick, transform.localScale.z * scaleMultiplayerOnClick);
            transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
            if(transform.childCount>1)
            transform.GetChild(1).gameObject.SetActive(true);

            //this is important for detail of anchor animation 
            transform.position = Input.mousePosition;
            //anchor animation(not really an animation but kind of feels like that to human eye )
            // AnchorSet(anchorMinX, anchorMaxX, anchorMinY, anchorMaxY);
            //enable touch
            //canvasGroup.blocksRaycasts = false;
            transform.position = Input.mousePosition;
            #endregion 
        }

        


    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (flag)
        {
            //disable touch after releasing 
            //canvasGroup.blocksRaycasts = true;

            //scale it back to our initial scale

            if (transform.parent == skillStartParent)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(1.7f, 1.61f, 1);
                
               
            }

            //set our initial anchor settings back
            // AnchorReset();
            //place it back
            transform.position = startPos;
            if (transform.childCount > 1)
                transform.GetChild(1).gameObject.SetActive(false);//text

            GetComponent<Canvas>().sortingOrder = 20; 


        }
           

    }
    public void OnBeginDrag(PointerEventData eventData)
    {

        if (flag)
        {
            //this is important for detail of anchor animation  . It will align itself with the onPointerDown one , otherwise it wont be in the right position
            transform.position = Input.mousePosition;
            // AnchorSet(anchorMinX, anchorMaxX, anchorMinY, anchorMaxY);
            //enable touch
            // canvasGroup.blocksRaycasts = false;
            GetComponent<Canvas>().sortingOrder = 20; 
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (flag)
        {
            //always follows our touch
            transform.position = Input.mousePosition; 
        }
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (flag)
        {
            //AnchorReset();
            GetComponent<Canvas>().sortingOrder = 14;



            // get childs container class(holds valuable info for itself)
            ItemHelper skillHandler = transform.GetComponent<ItemHelper>();


            //if it has collided with a slot and if slot hasn't got any child then increase the count
            if (skillHandler.slot != null /*&& skillHandler.slot.transform.childCount <= 0*/)
            {
                if (skillHandler.slot.transform.childCount == 1)
                {
                    DragSkillHandler oldSkill = skillHandler.slot.transform.GetChild(0).GetComponent<DragSkillHandler>();
                    oldSkill.transform.SetParent(oldSkill.skillStartParent);
                    oldSkill.GetComponent<Collider2D>().enabled = true;
                    oldSkill.transform.localScale = skillStartParent.localScale;
                    oldSkill.transform.localPosition = skillStartParent.localPosition;
                    oldSkill.flag = true;
                }


                //lets set our children's parent to their collided slot
                transform.SetParent(skillHandler.slot.transform);
                transform.GetComponent<Collider2D>().enabled = false;
                flag = false;
                //we set our children's scale to fit the slot's scale
                transform.localScale = new Vector3(1.7f, 1.61f, 1);


            }
            else
            {
                if (transform.parent == skillStartParent)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                }
                //if we are not fit, we need to go back to where we come
                transform.position = startPos;


            }






            if (transform.parent.CompareTag("Left"))
            {
                SkillsManager.Instance.skillLeft = skill;
                
            }
            else if (transform.parent.CompareTag("Right"))
            {
                SkillsManager.Instance.skillRight = skill;
            }

        }

    }
    
}