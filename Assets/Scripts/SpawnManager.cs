using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public enum BlockTheme
{
    Classic,
    New
}
public class SpawnManager : MonoBehaviour {
    
    public List<Transform> SpawnSlotList = new List<Transform>();
    int[,] spawnSlotMatrix = new int[5, 5];
    public int[,] MainMatrixFlag = new int[10, 10];
    struct cell
    {
        public int row;
        public int column;

        
    }
    
    List<cell> searchList = new List<cell>();
  
    public BlockTheme theme;

    public Sprite[] BlockSpriteThemes;
    public Sprite[] RockSprites;

    private void Awake()//this must be awake 
    {
        
        //get all 3 spawn slots
        SpawnSlotList.Clear();
        foreach (Transform child in transform)
        {
            SpawnSlotList.Add(child);
        }
        
    }
    public void RefreshFullMatrix()
    {
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {

                if (GameManager.GameManagerInstance.MatrixArray[i, j].transform.childCount > 0)
                {
                    MainMatrixFlag[i, j] = 1;
                }

                else if (GameManager.GameManagerInstance.MatrixArray[i, j].transform.childCount == 0)
                {
                    MainMatrixFlag[i, j] = 0;

                }



            }

        }
    }
    public void SpaceControl()
    {
        StartCoroutine(IESpaceControl());
    }
    public IEnumerator IESpaceControl()
    {
        yield return new WaitForSeconds(0.1f);


        RefreshFullMatrix();
        int canEnd = 0;
        int notFitAmount = 0;
        #region ChechEnoughSpace
        for (int i = 0; i < 3; i++)
        {//After dragging, check if spawned items or current ones fit anywhere , if not then end the game
            //pick the right ones , if no child then there is no object there
            if (SpawnSlotList[i].childCount > 0)
            {
                canEnd++;
             
               

                SpawnMatrix(i,ref notFitAmount);
            }
           


        }
        if (canEnd != 0 && canEnd == notFitAmount)
        {

            //if the program comes here this means that in previous function we didnt return that means there is no space then we need to end the game
            
                GameManager.StateManager.SetState(States.EndGameState);
            
        }
        #endregion

        //we have  3 objects at the most at the same time after it runs empty we got to fill it up
        
        SpawnNewObjects();
    }
    
    
    private void   SpawnMatrix(int a,ref int notFitAmount)
    {
         searchList.Clear();
        if (SpawnSlotList[a].transform.childCount > 0)
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (SpawnSlotList[a].transform.GetChild(0).GetChild(0).GetChild(j+i*5).childCount>0)
                    {
                        spawnSlotMatrix[i, j] = 1;
                            }
                    else
                    {
                        spawnSlotMatrix[i, j] = 0;
                    }



                }

            }
            CheckSpace(ref notFitAmount);
        }
       
       
    



    }
    private void CheckSpace(ref int notFitAmount)
    {
         for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                
                if (spawnSlotMatrix[i, j] == 1)
                {
                    cell newCell = new cell();
                    newCell.row = i;
                    newCell.column = j;
                    



                    searchList.Add(newCell);
                }

            }
        }
       
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                if (MainMatrixFlag[i,j]==0)
                {
                    cell temp = (cell)searchList[0];
                   
                    int tempRow = i - temp.row;
                    int tempCol = j - temp.column;
                    

                    for (int a = 0; a < searchList.Count; a++)
                    {
                        try
                        {
                            if (MainMatrixFlag[searchList[a].row + tempRow, searchList[a].column + tempCol] == 0)
                            {
                                if (a + 1 == searchList.Count)
                                {
                                    Debug.Log("fit");
                                    return;
                                }
                            }
                            else
                            {
                                Debug.Log("doesnt fit");
                                break;
                            }
                        }
                        catch (System.IndexOutOfRangeException)
                        {
                            Debug.Log("doesnt fit");
                            break;

                        }
                    }
                }

            }
        }

        notFitAmount++;
       




    }
    private void SpawnNewObjects()
    {
        
        //check if spawn slots empty
        if (SpawnSlotList[0].transform.childCount == 0 && SpawnSlotList[1].transform.childCount == 0 && SpawnSlotList[2].transform.childCount == 0)
        {
            //if they all are empty then spawn
            SpawnItems();

        }
        
        
           
        


    }

    public void SpawnItems()
    {

        GetComponent<Animator>().SetTrigger("Spawn");
       
        for (int i = 0; i < 3; i++)
        {
            #region InstantiateAndSetColor
            //get the color randomly
            Color temp = GameManager.ColorManager.TurnRandomColorFromTheme();
            
            GameObject Clone;
            //instantiate
            switch (GameManager.StateManager.StateName)
            {
                case States.PlayState:
                     Clone = (GameObject)Instantiate(GameManager.GameManagerInstance.ClassicModeItems[Random.Range(0, GameManager.GameManagerInstance.ClassicModeItems.Length)], SpawnSlotList[i].transform);
                    foreach (Image image in Clone.GetComponentsInChildren<Image>().ToList())
                    {    //set the color of children
                        if (image.color.a != 0)//this is for touch area
                        {
                            image.GetComponent<Canvas>().sortingOrder = 5;
                            if (image.gameObject.tag != "Rock")
                            {
                                    image.color = temp;
                                
                            }

                            //change sprite theme here
                            switch (theme)
                            {
                                case BlockTheme.Classic:
                                    image.sprite = BlockSpriteThemes[0];
                                    

                                    break;
                                case BlockTheme.New:
                                    image.sprite = BlockSpriteThemes[1];
                                    break;

                                default:
                                    break;

                            }

                        }





                    }
                    break;
                
                
                case States.PlayStateRockSolid:
                    int rand = RandomWithProbability.GetRandomValue(new RandomWithProbability.RandomSelection(0, GameManager.GameManagerInstance.ClassicModeItems.Length-1, 0.9f), new RandomWithProbability.RandomSelection(GameManager.GameManagerInstance.ClassicModeItems.Length, GameManager.GameManagerInstance.RockSolidModeItems.Length-1, 0.1f));
                   
                    Clone = (GameObject)Instantiate(GameManager.GameManagerInstance.RockSolidModeItems[rand], SpawnSlotList[i].transform);
                    

                        int rndm = UnityEngine.Random.Range(1, 4);
                        foreach (Image image in Clone.GetComponentsInChildren<Image>().ToList())
                        {    //set the color of children
                            if (image.color.a != 0)
                            {

                            image.GetComponent<Canvas>().sortingOrder = 5;
                            if (image.gameObject.tag != "Rock")
                            {
                               
                               image.color = temp;
                               
                                //change sprite theme here
                                switch (theme)
                                {
                                    case BlockTheme.Classic:
                                        image.sprite = BlockSpriteThemes[0];


                                        break;
                                    case BlockTheme.New:
                                        image.sprite = BlockSpriteThemes[1];
                                        break;

                                    default:
                                        break;

                                }
                            }
                            else if (Clone.CompareTag("Rock"))
                             {

                                GameObject block = image.gameObject;
                                block.GetComponent<Durability>().durability = rndm;
                                
                                //change sprite theme here
                                int drb = block.GetComponent<Durability>().durability;

                                switch (drb)
                                {
                                    case 1:
                                        image.sprite = RockSprites[0];


                                        break;
                                    case 2:
                                        image.sprite = RockSprites[1];
                                        break;
                                    case 3:
                                        image.sprite = RockSprites[2];
                                        break;
                                    case 4:
                                        image.sprite = RockSprites[3];
                                        break;
                                    default:
                                        break;
                                }


                             }
                           }

                        }
                        
                        
                        
                    

                    break;
                case States.PlayStateBomb:

                    Clone = (GameObject)Instantiate(GameManager.GameManagerInstance.ClassicModeItems[Random.Range(0, GameManager.GameManagerInstance.ClassicModeItems.Length)], SpawnSlotList[i].transform);
                    foreach (Image image in Clone.GetComponentsInChildren<Image>().ToList())
                    {    //set the color of children
                        if (image.color.a != 0)
                        {
                            if (image.gameObject.tag != "Rock")
                            {
                                   image.color = temp;
                                
                                //change sprite theme here
                                switch (theme)
                                {
                                    case BlockTheme.Classic:
                                        image.sprite = BlockSpriteThemes[0];


                                        break;
                                    case BlockTheme.New:
                                        image.sprite = BlockSpriteThemes[1];
                                        break;

                                    default:
                                        break;

                                }
                            }

                            //change sprite theme here
                            switch (theme)
                            {
                                case BlockTheme.Classic:
                                    image.sprite = BlockSpriteThemes[0];


                                    break;
                                case BlockTheme.New:
                                    image.sprite = BlockSpriteThemes[1];
                                    break;

                                default:
                                    break;

                            }
                            image.GetComponent<Canvas>().sortingOrder = 5;

                        }





                    }

                    break;
                default:
                    throw new System.Exception("Spawn Error");
                    
            }

            
               
            
            #endregion


        }

       


    }
    public void ResetSpawnSlots()
    {
      GameObject  Pool = GameObject.FindGameObjectWithTag("Pool");
        for (int i = 0; i < 3; i++)
        {
            if (SpawnSlotList[i].transform.childCount > 0)
            {
                Transform spawner = SpawnSlotList[i].transform.GetChild(0);
                spawner.SetParent(Pool.transform);
                Destroy(spawner.gameObject);
            }
            
        }
       
    }
    
}
