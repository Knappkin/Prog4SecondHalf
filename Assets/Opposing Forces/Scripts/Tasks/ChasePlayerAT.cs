using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions {

	public class ChasePlayerAT : ActionTask {

        public BBParameter<BtFixedUpdate> myFixedUpdateBBP;
        public BBParameter<Transform> targetPointBBP;
        public float chaseSpeed;
        public float bufferDist;

        private Rigidbody rb;
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
            myFixedUpdateBBP.value.fixedUpdateCall.AddListener(MyFixedUpdate);
            //EndAction(true);
        }

        //Called once per frame while the action is active.
        protected override void OnUpdate()
        {

            if ((targetPointBBP.value.transform.position - agent.transform.position).magnitude < bufferDist)
            {
                rb.linearVelocity = Vector3.zero;
                EndAction();
            }
        }

        protected override void OnStop()
        {
            myFixedUpdateBBP.value.fixedUpdateCall.RemoveListener(MyFixedUpdate);
        }
       private void MyFixedUpdate()
        {
            Vector3 directionToTarget = (targetPointBBP.value.transform.position - agent.transform.position).normalized;

            rb.linearVelocity = chaseSpeed * directionToTarget;
        }
    }
}