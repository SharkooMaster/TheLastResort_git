using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using System;

public class S_Health : MonoBehaviour
{
    public float health = 100;
    public float armor  = 100;

    public void damage(float a)
    {
        if(armor > 0) { armor -= a; }
        else if(health > 0) { health -= a; }
    }

    private void Update()
    {
        if(health <= 0) { Destroy(this.gameObject); }
    }
}
