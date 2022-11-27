using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

using UnityEngine.UI;
using TMPro;

public class S_invBtn : MonoBehaviour
{
    public Action<int> fn;
    public Button btn;

    public TMP_Text textName;
    public TMP_Text textCount;
    public Item item;

    public int id;

    public void set()
    {
        fn(id);
    }

    private void Start()
    {
        btn = GetComponent<Button>();
        textName.text = item._name;
        textCount.text = item.count.ToString();
    }
}
