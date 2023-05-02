using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission : MonoBehaviour
{
    public string _name;
    public string _description;

    public enum types { item }
    public types _type;

    public bool _enabled = false;

    public enum status { OFF, ON, DONE }
    public status _stat = status.OFF;

    private void Start()
    {
        if(_stat == status.OFF) { transform.gameObject.SetActive(false); }
    }

    private void Update()
    {
        switch (_type)
        {
            case types.item:
                if(!transform.gameObject.activeSelf)
                    _enabled = false;
                break;
        }
    }
}
