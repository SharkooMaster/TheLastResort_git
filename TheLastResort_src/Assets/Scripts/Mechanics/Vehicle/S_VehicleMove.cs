using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_VehicleMove : MonoBehaviour
{
    [SerializeField] private WheelCollider[] wheels;
    [SerializeField] private float enginePower;
    [SerializeField] private float startEnginePower;
    [SerializeField] private float steeringPower;
    [SerializeField] private float brakePower;
    [SerializeField] private bool handBrake;
    [SerializeField] private float downForce;
    [SerializeField] private float[] slip = new float[4];

    [SerializeField] private GameObject centerOfMass;
    [SerializeField] private Rigidbody rb;

    [SerializeField] private AudioSource engineSFX;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = centerOfMass.transform.localPosition;

        Cursor.lockState = CursorLockMode.Locked;

        turnOff();
    }

    [SerializeField] private GameObject cam;
    [SerializeField] private GameObject playerCam;
    [SerializeField] private GameObject playerSpawn;
    public void turnOff()
    {
        GetComponent<S_VehicleMove>().enabled = false;
        cam.SetActive(false);
        playerCam.GetComponent<S_playerCamera>().on(playerSpawn);
    }
    public void turnOn()
    {
        GetComponent<S_VehicleMove>().enabled = true;
        cam.SetActive(true);
    }

    private void FixedUpdate()
    {
        handBrake = Input.GetAxis("Jump") != 0 ? true : false;
        foreach (var wheel in wheels)
        {
            wheel.motorTorque = Input.GetAxis("Vertical") * (enginePower / 4);
        }

        for (int i = 0; i < wheels.Length; i++)
        {
            if(i < 2)
            {
                wheels[i].steerAngle = Input.GetAxis("Horizontal") * steeringPower;
                if(isFlipped)
                {
                    transform.Rotate(0, 0, wheels[i].steerAngle);
                }
            }
        }

        rb.AddForce(-transform.up * downForce * rb.velocity.magnitude); // Downforce

        wheels[2].brakeTorque = wheels[3].brakeTorque = (handBrake) ? brakePower : 0;
        if (!handBrake) { enginePower = startEnginePower; }
        drift();

        float temp = Mathf.Abs(wheels[2].rpm * 0.2f);

        engineSFX.pitch = Mathf.SmoothDamp(-2, 5, ref temp , Time.deltaTime);
    }

    [SerializeField] private bool isFlipped = false;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            turnOff();
        }

        isFlip();
        if (handBrake) { emitDrift(); } else { _emitDrift(); }
    }

    [SerializeField] private WheelFrictionCurve fwFriction;
    [SerializeField] private WheelFrictionCurve swFriction;

    private void drift()
    {
        WheelHit hit;
        for (int i = 0; i < wheels.Length; i++)
        {
            if (wheels[i].GetGroundHit(out hit))
            {
                fwFriction = wheels[i].forwardFriction;
                fwFriction.stiffness = (handBrake) ? Mathf.SmoothDamp(fwFriction.stiffness, 0.5f, ref enginePower, Time.deltaTime) : 1;
                wheels[i].forwardFriction = fwFriction;

                swFriction = wheels[i].sidewaysFriction;
                swFriction.stiffness = (handBrake) ? Mathf.SmoothDamp(fwFriction.stiffness, 0.5f, ref enginePower, Time.deltaTime) : 1;
                wheels[i].sidewaysFriction = swFriction;
            }
        }
    }

    private bool tireMarksFlag;
    [SerializeField] private List<TrailRenderer> tireMarks = new List<TrailRenderer>();
    [SerializeField] private AudioSource SkidSfx;
    private void emitDrift()
    {
        if (tireMarksFlag) return;
        foreach (TrailRenderer T in tireMarks)
        {
            T.emitting = true;
        }
        SkidSfx.Play();
        tireMarksFlag = true;
    }
    private void _emitDrift()
    {
        if (!tireMarksFlag) return;
        foreach (TrailRenderer T in tireMarks)
        {
            T.emitting = false;
        }
        SkidSfx.Stop();
        tireMarksFlag = false;
    }

    private void isFlip()
    {
        if(Vector3.Dot(transform.up, Vector3.down) > 0)
        {
            isFlipped = true;
        }
        else
        {
            isFlipped = false;
        }
    }

}
