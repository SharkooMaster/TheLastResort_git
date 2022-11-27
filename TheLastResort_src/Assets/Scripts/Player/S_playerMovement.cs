using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_playerMovement : MonoBehaviour, IHealth
{
    [SerializeField] float wSpeed;
    [SerializeField] float rSpeed;
    float speed;

    private float moveX;
    private float moveZ;

    private Rigidbody rb;
    private float moveY;
    [SerializeField] private float Jump;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        moveX = Input.GetAxis("Horizontal") * (speed * Time.deltaTime);
        moveZ = Input.GetAxis("Vertical")   * (speed * Time.deltaTime);
        moveY = Input.GetAxis("Jump")       *  (Jump * Time.deltaTime);

        speed = (Input.GetKey(KeyCode.LeftShift)) ? rSpeed : wSpeed;

        move();
    }

    private void move()
    {
        transform.Translate(moveX, moveY, moveZ);
    }


    // ############################################### //
    //                     IHealth                     //
    // ############################################### //
    public float health { get; set; }
    public float armour { get; set; }
}
