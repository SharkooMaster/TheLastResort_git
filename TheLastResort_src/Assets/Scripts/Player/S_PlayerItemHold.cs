using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_PlayerItemHold : MonoBehaviour
{
    public S_Inventory inventory;
    public GameObject _hand;

    private void Start()
    {
        inventory = gameObject.GetComponent<S_playerCamera>().inventory;
        switchObj();
    }

    public void switchObj()
    {
        if (inventory.activeIndex != -1)
        {
            GameObject go = Instantiate(inventory.inventoryItems[inventory.activeIndex].go, _hand.transform);
            go.transform.localPosition = Vector3.zero;
            go.transform.localRotation = Quaternion.identity;
            go.GetComponent<BoxCollider>().enabled = false;
            go.SetActive(true);
        }
    }

    // Incase we ever decided to add scroll through inventory
    /*private void Update()
    {
        switch (Input.mouseScrollDelta.y)
        {
            case > 0:
                inventory++;
                switchObj();
                break;
            case < 0:
                inventory--;
                switchObj();
                break;
        }
    }*/
}
