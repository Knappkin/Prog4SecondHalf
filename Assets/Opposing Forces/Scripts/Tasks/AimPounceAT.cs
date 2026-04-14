using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions {

	public class AimPounceAT : ActionTask {

        public BBParameter<BtFixedUpdate> myFixedUpdateBBP;
       
        public BBParameter<Transform> playerBBP;
        public BBParameter<Transform> pounceTargetBBP;

        private Vector3 playerPosThisFrame;

        private Vector3 playerDirectionLastFrame;
        private Vector3 playerDirectionThisFrame;

        public BBParameter<LineRenderer> aimLineBBP;

        public float trackSpeed;
        public float distAhead;

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
            aimLineBBP.value.enabled = true;
            //EndAction(true);
            myFixedUpdateBBP.value.fixedUpdateCall.AddListener(MyFixedUpdate);
            playerDirectionThisFrame = playerBBP.value.GetComponent<Rigidbody>().transform.forward;
        }

        //Called once per frame while the action is active.
        protected override void OnUpdate()
        {

           // agent.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
            //agent.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

        
        }

        //Called when the task is disabled.
        protected override void OnStop()
        {
            myFixedUpdateBBP.value.fixedUpdateCall.RemoveListener(MyFixedUpdate);
            aimLineBBP.value.enabled = false;
        }

        private void MyFixedUpdate()
        {
            playerPosThisFrame = playerBBP.value.GetComponent<Rigidbody>().position;

            playerDirectionLastFrame = playerDirectionThisFrame;
            playerDirectionThisFrame = playerBBP.value.GetComponent<Rigidbody>().transform.forward;

            Vector3 directionChange = (playerDirectionThisFrame - playerDirectionLastFrame).normalized;

            Vector3 newDirection = (playerBBP.value.GetComponent<Rigidbody>().transform.forward + directionChange).normalized;


            pounceTargetBBP.value.transform.position = playerPosThisFrame + newDirection * distAhead;
        }
       
    }
}