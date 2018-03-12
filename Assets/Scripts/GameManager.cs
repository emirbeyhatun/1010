using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class GameManager : MonoBehaviour {

    

    private static ColorManager colorManager;
    public static ColorManager ColorManager
    {
        get
        {
           
            return colorManager;
        }

    }
    private static GameManager gameManagerInstance;
    public static GameManager GameManagerInstance
    {
        get
        {

            return gameManagerInstance;
        }
    }
    private static SpawnManager spawnManager;
    public static SpawnManager SpawnManager
    {
        get
        {

            return spawnManager;
        }

    }
    private static StatsManager statsManager;
    public static StatsManager StatsManager
    {
        get
        {

            return statsManager;
        }

    }

    private static UiManager uiManager;
    public static UiManager UiManager
    {
        get
        {

            return uiManager;
        }

    }
    private static StateManager stateManager;
    public static StateManager StateManager
    {
        get { return stateManager; }
    }

    private static AchievementPublisher achievementPublisher;
    public static AchievementPublisher AchievementPublisher
    {
        get
        {

            return achievementPublisher;
        }

    }

    public GameObject[,] MatrixArray = new GameObject[10, 10];
    

    public int[] columnFlag = new int[10];
    public  int[] rowFlag = new int[10];
    
    [SerializeField]
    private GameObject[] classicModeItems;
   // [SerializeField]
    //private GameObject[] bombModeItems;
    
    private GameObject[] rockSolidModeItems;
    [SerializeField]
    private GameObject[] rockSolidItems;
    [SerializeField]
    private GameObject bomb;
    public GameObject Bomb
    {
        get { return bomb; }
    }

    public GameObject[] ClassicModeItems {//mode 1
        get { return classicModeItems; }
    }
   
    public GameObject[] RockSolidModeItems
    {//mode 2
        get { return rockSolidModeItems; }
    }

    public GameObject Pool;
    [HideInInspector]
    public GameObject slotParent;
    void Awake()
        {
        #region Singleton
        if (gameManagerInstance != null && gameManagerInstance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            gameManagerInstance = this;

        } 
        #endregion

        colorManager = GetComponent<ColorManager>();
        spawnManager = GameObject.FindObjectOfType<SpawnManager>();
        statsManager = GetComponent<StatsManager>();
        uiManager = GetComponent<UiManager>();
        stateManager = GetComponent<StateManager>();
        achievementPublisher = GetComponent<AchievementPublisher>();
        }
    
    void Start () {
       Pool = GameObject.FindGameObjectWithTag("Pool");
        //Find parent object of 100 slots
         slotParent = GameObject.FindGameObjectWithTag("Puzzle");

        rockSolidModeItems = new GameObject[classicModeItems.Length + rockSolidItems.Length];

        Array.Copy(classicModeItems, rockSolidModeItems, classicModeItems.Length);
        Array.Copy(rockSolidItems,0, rockSolidModeItems, classicModeItems.Length, rockSolidItems.Length);

       



        #region MatrixAssignment
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                //find all 100 slot and set it to matrix
                MatrixArray[i, j] = slotParent.transform.GetChild((i) * 10 + j).gameObject;



            }
        }
        #endregion







        slotParent.SetActive(false);


    }

    public  void RowColumnControl()
    {

        #region FullColumnFlags
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                //find full columns(that column's all slots must be full)
                if (MatrixArray[j, i].transform.childCount > 0)
                {
                    columnFlag[i] += 1;

                }
            }

        }
        #endregion

        #region FullRowFlags
        for (int a = 0; a < 10; a++)
        {
            for (int b = 0; b < 10; b++)
            {
                //find full rows(that row's all slots must be full)
                if (MatrixArray[a, b].transform.childCount > 0)
                {
                    rowFlag[a] += 1;
                }
            }

        }

        #endregion
        RowDelete();
        ColumnDelete();
    }
	
	public  void RowDelete()
    {

        #region CleanoutRow
        for (int a = 0; a < 10; a++)
        {

            //this clears all fulled slots
            if (rowFlag[a] == 10)
            {
                for (int i = 0; i < 10; i++)
                {
                    //get the child
                    GameObject child = MatrixArray[a, i].transform.GetChild(0).gameObject;
                    statsManager.OnScore(1);


                    if (child != null && child.tag != "Rock")
                    {
                        StartCoroutine(DestroyObject(child,i*0.05f));
                    }
                    else if(child.tag == "Rock") {

                        child.GetComponent<Durability>().DecreaseRockDurability();
                    }

                    


                }

            }

            rowFlag[a] = 0;
        } 
        #endregion

    }
    private IEnumerator DestroyObject(GameObject child,float time)
    {
        yield return new WaitForSeconds(time);
        child.transform.SetParent(Pool.transform);
        Destroy(child);
    }
    public  void ColumnDelete()
    {



        #region CleanoutColumn
        for (int a = 0; a < 10; a++)
        {


            if (columnFlag[a] == 10)
            {
                for (int i = 0; i < 10; i++)
                {
                    //get the child
                    GameObject child = MatrixArray[i, a].transform.GetChild(0).gameObject;
                    statsManager.OnScore(1);

                    if (child != null && child.tag != "Rock")
                    {
                        StartCoroutine(DestroyObject(child, i * 0.05f));
                    }
                    else if (child.tag == "Rock")
                    {

                        child.GetComponent<Durability>().DecreaseRockDurability();
                    }




                }

            }

            columnFlag[a] = 0;
        } 
        #endregion


    }
    public void ClearMatrix()
    {
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                
                if (MatrixArray[j, i].transform.childCount > 0)
                {
                    if(MatrixArray[j, i].transform.childCount > 0)
                    {
                        GameObject child = MatrixArray[j, i].transform.GetChild(0).gameObject;
                        child.transform.SetParent(Pool.transform);
                        Destroy(child);
                    }
                    

                }
            }

        }
    }



    


}
