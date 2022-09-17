using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_playerCamera : MonoBehaviour
{
    public GameObject player;

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

        rotV = Mathf.Clamp(rotV, -45, 65);

        rot();
    }

    private void rot()
    {
        player.transform.rotation = Quaternion.Euler(player.transform.rotation.x, rotH, player.transform.rotation.z);
        transform.rotation = Quaternion.Euler(rotV, player.transform.rotation.y, player.transform.rotation.z);
    }
}
