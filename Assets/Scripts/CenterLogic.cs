using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CenterLogic : MonoBehaviour
{
    public GameObject vievScoreText;
    InterfaceController interfaceLogic;

    private void Start()
    {
        interfaceLogic = FindObjectOfType<InterfaceController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        interfaceLogic.ChangeHealth(collision.GetComponent<ShapeLogic>().score * (-2));
    }

}
