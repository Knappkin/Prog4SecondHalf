using NodeCanvas.Framework;
using ParadoxNotion.Design;
using System.Collections;
using UnityEngine;

namespace NodeCanvas.Tasks.Actions {

	public class WallCrashAT : ActionTask {

        public BBParameter<BtFixedUpdate> btFixedUpdateBBP;
        public BBParameter<Vector3> contactPointBBP;
        public BBParameter<bool> isRollingBBP;
        public BBParameter<float> stunDurationBBP;
        public BBParameter<bool> canChaseCheeseBBP;
        public float reboundVelo;
        private float currentDecel;
        // public BBParameter<>

        private float rotundDecel;
        private Rigidbody rb;
        private Vector3 knockbackDirection;

        //Use for initialization. This is called only once in the lifetime of the task.
        //Return null if init was successfull. Return an error string otherwise
        protected override string OnInit()
        {
            rb = agent.GetComponent<Rigidbody>();
           
            return null;
        }

        //This is called once each time the task is enabled.
        //Call EndAction() to mark the action as finished, either in success or failure.
        //EndAction can be called from anywhere.
        protected override void OnExecute()
        {
            reboundVelo = Mathf.Abs(rb.linearVelocity.magnitude) * 0.6f;
            rotundDecel = reboundVelo / stunDurationBBP.value;
            knockbackDirection = GetReboundDirection();
            rb.angularVelocity = Vector3.zero;
            currentDecel = 0f;
            btFixedUpdateBBP.value.fixedUpdateCall.AddListener(MyFixedUpdate);
            StartCoroutine(ActionTimer());
        }


        private void MyFixedUpdate()
        {
           
            rb.linearVelocity = knockbackDirection * reboundVelo + (-knockbackDirection * currentDecel);
            currentDecel += rotundDecel * Time.deltaTime;
          
        }
        private Vector3 GetReboundDirection()
        {
            Vector3 incomingDirection = (contactPointBBP.value - agent.transform.position).normalized;
            Vector3 reboundDirection = -incomingDirection;
            return reboundDirection;
        }

       private IEnumerator ActionTimer()
        {
            yield return new WaitForSeconds(3);
            //Debug.Log("Waited the 3 seconds");
            isRollingBBP.value = false;
            rb.linearVelocity = Vector3.zero;
            btFixedUpdateBBP.value.fixedUpdateCall.RemoveListener(MyFixedUpdate);
            canChaseCheeseBBP.value = true;
            EndAction(true);
        }
    }
}