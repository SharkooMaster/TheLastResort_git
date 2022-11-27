using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Inventory : MonoBehaviour
{
    public List<Item> inventoryItems;
    public int activeIndex = -1;

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
            _nbtn.item = inventoryItems[i];
        }
    }

    void clear()
    {
        int cc = inventory_Obj.transform.childCount;

        for (int i = 0; i < cc; i++)
        {
            Destroy(inventory_Obj.transform.GetChild(i));
        }
    }
}
