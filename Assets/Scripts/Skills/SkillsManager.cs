using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SkillsManager : MonoBehaviour {
    struct Index
    {
       public int row, column;
    }
    private static SkillsManager instance;
    public static SkillsManager Instance
    {
        get { return instance; }
    }
    
   
    public GameObject DragItem;
    [HideInInspector]
    public List<skills> unlockedSkills = new List<skills>();
    [HideInInspector]
    public skills skillLeft;
    [HideInInspector]
    public skills skillRight;
    [SerializeField]
    private Transform LeftSkill;
    [SerializeField]
    private Transform RightSkill;
    [SerializeField]
    private GameObject skillObject;
    [SerializeField]
    private GameObject RightBtn;
    [SerializeField]
    private GameObject LeftBtn;
    [SerializeField]
    private Transform ParentSkill;
    [SerializeField]
    private List<Sprite> skillImages = new List<Sprite>();
    public Dictionary<int, Sprite> imageTable = new Dictionary<int, Sprite>();

    private void Update()
    {
        GameManager.StatsManager.stats.rightSkill = skillRight;
        GameManager.StatsManager.stats.leftSkill = skillLeft;
    }
    private void Start()
    {

       skillLeft = GameManager.StatsManager.stats.leftSkill;
        skillRight = GameManager.StatsManager.stats.rightSkill;
        unlockedSkills = GameManager.StatsManager.stats.unlockedSkills.ToList();
        for (int i = 0; i < skillImages.Count; i++)
        {
          
            imageTable.Add(i, skillImages[i]);
        }
        

           
        SetSkills();

    }
    public void SetBtn()
    {
        RightBtn.GetComponent<ButtonForSkills>().SetSkills();
        LeftBtn.GetComponent<ButtonForSkills>().SetSkills();
    }
    public void SetSkills()
    {


        if (ParentSkill.childCount != unlockedSkills.Count)
        {
            ResetSkills();
            for (int i = 0; i < unlockedSkills.Count; i++)
            {
                GameObject Object = (GameObject)Instantiate(skillObject, ParentSkill);

                Object.transform.GetChild(0).GetComponent<DragSkillHandler>().skill = unlockedSkills[i];

                Object.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = imageTable[(int)Object.transform.GetChild(0).GetComponent<DragSkillHandler>().skill];
                Object.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = Object.transform.GetChild(0).GetComponent<DragSkillHandler>().skill.ToString();

                if (skillLeft == Object.transform.GetChild(0).GetComponent<DragSkillHandler>().skill)
                {
                    Object.transform.GetChild(0).transform.GetComponent<Collider2D>().enabled = false;
                    Object.transform.GetChild(0).transform.GetComponent<DragSkillHandler>().flag = false;
                    Object.transform.GetChild(0).localScale = new Vector3(1.7f, 1.61f, 1);
                    Object.transform.GetChild(0).transform.GetComponent<Canvas>().sortingOrder = 14;
                    Object.transform.GetChild(0).SetParent(LeftSkill,false);
                   


                }
                else
                 if (skillRight == Object.transform.GetChild(0).GetComponent<DragSkillHandler>().skill)
                {
                    Object.transform.GetChild(0).transform.GetComponent<Collider2D>().enabled = false;
                    Object.transform.GetChild(0).transform.GetComponent<DragSkillHandler>().flag = false;
                    Object.transform.GetChild(0).localScale = new Vector3(1.7f, 1.61f, 1);
                    Object.transform.GetChild(0).transform.GetComponent<Canvas>().sortingOrder = 14;
                    Object.transform.GetChild(0).SetParent(RightSkill, false);
                    

                }

            }

        }
        
       
    }
    private void ResetSkills()
    {
        for (int i = 0; i < ParentSkill.childCount; i++)
        {
            if(ParentSkill.childCount>0)
            Destroy(ParentSkill.GetChild(i).gameObject);
            if(LeftSkill.childCount>0)
            Destroy(LeftSkill.GetChild(0).gameObject);
            if(RightSkill.childCount>0)
            Destroy(RightSkill.GetChild(0).gameObject);
        }
    }



    private void Awake()
    {
        

        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;

        }
       
    }
    
    public void RDFF(ref GameObject[,] Matrix,int amountToClear)
    {
        int row, column;

        row = Matrix.GetLength(0);

        column = Matrix.GetLength(1);

        List<Index> indexToClear = new List<Index>();

        List<Index> fullMatrices = new List<Index>();

        GameManager.SpawnManager.RefreshFullMatrix();
        
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                if (GameManager.SpawnManager.MainMatrixFlag[i,j]==1)
                {
                  

                    Index index = new Index();
                    index.row = i;
                    index.column = j;
                  
                    fullMatrices.Add(index);
                }
            }
        }
        
        for (int i = 0; i < amountToClear; i++)
        {
            Index temp;
            if (fullMatrices.Count > 0 )
            {
                 temp = fullMatrices[UnityEngine.Random.Range(0, fullMatrices.Count - 1)];
                //if (indexToClear.Any(x => x.row == temp.row) && indexToClear.Any(x => x.column == temp.column))
                //{

                //}

                indexToClear.Add(temp);
                fullMatrices.Remove(temp);
            }
            
            
        }


        for (int i = 0; i < indexToClear.Count; i++)
        {
            if (Matrix[indexToClear[i].row, indexToClear[i].column].transform.childCount > 0)
            {
                Matrix[indexToClear[i].row, indexToClear[i].column].transform.GetChild(0).GetComponent<Image>().color = Color.black;
                StartCoroutine(WaitFor(1f, Matrix[indexToClear[i].row, indexToClear[i].column].transform.GetChild(0).gameObject,true));
               
            }
        }


    }
    

    public void RD(ref GameObject[,] Matrix, int amountToClear)
    {
        int row, column;

        row = Matrix.GetLength(0);

        column = Matrix.GetLength(1);

        List<Index> indexToClear = new List<Index>();

        List<Index> MatrixList = new List<Index>();

        GameManager.SpawnManager.RefreshFullMatrix();

        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                
                    Index index = new Index();
                    index.row = i;
                    index.column = j;

                    MatrixList.Add(index);
                
            }
        }

        for (int i = 0; i < amountToClear; i++)
        {
            Index temp;
            if (MatrixList.Count > 0)
            {
                temp = MatrixList[UnityEngine.Random.Range(0, MatrixList.Count - 1)];
            
                indexToClear.Add(temp);
                MatrixList.Remove(temp);
            }


        }

        
        for (int i = 0; i < indexToClear.Count; i++)
        {
            //Start rocket animation for every cell

           
            if (Matrix[indexToClear[i].row, indexToClear[i].column].transform.childCount > 0)
            {

                //burayida ayri bir fonksiyona tasi ve animasyon bittiginde destroy yapsin
                
                Matrix[indexToClear[i].row, indexToClear[i].column].transform.GetChild(0).GetComponent<Image>().color = Color.black;
                StartCoroutine(WaitFor(1f, Matrix[indexToClear[i].row, indexToClear[i].column].transform.GetChild(0).gameObject,true));
               
                

            }
            else
            {
                Matrix[indexToClear[i].row, indexToClear[i].column].GetComponent<Image>().color = Color.gray;
                StartCoroutine(WaitFor(1f, Matrix[indexToClear[i].row, indexToClear[i].column].gameObject, false));

            }
        }


    }
    private IEnumerator WaitFor(float time,GameObject matrix,bool hit)
    {
        if (hit)
        {
            matrix.transform.SetParent(GameManager.GameManagerInstance.Pool.transform);
            yield return new WaitForSeconds(time);
            Destroy(matrix);
            GameManager.StatsManager.OnScore(1); 
        }else if (!hit)
        {
            yield return new WaitForSeconds(time);
            matrix.GetComponent<Image>().color =new Color32(206,206,206,255);
        }

    }
    public void ClearAllMatrix(ref GameObject[,] Matrix)
    {
       
        int row, column;

        row = Matrix.GetLength(0);

        column = Matrix.GetLength(1);

        

        List<Index> fullMatrices = new List<Index>();

        GameManager.SpawnManager.RefreshFullMatrix();

        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                if (GameManager.SpawnManager.MainMatrixFlag[i, j] == 1)
                {
                   
                    Index index = new Index();
                    index.row = i;
                    index.column = j;

                    fullMatrices.Add(index);
                }
            }
        }

        for (int i = 0; i < fullMatrices.Count; i++)
        {
            


                Destroy(Matrix[fullMatrices[i].row, fullMatrices[i].column].transform.GetChild(0).gameObject);
                GameManager.StatsManager.OnScore(1);

            
        }


    }


   public void ReloadSpawners()
    {
        GameManager.SpawnManager.ResetSpawnSlots();
        GameManager.SpawnManager.SpawnItems();
        GameManager.SpawnManager.SpaceControl();
    }

    public void SquareBomb()
    {
        if (GameManager.SpawnManager.SpawnSlotList[0].transform.childCount == 0 || GameManager.SpawnManager.SpawnSlotList[1].transform.childCount == 0 || GameManager.SpawnManager.SpawnSlotList[2].transform.childCount == 0)
        {
            for (int i = 0; i < GameManager.SpawnManager.SpawnSlotList.Count; i++)
            {
                if (GameManager.SpawnManager.SpawnSlotList[i].transform.childCount==0)
                {
                    //get the color randomly
                    Color temp =Color.black/* GameManager.ColorManager.TurnRandomColorFromTheme()*/;

                    GameObject Clone;
                    //instantiate
                    
                       
                            Clone = (GameObject)Instantiate(DragItem, GameManager.SpawnManager.SpawnSlotList[i].transform);
                            foreach (Image image in Clone.GetComponentsInChildren<Image>().ToList())
                            {    //set the color of children
                                if (image.color.a != 0)//this is for touch area
                                {

                                    if (image.gameObject.tag != "Rock")
                                    {
                                        image.color = temp;

                                    }

                                    //change sprite theme here
                                    switch (GameManager.SpawnManager.theme)
                                    {
                                        case BlockTheme.Classic:
                                            image.sprite = GameManager.SpawnManager.BlockSpriteThemes[0];


                                            break;
                                        case BlockTheme.New:
                                            image.sprite = GameManager.SpawnManager.BlockSpriteThemes[1];
                                            break;

                                        default:
                                            break;

                                    }

                                }





                            }





                    return;
                   
                }
            }

        }
       
    }
}
