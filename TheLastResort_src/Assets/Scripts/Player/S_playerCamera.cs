using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class S_playerCamera : MonoBehaviour
{
    public GameObject player;
    public bool toggle = true;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        player = transform.parent.gameObject;
    }

    [SerializeField] float sensitivityX;
    [SerializeField] float sensitivityY;

    public float rotH;
    public float rotV;

    private void Update()
    {
        if (toggle)
        {
            rotH += Input.GetAxis("Mouse X") * (sensitivityX * Time.deltaTime);
            rotV -= Input.GetAxis("Mouse Y") * (sensitivityY * Time.deltaTime);

            rotV = Mathf.Clamp(rotV, -65, 65);

            rot();
            ray();
        }
    }

    private void rot()
    {
        player.transform.rotation = Quaternion.Euler(player.transform.rotation.x, rotH, player.transform.rotation.z);
        transform.rotation = Quaternion.Euler(rotV, rotH, transform.rotation.z);
    }

    public S_Inventory inventory;

    private void ray()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit, 100))
        {
            if(hit.transform.tag == "pick")
            {
                if(Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("xbox pick up"))
                {
                    inventory.add(hit.transform.GetComponent<Item>());
                }
            }
        }
    }

    public TMP_Text textAmmo;
    public TMP_Text textTotAmmo;

}
