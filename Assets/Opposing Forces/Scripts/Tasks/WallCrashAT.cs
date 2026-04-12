using NodeCanvas.Framework;
using ParadoxNotion.Design;
using System.Collections;
using UnityEngine;

namespace NodeCanvas.Tasks.Actions {

	public class WallCrashAT : ActionTask {

        public BBParameter<BtFixedUpdate> btFixedUpdateBBP;
        public BBParameter<Vector3> contactPointBBP;
        public BBParameter<bool> isRollingBBP;
        public float reboundVelo;
       // public BBParameter<>

        private Rigidbody rb;
        private Vector3 knockbackDirection;

        //Use for initialization. This is called only once in the lifetime of the task.
        //Return null if init was successfull. Return an error string otherwise
        protected override string OnInit()
        {
            rb = agent.GetComponent<Rigidbody>();
            btFixedUpdateBBP.value.fixedUpdateCall.AddListener(MyFixedUpdate);
            return null;
        }

        //This is called once each time the task is enabled.
        //Call EndAction() to mark the action as finished, either in success or failure.
        //EndAction can be called from anywhere.
        protected override void OnExecute()
        {
            knockbackDirection = GetReboundDirection();
            StartCoroutine(ActionTimer());
            //EndAction(true);
        }

        //Called once per frame while the action is active.
        protected override void OnUpdate()
        {

        }

        private void MyFixedUpdate()
        {
           Debug.Log("THIS HAPPENED");
            rb.linearVelocity = knockbackDirection * reboundVelo;
        }
        private Vector3 GetReboundDirection()
        {
            Vector3 incomingDirection = (contactPointBBP.value - agent.transform.position).normalized;
            Vector3 reboundDirection = -incomingDirection;
            return reboundDirection;
        }

       private IEnumerator ActionTimer()
        {
            yield return new WaitForSeconds(1);
            isRollingBBP.value = false;
            EndAction(true);
        }
    }
}