using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_VehicleCam : MonoBehaviour
{
    [SerializeField] private GameObject _vehicle;

    [SerializeField] private float sensitivityX;
    [SerializeField] private float sensitivityY;
    [SerializeField] private float mouseX;
    [SerializeField] private float mouseY;

    [SerializeField] private float offsetY;
    [SerializeField] private float initOffsetY;
    
    [SerializeField] private float magnitude;
    [SerializeField] private float initMagnitude;
    [SerializeField] private float closeMagnitude;
    [SerializeField] private float colDist;

    private void Start()
    {
        _vehicle = transform.parent.gameObject;
    }

    private void Update()
    {
        mouseX += Input.GetAxis("Mouse X") * sensitivityX * Time.deltaTime;
        mouseY += Input.GetAxis("Mouse Y") * sensitivityY * Time.deltaTime;

        transform.position = _vehicle.transform.position - sphericalCoord() + new Vector3(0, offsetY, 0);
        transform.LookAt(_vehicle.transform.position + new Vector3(0, offsetY, 0));
    }

    [SerializeField] private float declineCollisionSpeed;
    [SerializeField] private float InclineCollisionSpeed;

    private void LateUpdate()
    {
        RaycastHit col;
        if (Physics.SphereCast(transform.localPosition, 0.3f, Vector3.down, out col, 0.5f) || Physics.SphereCast(transform.localPosition, 0.5f, Vector3.left, out col, 1) || 
            Physics.SphereCast(transform.localPosition, 0.5f, Vector3.right, out col, 1) || Physics.SphereCast(transform.localPosition, 0.5f, Vector3.back, out col, 0.3f))
        {
            if (magnitude > closeMagnitude)
            {
                magnitude -= declineCollisionSpeed * Time.deltaTime;
            }
        }
        else
        {
            if (magnitude < initMagnitude)
            {
                magnitude += InclineCollisionSpeed * Time.deltaTime;
            }
        }
    }

    Vector3 sphericalCoord()
    {
        float _x = magnitude * Mathf.Sin(mouseY) * Mathf.Cos(mouseX);
        float _y = magnitude * Mathf.Cos(mouseY);
        float _z = -magnitude * Mathf.Sin(mouseY) * Mathf.Sin(mouseX);
        return new Vector3(_x, _y, _z);
    }
}
