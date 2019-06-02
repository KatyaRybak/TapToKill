using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShapeLogic : MonoBehaviour
{
    public GameObject vievScoreText;
    Color shapeColor;
    [SerializeField] float speed;
    public int score;
    public ShapeData shapeProperties;
    InterfaceController interfaceLogic;
    Vector3 spawnPosition;
    Vector2 pointEnd;
    public bool isNegative;

   

    void Start()
    {
        interfaceLogic = FindObjectOfType<InterfaceController>();
        spawnPosition = gameObject.transform.position;
        
    }

    void Update()
    {
        //speed = shapeProperties.speed / ((Vector2)(transform.position - Camera.main.transform.position)).magnitude;
        MovedShapes();
    }


    void MovedShapes()
    {
        pointEnd = -spawnPosition;
        //GetComponent<Rigidbody2D>().velocity = pointEnd* Mathf.Min(speed,0.5f);
        GetComponent<Rigidbody2D>().velocity = pointEnd/7 * MainLogic.timeMultiplier;
        if (Time.timeScale > 0)
        {
            transform.Rotate(Vector3.forward * 1);
        }
        if (!GetComponent<Renderer>().isVisible)
        {
            gameObject.SetActive(false);
            if (isNegative)
            {
                MainLogic.instance.negativeCount--;
            }
            else
            {
                MainLogic.instance.positiveCount--;
            }
        }
    }


    public Vector3 SpawnPosition
    {
        get{return spawnPosition;}
        set{spawnPosition = value;}
    }
    private void OnMouseDown()
    {
        MainLogic.instance.AddToScore(score,score<0);
        StartCoroutine(UnactiveShape(score, isNegative));
    }

    public IEnumerator UnactiveShape(int scoreOne, bool negative)
    {
        gameObject.GetComponent<Collider2D>().enabled = false;
        GameObject textScore = Instantiate(vievScoreText, transform.position, Quaternion.identity, FindObjectOfType<Canvas>().transform);
        Destroy(textScore, 1f);
        textScore.GetComponent<ScoreTextLogic>().SetTextParameters(scoreOne);

        textScore.transform.position = transform.position;
        yield return new WaitForSeconds(0.25f);
        if (negative)
        {
            MainLogic.instance.negativeCount--;
        }
        else
        {
            MainLogic.instance.positiveCount--;
        }
        interfaceLogic.UpdateText();
        gameObject.GetComponent<Collider2D>().enabled = true;
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        MainLogic.instance.AddToScore(-score*2, score < 0);
        StartCoroutine(UnactiveShape(-score * 2, isNegative));
    }
}
