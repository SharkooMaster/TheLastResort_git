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

    [Header("Animation")]
    public Animator animator;
    public S_Item_Gun weapon;

    private void Start()
    {
        health = GetComponent<S_Health>();
        speed = walkingSpeed;
        conversationHandler = GetComponent<S_AI_ConversationHandler>();
    }
    float nextShot = 0f;

    private void Update()
    {
        scout();
        if (state == 0 && Time.time >= nextPathTime)
        {
            walkPath();
            hasArrived();
        }else if(state == 2)
        {
            if(Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(target.x, target.z)) > 2f)
            {
                print("With in range");
                transform.LookAt(new Vector3(target.x, 0, target.z));
                isAlert = true;
                //lookAtAlertTarget();
                move();
            }
            else
            {
                animator.SetBool("Walking", false);
                animator.SetBool("Combat", false);
            }
        }else if(state == 3)
        {
            target = hitPlayer.position;
            transform.LookAt(new Vector3(target.x, transform.position.y, target.z));
            isAlert = true;
            if(Time.time >= nextPathTime)
            {
                nextPathTime += Random.Range(0.5f, 1.5f);
                RaycastHit bulletHit;
                Debug.DrawRay(_eyes.transform.position, transform.forward, Color.green, 10);
                hitPlayer.transform.GetComponent<S_Health>().damage(weapon.damage);
                weapon.playShot();
            }
        }
    }
    
    /* ### State0 ###*/
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
        animator.SetBool("Walking", true);
        move();
    }

    private bool hasArrived()
    {
        if (Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), path[currentPath]) < 2)
        {
            Debug.Log("Arrived");
            animator.SetBool("Walking", false);
            nextPathTime = Time.time + pathDelays[currentPath];
            incrementCurrentPath();
            return true;
        }
        Debug.Log("NOT Arrived");
        return false;
    }

    void move()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    [Header("Alert")]
    public Vector3 target;

    /* ### State1 ###*/

    public bool isAlert = false;
    GameObject _transform_player;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "alert" && !isAlert)
        {
            conversationHandler.conversationRunning = true;
            conversationHandler.triggerAlertClip("Nathan_Alert1");
            target = other.transform.position;
            _transform_player = other.transform.gameObject;
            state = 2;
        }

        if(other.tag == "Damagable")
        {
            if(state == 2 && other.gameObject.GetComponent<Ai>().state != 2)
            {
                print("bitch");
                other.gameObject.GetComponent<Ai>().target = target;
                other.gameObject.GetComponent<Ai>().state = 2;
                other.gameObject.GetComponent<Ai>().conversationHandler.conversationRunning = true;
                other.gameObject.GetComponent<Ai>().conversationHandler.triggerAlertClip("Nathan_Alert1");
                other.gameObject.GetComponent<Ai>().target = other.transform.position;
                other.gameObject.GetComponent<Ai>()._transform_player = other.transform.gameObject;
                other.gameObject.GetComponent<Ai>().state = 2;
            }else if(state == 3 && other.gameObject.GetComponent<Ai>().state != 3)
            {
                print("bitch2");
                //other.gameObject.GetComponent<Ai>().target = target;
                other.gameObject.GetComponent<AI_Eyes>().trigger(_transform_player.GetComponent<Collider>());
            }
        }
    }

    public float lookAtSpeed = 4;

    private void lookAtAlertTarget()
    {
        Vector3 direction = target - transform.position;
        Vector3 fDir = new Vector3(direction.x, transform.position.y, direction.z);

        Quaternion toRot = Quaternion.FromToRotation(transform.forward, fDir);
        transform.rotation = Quaternion.Lerp(transform.rotation, toRot, lookAtSpeed * Time.time);
        isAlert = true;
    }

    public void engage()
    {
    }

    /* ### State2 ###*/
    public Transform _eyes;
    public RaycastHit HitInfo;
    public Transform hitPlayer;
    public S_AI_ConversationHandler conversationHandler;
    public void scout()
    {
        //Vector3 toTarget = target - transform.position;
        //if(Vector3.Angle(transform.forward, toTarget) <= 120)
        //{
        //    if(Physics.Raycast(_eyes.transform.position, _eyes.transform.forward, out RaycastHit hit, 1000))
        //    {
        //        if(hit.transform.tag == "Player")
        //        {
        //            if(state != 3)
        //            {
        //                conversationHandler.triggerCombatClip("Nathan_Combat1");
        //                animator.SetBool("Walking", false);
        //                animator.SetBool("Combat", true);
        //            }
        //            // Initiate target.
        //            hitPlayer = hit.transform;
        //            print(hit.transform.name);
        //            state = 3;
        //        }
        //    }
        //}

        //if (Physics.Raycast(_eyes.transform.position, _eyes.transform.forward, out HitInfo, 10.0f))
        //{
        //    if(HitInfo.transform.tag == "Player")
        //    {
        //        // Quote combat.
        //        if(state != 3)
        //        {
        //            conversationHandler.triggerCombatClip("Nathan_Combat1");
        //            animator.SetBool("Walking", false);
        //            animator.SetBool("Combat", true);
        //        }
        //        // Initiate target.
        //        hitPlayer = HitInfo.transform;
        //        print(HitInfo.transform.name);
        //        state = 3;
        //    }
        //}
    }
}
