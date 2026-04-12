using NodeCanvas.BehaviourTrees;
using NodeCanvas.Tasks.Actions;
using UnityEngine;
using UnityEngine.Events;

public class BtFixedUpdate : MonoBehaviour
{

    public UnityEvent fixedUpdateCall;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
 
    // Update is called once per frame
    private void FixedUpdate()
    {
        fixedUpdateCall.Invoke();
    }
}
