using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using System;

public class Item : MonoBehaviour
{
    public GameObject go;
    public string _name;
    public int maxCount;
    public int count;

    // Interaction //
    public Action fn;
    public bool isActive = false;
}
