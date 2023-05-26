using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Eyes : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Ai _ai;
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            trigger(other);
        }
    }

    public void trigger(Collider other)
    {
        if(_ai.state != 3)
        {
            _ai.conversationHandler.triggerCombatClip("Nathan_Combat1");
            _ai.animator.SetBool("Walking", false);
            _ai.animator.SetBool("Combat", true);
        }
        // Initiate target.
        _ai.hitPlayer = other.transform;
        print(other.transform.name);
        _ai.state = 3;
    }
}
