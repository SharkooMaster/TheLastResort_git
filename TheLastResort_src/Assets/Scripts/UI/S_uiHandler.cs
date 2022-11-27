using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class S_uiHandler : MonoBehaviour
{
    [SerializeField] private S_playerCamera Cam;
    [SerializeField] private GameObject HUD;

    private void Update()
    {
        toggleInv();
    }

    [Header("UI components")]
    [SerializeField] private GameObject INVENTORY;
    [SerializeField] private GameObject INVENTORYITEMS;

    private bool showInventory = false;

    private void toggleInv()
    {
        HUD.SetActive((showInventory) ? false : true);
        INVENTORY.SetActive((showInventory) ? true : false);

        if (Input.GetKeyDown(KeyCode.Tab))
            showInventory = !showInventory;

        Cam.toggle = !showInventory;
        Cursor.lockState = (Cam.toggle) ? CursorLockMode.Locked : CursorLockMode.Confined;
    }
}
