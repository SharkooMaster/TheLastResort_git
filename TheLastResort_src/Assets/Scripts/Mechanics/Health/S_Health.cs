using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using System;

public class S_Health : MonoBehaviour
{
    public int health = 100;
    public int armor  = 100;

    public void damage(int a)
    {
        if(armor > 0) { armor -= a; }
        else if(health > 0) { health -= a; }
        else if(health <= 0) { Destroy(gameObject); }
    }
}
