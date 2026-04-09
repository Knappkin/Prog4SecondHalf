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
    [SerializeField] private Color normalColour;
    [SerializeField] private Color trappedColour;

    //Cheese Carrying Variables
    public bool isHoldingCheese;
    public bool loseCheese;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerRotation = transform.localEulerAngles;
        timeTrapped = 0f;

        isHoldingCheese = true;
    }

    // Update is called once per frame
    void Update()
    {
       

        if (Input.GetKey(KeyCode.A) && !isTrapped)
        {
            playerRotation.y -= rotationSpeed * Time.deltaTime;
        }

        else if (Input.GetKey(KeyCode.D) && !isTrapped)
        {
            playerRotation.y += rotationSpeed * Time.deltaTime;
        }


        if (!isTrapped)
        {
            transform.localEulerAngles = playerRotation;
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }

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
}
