using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public struct SerializablePositionValues
{
    public float x;
    public float y;
    public float z;

    public Vector3 GetPosition()
    {
        return new Vector3(x, y, z);
    }
}


public class ShapesData
{
    public string name;

    public SerializablePositionValues myPosition;
}
   
