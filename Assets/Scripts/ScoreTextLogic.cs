using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTextLogic : MonoBehaviour
{
     private void Start()
    {
    }
    public void SetTextParameters(int value)
    {
        if (value < 0)
        {
            GetComponentInChildren<Text>().text = value.ToString();
            GetComponentInChildren<Text>().color = Color.red;
        }
        else
        {
            GetComponentInChildren<Text>().text = string.Format("+ {0}", value);
            GetComponentInChildren<Text>().color = Color.green;
        }

}
}
