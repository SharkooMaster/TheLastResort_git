using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Json_Array
{
    public List<Json_Conversation> array;

    public void printArr()
    {
        Debug.Log($"Conversation Length: {array.Count}");
        foreach (var item in array)
        {
            Debug.Log($"{item.path}");
        }
    }
}
