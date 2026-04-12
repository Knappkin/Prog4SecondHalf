using NodeCanvas.BehaviourTrees;
using NodeCanvas.Framework;
using NodeCanvas.Tasks.Actions;
using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed;
    public float rotationSpeed;
    Vector3 playerRotation;
    public bool isTrapped;
    public float trapTimeLimit;
    private float timeTrapped;
    private Rigidbody rb;
    [SerializeField] private Color normalColour;
    [SerializeField] private Color trappedColour;

    public GameObject rotund;
    public Cheese cheeseScript;

    //Cheese Carrying Variables
    public bool isHoldingCheese;
    public bool loseCheese;

    private float moveInput;
    private bool isKnockedBack;
    private bool knockMeBack;
    private Vector3 knockBackDirection;
    private float knockBackTime;
    private float stunTime;

     private float knockbackSpeed;
    [SerializeField] private float bumpSpeed;
    [SerializeField] private float runoverSpeed;
    [SerializeField] private float runoverStunTime;
    [SerializeField] private float bumpStunTime;
    [SerializeField] private float resistance;

    private float Decel = 3f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerRotation = rb.transform.localEulerAngles;
        timeTrapped = 0f;

        isHoldingCheese = true;
    }

    // Update is called once per frame
    void Update()
    {

        moveInput = Input.GetAxisRaw("Horizontal");
       

        if (isTrapped)
        {
            GetComponentInChildren<Renderer>().material.color = trappedColour;
          timeTrapped += Time.deltaTime;
            if (timeTrapped > trapTimeLimit)
            {
                GetComponentInChildren<Renderer>().material.color = normalColour;
                timeTrapped = 0f;
                isTrapped = false;
            }
        }

        }
    private void FixedUpdate()
    {
        
        if (moveInput < 0)
        {
            playerRotation.y -= rotationSpeed * Time.fixedDeltaTime;
        }

        if (moveInput > 0)
        {
            playerRotation.y += rotationSpeed * Time.fixedDeltaTime;
        }

        if (isTrapped)
        {
            rb.linearVelocity = Vector3.zero;
            
        }

        if (knockMeBack)
        {
            knockMeBack = false;
            rb.linearVelocity = knockbackSpeed * knockBackDirection;
            isKnockedBack = true;
            StartCoroutine(KnockBackTimer());
            StartCoroutine(invulFromRatTimer());
        }
        
        if (isKnockedBack)
        {
            rb.linearVelocity += -knockBackDirection * Decel * Time.deltaTime;
        }
        if (!isTrapped && !isKnockedBack)
        {
            rb.transform.localEulerAngles = playerRotation;
            rb.linearVelocity = transform.forward * moveSpeed;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        LayerMask colLayer = collision.gameObject.layer;        //if (collision.gameObject.layer == 11) //Checks trap layer
        //{
        //    moveInput = 0; //Without this player would sometimes keep spinning while trapped
        //    isTrapped = true;
        //    collision.gameObject.GetComponent<TrapBehaviour>().BreakTrap(0);
        //}

        if (colLayer == 9)
        {
            Physics.IgnoreLayerCollision(8,9,true);
            if (rotund.GetComponent<BehaviourTreeOwner>().GetComponent<Blackboard>().GetVariable<bool>("isRolling").value == false)
            {
                knockbackSpeed = bumpSpeed;
                stunTime = bumpStunTime;
                
            }
            else
            {
                knockbackSpeed = runoverSpeed;
                stunTime = runoverStunTime;

                cheeseScript.LaunchCheese();
            }
            knockMeBack = true;
            knockBackDirection = (rb.transform.position - collision.gameObject.transform.position).normalized;

            
        }
    }

    private IEnumerator invulFromRatTimer()
    {
        yield return new WaitForSeconds(3);
        Physics.IgnoreLayerCollision(8, 9, false);
    }
    private IEnumerator KnockBackTimer()
    {
        yield return new WaitForSeconds(stunTime);
        isKnockedBack = false;
    }
}
