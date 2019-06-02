using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainLogic : MonoBehaviour
{
    static int countDifShapes = 3;
    public GameObject[] shapesPrefabs;
    Queue<GameObject>[] shapes; 
    PopUpMethods popUpMetod  = PopUpMethods.InCenter;

    public static float timeMultiplier = 1;
    public int totalScore;
    public int timerMin;
    public int timerSec;
    public bool isGamePaused;
    public bool isGameStarted;
    public int positiveCount;
    public int negativeCount;
    [SerializeField] int countOfShapes;

    public static MainLogic instance;
    InterfaceController interfaceLogic;


    enum PopUpMethods
    {
        InCenter,
        Bounds,
        Rounded,
        Kicking
    }


    private void Awake()
    {
        instance = this;
        shapes = new Queue<GameObject>[shapesPrefabs.Length];
    }

    private void Start()
    {
        SetStartParameters();

        AddToPool();
        StartCoroutine(SpawnObjects());
        interfaceLogic.UpdateText();
        StartCoroutine(TimerCounter());

    }

    private void SetStartParameters()
    {
        isGameStarted = true;
        isGamePaused = false;
        interfaceLogic = FindObjectOfType<InterfaceController>();
        totalScore = 0;
        timerMin = 1;
        timerSec = 0;
        countOfShapes = 10;
    }


    void AddToPool()
    {
        for (int i = 0; i < countDifShapes; i++)
        {
            shapes[i] = new Queue<GameObject>();
            for (int j = 0; j < countOfShapes; j++)
            {
                GameObject shapeInPool = Instantiate(shapesPrefabs[i], transform.position, transform.rotation);
                shapeInPool.SetActive(false);
                shapes[i].Enqueue(shapeInPool);
            }
        }
    }


    void PopUpOnScreen()
    {
        Vector2 instPoint = GetPopUpPosition();
        int numberQueue = Random.Range(0, countDifShapes);
        Queue<GameObject> randomQueue = shapes[numberQueue];
        for (int i = 0; i < countOfShapes; i++)
        {
            GameObject shapeOnScene = randomQueue.Dequeue();
            if (shapeOnScene.activeSelf == false)
            {
                SetScoreAndColor(shapeOnScene);
                shapes[numberQueue].Enqueue(shapeOnScene);
                shapeOnScene.SetActive(true);
                shapeOnScene.transform.position = instPoint;
                shapeOnScene.GetComponent<ShapeLogic>().SpawnPosition= instPoint;
                break;
            }
            else
            {
                shapes[numberQueue].Enqueue(shapeOnScene);
            }
        }
    }

    private void SetScoreAndColor(GameObject shapeOnScene)
    {
        if(negativeCount<3)
        {
            shapeOnScene.GetComponent<ShapeLogic>().score = -1;
            shapeOnScene.GetComponent<ShapeLogic>().isNegative = true;
            shapeOnScene.GetComponent<SpriteRenderer>().color = Color.red;
            negativeCount++;
        }
        else
        {
            shapeOnScene.GetComponent<ShapeLogic>().score = 1;
            shapeOnScene.GetComponent<ShapeLogic>().isNegative = false;
            shapeOnScene.GetComponent<SpriteRenderer>().color = new Color(0,0, Random.value);
            positiveCount++;
        }
    }

    private Vector2 GetPopUpPosition()
    {
        float maxPointY = Camera.main.pixelHeight;
        float maxPointX = Camera.main.pixelWidth;
        Vector2 instPoint = Camera.main.transform.position;
        if (popUpMetod==PopUpMethods.InCenter)
        {
            instPoint = new Vector2(Random.Range(0.0f, maxPointX), Random.Range(0.0f, maxPointY));
            int upDown = Random.Range(0, 2);
            if(upDown == 0)
            {
                instPoint.y = Random.Range(0, 2) == 0 ? 0f : maxPointY;
            }
            else
            {
                instPoint.x = Random.Range(0, 2) == 0 ? 0f : maxPointX;
            }
            instPoint = Camera.main.ScreenToWorldPoint(instPoint);           
        }
        return instPoint;
    }




    IEnumerator SpawnObjects()
    {
        while (isGameStarted)
        {
            yield return new WaitForSeconds(0.5f/timeMultiplier);
            if (isGameStarted && FindObjectsOfType<ShapeLogic>().Length< countOfShapes)
            {
                PopUpOnScreen();
            }
        }
    }

    IEnumerator TimerCounter()
    {
        timeMultiplier = 1f;
        while (isGameStarted == true)
        {
            yield return new WaitForSeconds(1f);
            if (timerSec <= 0 && timerMin > 0)
            {
                timerMin--;
                timerSec = 59;
            }
            else
            {
                timerSec--;
            }
            if (timerSec + timerMin * 60 <= 30 && timeMultiplier == 1f)
            {
                timeMultiplier = 1.4f;
            }
            if (timerSec + timerMin * 60 <= 15 && timeMultiplier == 1.4f)
            {
                timeMultiplier = 1.8f;
            }
            interfaceLogic.UpdateText();
            if (timerSec <= 0 && timerMin <= 0)
            {
                PlayGameOver();
            }
        }
        
    }

    public void AddToScore(int score, bool negative)
    {
        if (isGamePaused == false&& isGameStarted)
        {
            totalScore += score;
            if (totalScore < 0)
            {
                totalScore = 0;
            }          
          
        }
    }

    public void ClearScene()
    {
        ShapeLogic[] shapeArray = FindObjectsOfType<ShapeLogic>();
        foreach(ShapeLogic oneShape in shapeArray)
        {
            Destroy(oneShape.gameObject);
        }
    }

    public void PlayGameOver()
    {
        isGameStarted = false;
        ClearScene();
        interfaceLogic.ShowEndRoundMenu();
    }
    

}
