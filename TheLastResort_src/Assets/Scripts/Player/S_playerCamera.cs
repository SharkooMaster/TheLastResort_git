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
    [SerializeField] private TMPro.TMP_Text hud_pick;

    private void ray()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit, 100))
        {
            if(hit.transform.tag == "pick")
            {
                hud_pick.text = $"Pick up {hit.transform.GetComponent<Item>()._name}";
                if(Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("xbox pick up"))
                {
                    inventory.add(hit.transform.GetComponent<Item>());
                }
            }
            else
            {
                hud_pick.text= "";
            }

            if(hit.transform.tag == "Vehicle")
            {
                if(Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("xbox pick up"))
                {
                    player.transform.gameObject.SetActive(false);
                    player.transform.position = hit.transform.position;
                    player.transform.parent = hit.transform;
                    hit.transform.GetComponent<S_VehicleMove>().turnOn();
                }
            }
        }
    }

    public void on(GameObject _v)
    {
        player.transform.gameObject.SetActive(true);
        player.transform.parent = null;
        player.transform.position = _v.transform.position;
    }

    public TMP_Text textAmmo;
    public TMP_Text textTotAmmo;

}
