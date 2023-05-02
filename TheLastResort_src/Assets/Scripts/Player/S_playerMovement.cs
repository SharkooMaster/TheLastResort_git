using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    [SerializeField] private S_Health _shealth;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        _shealth = GetComponent<S_Health>();
    }

    private void Update()
    {
        moveX = Input.GetAxis("Horizontal") * (speed * Time.deltaTime);
        moveZ = Input.GetAxis("Vertical")   * (speed * Time.deltaTime);
        moveY = Input.GetAxis("Jump")       *  (Jump * Time.deltaTime);

        speed = (Input.GetKey(KeyCode.LeftShift)) ? rSpeed : wSpeed;

        if(moveX == 0 && moveZ == 0 && stepSFX.isPlaying) { stepSFX.Stop(); } else if(!stepSFX.isPlaying) { stepSFX.Play(); }
        if(moveX == 0 && moveZ == 0) { alertIdentifier.radius = 0; } else { alertIdentifier.radius = alertRadius; }
        if(Input.GetKeyDown(KeyCode.LeftShift)) { stepSFX.Stop(); stepSFX.clip = runningSfx; stepSFX.Play(); alertRadius += 2; }
        if(Input.GetKeyUp(KeyCode.LeftShift)) { stepSFX.Stop(); stepSFX.clip = walkingSfx; stepSFX.Play(); alertRadius -= 2; }

        move();
    }

    public SphereCollider alertIdentifier;
    public float alertRadius;
    public float nextStep = 0;
    public int stepInterval = 2;
    public AudioSource stepSFX;
    public AudioClip walkingSfx;
    public AudioClip runningSfx;

    private void move()
    {
        transform.Translate(moveX, moveY, moveZ);
    }

    // ############################################### //
    //                     IHealth                     //
    // ############################################### //
    public float health { get; set; }
    public float armour { get; set; }

    public bool danger = false;
    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Ai>() && !danger)
        {
            Ai _ai = other.GetComponent<Ai>();
            _ai.state = 2;
            _ai.target = transform.position;
            _ai.conversationHandler.conversationRunning = true;
            _ai.conversationHandler.triggerAlertClip("Nathan_Alert1");
            danger = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        Ai _ai = other.GetComponent<Ai>();
        _ai.conversationHandler.conversationRunning = false;
    }

    [SerializeField] private Slider hpSlider;
    [SerializeField] private TMPro.TMP_Text hpText;
    private void LateUpdate()
    {
        hpSlider.value = _shealth.health/100;
        hpText.text = _shealth.health.ToString() + " HP";
    }
}
