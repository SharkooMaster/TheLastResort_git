using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Car : MonoBehaviour
{
    [SerializeField]private GameObject player;
    [SerializeField]private GameObject carCam;

    [SerializeField] private bool carActive = false;

    private void Start()
    {
        carCam.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("test");
            if(Input.GetKeyDown(KeyCode.F))
            {
                player.SetActive(false);
                carCam.SetActive(true);
                carActive = true;
            }
        }
    }

    private float moveX;
    private float moveZ;

    [SerializeField] private float speed;

    private void driveCar()
    {
        moveX = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        moveZ = Input.GetAxis("Vertical") * speed * Time.deltaTime;

        moveCar();
    }

    private void moveCar()
    {
        transform.Translate(transform.worldToLocalMatrix.MultiplyVector(transform.right) * moveZ);
        transform.Rotate(0, moveX, 0);
    }

    private void Update()
    {
        if(carActive)
        {
            driveCar();
        }
    }
}
