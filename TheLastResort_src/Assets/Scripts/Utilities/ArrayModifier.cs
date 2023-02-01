using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ArrayModifier : MonoBehaviour
{
    public GameObject _this;
    public int offsX;
    public int offsY;
    public int offsZ;

    public int count;

    public void Awake()
    {
        this.useGUILayout = true;
    }

    public void Update()
    {
    }

    public void OnGUI()
    {
        if(GUILayout.Button("Apply array modifier"))
        {
            _this.transform.localScale = new Vector3(1, 1, 1);
            for (int i = 1; i <= count; i++)
            {
                GameObject inserted = Instantiate(_this, new Vector3((i * offsX) + transform.position.x ,
                    (i * offsY) + transform.position.y,
                    (i * offsZ) + transform.position.z ), this.transform.rotation, this.transform);
            }
        }
    }
}
