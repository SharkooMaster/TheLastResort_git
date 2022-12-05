using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_PublicTransport_HoveringTrain : MonoBehaviour
{
    [SerializeField] private List<Vector3> positions;
    [SerializeField] private int currentStation = 0;
    [SerializeField] private List<float> stationStopTime;
    private float nextStopTime = 0;

    [SerializeField] private float speed;
    [SerializeField] private float rotSpeed;

    private void moveToStation()
    {
        Quaternion targetRotation = Quaternion.LookRotation(positions[currentStation] - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotSpeed * Time.deltaTime);

        transform.Translate(transform.worldToLocalMatrix.MultiplyVector(transform.forward) * speed * Time.deltaTime);
        //GetComponent<Rigidbody>().AddRelativeForce(transform.worldToLocalMatrix.MultiplyVector(transform.forward) * speed * Time.deltaTime);
        arrived();
    }

    private void arrived()
    {
        if(Vector3.Distance(transform.position, positions[currentStation]) < 2)
        {
            nextStopTime = Time.time + stationStopTime[currentStation];
            nextStation();
        }
    }

    private void nextStation()
    {
        if(currentStation < positions.Count - 1)
        {
            currentStation++;
        }else if(currentStation == positions.Count - 1)
        {
            currentStation = 0;
        }
    }

    private void Update()
    {
        if(Time.time >= nextStopTime)
        {
            moveToStation();
        }
    }
}
