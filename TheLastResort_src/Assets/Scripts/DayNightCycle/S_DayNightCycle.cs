using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_DayNightCycle : MonoBehaviour
{
    [SerializeField] GameObject lightSource;

    [SerializeField] float dayLength;       // Minutes
    [SerializeField] float lerpFrequency;
    [SerializeField] float rotx = 0;

    [SerializeField]int min, max;

    private void Start()
    {
        //lerpFrequency = (dayLength * 0.1f) / max;
    }

    private void Update()
    {
        lightSource.transform.Rotate(lerpFrequency * Time.deltaTime, 0, 0);
    }
}
