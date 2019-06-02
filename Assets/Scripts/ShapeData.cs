using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Shape", menuName = "Shape Data", order = 51)]
public class ShapeData : ScriptableObject
{
    public float speed;
    public float tapsAmount;
    public float size;
}
