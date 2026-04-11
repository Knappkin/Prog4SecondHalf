using System.Numerics;
using UnityEngine;

public class RbRotatePhysics : MonoBehaviour
{

    public GameObject rotatingObject;

    public bool isRotating;
    public bool finishRotating;

    public float turnThreshold;
    public float turnspeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isRotating)
        {

        }
    }

    public void SetRotationValues(GameObject rotatingObject, float turnSpeed, float turnThreshold, GameObject RotationTarget, bool rotateUntilFinished)
    {

    }
}
