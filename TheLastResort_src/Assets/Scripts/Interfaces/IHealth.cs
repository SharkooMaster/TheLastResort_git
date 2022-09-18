using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealth
{
    float health { get; set; }
    float armour { get; set; }

    private void damage(float _f)
    {
        if(armour > 0) { armour-= _f; }
        else { health -= _f; }
    }
}
