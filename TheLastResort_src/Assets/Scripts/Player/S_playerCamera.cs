using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    float rotH;
    float rotV;

    private void Update()
    {
        rotH += Input.GetAxis("Mouse X") * (sensitivityX * Time.deltaTime);
        rotV -= Input.GetAxis("Mouse Y") * (sensitivityY * Time.deltaTime);

        rotV = Mathf.Clamp(rotV, -65, 65);

        if (toggle)
        {
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
                if(Input.GetKeyDown(KeyCode.E))
                {
                    inventory.add(hit.transform.GetComponent<Item>());
                }
            }
        }
    }
}
