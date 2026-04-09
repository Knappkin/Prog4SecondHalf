using UnityEngine;

public class Cheese : MonoBehaviour
{

    public GameObject parentObject;
    public GameObject player;
    private PlayerController playerScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerScript = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LaunchCheese();
        }
    }

    private void LaunchCheese()
    {
        transform.parent = null;
        gameObject.AddComponent<Rigidbody>();
        GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-5,5), Random.Range(0,5), Random.Range(-5,5)), ForceMode.Impulse);
        GetComponentInChildren<MeshCollider>().enabled = true;

        playerScript.isHoldingCheese = false;
    }

    private void GetEatenByRat()
    {

    }

    private void PlayerPickUp()
    {
        
        Destroy(gameObject.GetComponent<Rigidbody>());
        GetComponentInChildren<MeshCollider>().enabled = false;
        transform.parent = parentObject.transform;
        transform.position = transform.parent.position;
        transform.rotation = transform.parent.rotation;
        playerScript.isHoldingCheese = true;
    }

    private void ReturnToPlate()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 8)
        {
            PlayerPickUp();
        }
    }
}
