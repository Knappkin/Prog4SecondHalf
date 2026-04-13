using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions {

	public class PounceAT : ActionTask {

        public BBParameter<Transform> pounceTargetBBP;
        public BBParameter<BtFixedUpdate> myFixedUpdateBBP;
        private Vector3 pounceDestination;
        public float pounceSpeed;
        public float pounceAccelTime;
        private float pounceAcceleration;
        public float maxPounceSpeed;
        private float pounceVelo;
        public float basePounceVelo;
        public BBParameter<bool> isPouncingBBP;

        private Rigidbody rb;
        //Use for initialization. This is called only once in the lifetime of the task.
        //Return null if init was successfull. Return an error string otherwise
        protected override string OnInit()
        {
            rb = agent.GetComponent<Rigidbody>();
            pounceAcceleration = (maxPounceSpeed - basePounceVelo) / pounceAccelTime;

            return null;
        }

        //This is called once each time the task is enabled.
        //Call EndAction() to mark the action as finished, either in success or failure.
        //EndAction can be called from anywhere.
        protected override void OnExecute()
        {
            isPouncingBBP.value = true;
            myFixedUpdateBBP.value.fixedUpdateCall.AddListener(MyFixedUpdate);
            //pounceDirection = (pounceTargetBBP.value.transform.position - agent.transform.position).normalized;
            pounceVelo = basePounceVelo;
            //EndAction(true);
        }

        //Called once per frame while the action is active.
        protected override void OnUpdate()
        {
           
        }

        //Called when the task is disabled.
        protected override void OnStop()
        {
            myFixedUpdateBBP.value.fixedUpdateCall.RemoveListener(MyFixedUpdate);
           
        }

     
        private void MyFixedUpdate()
        {
            pounceVelo += pounceAcceleration * Time.deltaTime;

            if (pounceVelo > maxPounceSpeed)
            {
                pounceVelo = maxPounceSpeed;
            }
            rb.angularVelocity = Vector3.zero;
            rb.linearVelocity = pounceVelo * agent.transform.forward;
        }
       
    }
}