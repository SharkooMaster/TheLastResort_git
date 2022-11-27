using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Inventory : MonoBehaviour
{
    public List<Item> inventoryItems;
    public int maxCount = 10;
    public int activeIndex = -1;

    public static S_Inventory operator ++(S_Inventory a)
    {
        if (a.activeIndex < a.maxCount)
            a.activeIndex++;
        else
            a.activeIndex = 0;
        return a;
    }

    public static S_Inventory operator --(S_Inventory a)
    {
        if (a.activeIndex > 0)
            a.activeIndex--;
        else
            a.activeIndex = a.maxCount;
        return a;
    }

    public void add(Item a)
    {
        int search = searchForItem(a._name);
        if (search != -1)
        {
            inventoryItems[search].count += 1;
        }
        else
        {
            inventoryItems.Add(a);
            inventoryItems[inventoryItems.Count - 1].gameObject.SetActive(false);
        }

        syncInventoryUI();
    }

    public void del(Item a)
    {
        for (int i = 0; i < inventoryItems.Count; i++)
        {
            if (inventoryItems[i]._name == a._name)
            {
                inventoryItems.RemoveAt(i);
            }
        }
    }

    private int searchForItem(string a)
    {
        for (int i = 0; i < inventoryItems.Count; i++)
        {
            if (inventoryItems[i]._name == a)
            {
                return i;
            }
        }
        return -1;
    }

    public GameObject inventory_Obj;
    public GameObject inventory_Btn;
    public void syncInventoryUI()
    {
        clear();

        for (int i = 0; i < inventoryItems.Count; i++)
        {
            GameObject nBtn = Instantiate(inventory_Btn);
            nBtn.transform.SetParent(inventory_Obj.transform, false);

            S_invBtn _nbtn = nBtn.GetComponent<S_invBtn>();
            _nbtn.fn = ((int id) => { activeIndex = id; });
            Debug.Log(i);
            _nbtn.item = inventoryItems[i];

            _nbtn.handListener = transform.GetComponent<S_PlayerItemHold>();
        }
    }

    void clear()
    {
        int cc = inventory_Obj.transform.childCount;

        for (int i = 0; i < cc; i++)
        {
            Destroy(inventory_Obj.transform.GetChild(i).gameObject);
        }
    }
}
