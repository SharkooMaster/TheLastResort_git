using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_playerMovement : MonoBehaviour
{
    [SerializeField] float wSpeed;
    [SerializeField] float rSpeed;
    float speed;

    private float moveX;
    private float moveZ;

    private void Update()
    {
        moveX = Input.GetAxis("Horizontal") * (speed * Time.deltaTime);
        moveZ = Input.GetAxis("Vertical")   * (speed * Time.deltaTime);

        speed = (Input.GetKey(KeyCode.LeftShift)) ? rSpeed : wSpeed;

        move();
    }

    private void move()
    {
        transform.Translate(moveX, 0, moveZ);
    }
}
