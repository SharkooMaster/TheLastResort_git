using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ai : MonoBehaviour
{
    /* #### Ai #### */
    [Header("AI")]
    public int id;

    public string _name;
    public string voice;

    public enum states
    {
        Idle,
        Scouting,
        Alert,
        Combat
    };
    public int state = 0;

    [Header("Attributes")]
    public S_Health health;

    public float speed;
    public float walkingSpeed;
    public float runningSpeed;

    public GameObject mainGun;

    [Header("Goal")]
    public List<Vector3> path;
    public List<float> pathDelays;
    public int currentPath;
    public float nextPathTime = 0;

    private void Start()
    {
        health = GetComponent<S_Health>();
    }

    private void Update()
    {
        if (state == 0 && Time.time >= nextPathTime)
        {
            walkPath();
            hasArrived();
        }
    }

    void incrementCurrentPath()
    {
        if (currentPath < path.Count - 1)
        {
            currentPath++;
        }else if (currentPath == path.Count - 1)
        {
            currentPath = 0;
        }
    }

    private void walkPath()
    {
        /* Walk to path */
        Debug.Log("walking");
        transform.LookAt(new Vector3(path[currentPath].x, transform.position.y, path[currentPath].z));
        move();
    }

    private bool hasArrived()
    {
        if (transform.position.x - 2 >= path[currentPath].x && transform.position.y - 2 >= path[currentPath].y && transform.position.z - 2 >= path[currentPath].z ||
            transform.position.x + 2 <= path[currentPath].x && transform.position.y + 2 <= path[currentPath].y && transform.position.z + 2 <= path[currentPath].z)
        {
            nextPathTime = Time.time + pathDelays[currentPath];
            incrementCurrentPath();
            return true;
        }
        return false;
    }

    void move()
    {
        transform.Translate(transform.forward * speed * Time.deltaTime);
    }
}
