using NodeCanvas.BehaviourTrees;
using NodeCanvas.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Cheese : MonoBehaviour
{

    public GameObject parentObject;
    public GameObject player;
    private PlayerController playerScript;
    public float pickupCD;

    public bool cheeseIsDropped;
    public UnityEvent cheeseDropped;

    public UnityEvent cheesePickedUp;

    public GameObject rotund;
    public GameObject riggley;

    public float spawnRadius;

    public float cheeseFlingForce;

    public bool cheeseIsEaten;
    [SerializeField] private float cheeseBufferSpace;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.layer = 10;
        playerScript = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
       if (cheeseIsEaten)
        {
            cheeseIsEaten = false;
            rotund.GetComponent<BehaviourTreeOwner>().GetComponent<Blackboard>().GetVariable<bool>("cheeseIsDropped").value = false;
            riggley.GetComponent<BehaviourTreeOwner>().GetComponent<Blackboard>().GetVariable<bool>("cheeseIsDropped").value = false;
            DropNewCheese();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            LaunchCheese();
        }
    }

   public void LaunchCheese()
    {
       
      
        EnableCheesePhysics();
        GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-cheeseFlingForce, cheeseFlingForce), Random.Range(0, 5), Random.Range(-cheeseFlingForce, cheeseFlingForce)), ForceMode.Impulse);
        playerScript.isHoldingCheese = false;
        StartCoroutine(PickupCooldown());
    }

    public void RiggleyPickup() 
    {
        Destroy(gameObject.GetComponent<Rigidbody>());
        GetComponentInChildren<MeshCollider>().enabled = false;
        transform.parent = riggley.transform;

        transform.position = transform.parent.position;
        Vector3 offsetTransform = transform.position;
        offsetTransform.y += 2f;
        transform.position = offsetTransform;
        transform.rotation = transform.parent.rotation;
        riggley.GetComponent<BehaviourTreeOwner>().GetComponent<Blackboard>().GetVariable<bool>("hasCheese").value = true;
    }

    public void PlayerPickUp()
    {

        Destroy(gameObject.GetComponent<Rigidbody>());
        GetComponentInChildren<MeshCollider>().enabled = false;
        transform.parent = parentObject.transform;
        transform.position = transform.parent.position;
        transform.rotation = transform.parent.rotation;
        playerScript.isHoldingCheese = true;
        rotund.GetComponent<BehaviourTreeOwner>().GetComponent<Blackboard>().GetVariable<bool>("cheeseIsDropped").value = false;
        riggley.GetComponent<BehaviourTreeOwner>().GetComponent<Blackboard>().GetVariable<bool>("cheeseIsDropped").value = false;
    }

    private void DropNewCheese()
    {
        Vector3 dropLocation = new Vector3();
        dropLocation.x = Random.Range(-spawnRadius, spawnRadius);
        dropLocation.z = Random.Range(-spawnRadius, spawnRadius);
        dropLocation.y = 1.5f;

        Collider[] hits = Physics.OverlapSphere(dropLocation, cheeseBufferSpace);

        if (hits.Length == 0)
        {
            dropLocation.y = 6f;

            transform.position = dropLocation;
            EnableCheesePhysics();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 8)
        {
            PlayerPickUp();
        }

        if (collision.gameObject.layer == 13)
        {
            RiggleyPickup();
        }

        if ((collision.gameObject.layer == 14))
        {
            DropNewCheese();
        }
    }

    private void EnableCheesePhysics()
    {
        cheeseDropped.Invoke();
        rotund.GetComponent<BehaviourTreeOwner>().GetComponent<Blackboard>().GetVariable<bool>("cheeseIsDropped").value = true;
        riggley.GetComponent<BehaviourTreeOwner>().GetComponent<Blackboard>().GetVariable<bool>("cheeseIsDropped").value = true;
        transform.parent = null;
        gameObject.AddComponent<Rigidbody>();
        GetComponentInChildren<MeshCollider>().enabled = true;
    }
    private IEnumerator PickupCooldown()
    {
        Debug.Log("StartCoroutine");
        Physics.IgnoreLayerCollision(8, 10, true);
        yield return new WaitForSeconds(pickupCD);
        Physics.IgnoreLayerCollision(8,10,false);
        Debug.Log("EndCoroutine");
    }
}
