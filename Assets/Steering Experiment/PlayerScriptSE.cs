using UnityEngine;
using UnityEngine.Rendering;

public class PlayerScriptSE : MonoBehaviour
{

    public float moveSpeed;
    public float rotationSpeed;
    Vector3 playerRotation;
    Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerRotation = transform.localEulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            playerRotation.y -= rotationSpeed * Time.deltaTime;
        }

        else if (Input.GetKey(KeyCode.D))
        {
            playerRotation.y += rotationSpeed * Time.deltaTime;
        }

        transform.localEulerAngles = playerRotation;
        transform.position += transform.forward * moveSpeed * Time.deltaTime;

    }
}

           