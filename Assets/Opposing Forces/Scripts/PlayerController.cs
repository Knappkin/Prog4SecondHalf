using NodeCanvas.Tasks.Actions;
using System;
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

    private bool turnLeft;
    private bool turnRight;

    //Cheese Carrying Variables
    public bool isHoldingCheese;
    public bool loseCheese;

    private float moveInput;
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

        if (!isTrapped)
        {
            
            rb.transform.localEulerAngles = playerRotation;
            rb.linearVelocity = transform.forward * moveSpeed;
        }
        else
        {
            rb.linearVelocity = Vector3.zero;
        }
    }
}
