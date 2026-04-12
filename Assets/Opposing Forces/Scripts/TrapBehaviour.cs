using UnityEngine;

public class TrapBehaviour : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 8)
        {
            other.gameObject.GetComponent<PlayerController>().isTrapped = true;
        }
        Destroy(gameObject);
    }
    public void BreakTrap(float timeToBreak)
    {
        Destroy(gameObject,timeToBreak);
    }
}
