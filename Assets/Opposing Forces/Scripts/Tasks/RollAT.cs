using NodeCanvas.Framework;
using ParadoxNotion.Design;
using Unity.VisualScripting;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions {

	public class RollAT : ActionTask {

        public BBParameter<Transform> targetBBP;
        public float maxRollSpeed;
        public float rollAccelTime;
        private float rollAccel;
        public Vector3 rollVelo;
        public BBParameter<float> turnSpeed;

        public BBParameter<bool> isRollingBBP;

        public float baseTurnSpeed;
        private float turnAccel;
        public float turnAccelTime;
        public float maxTurnSpeed;

        public BBParameter<LineRenderer> lineRenderedBBP;

        public float baseRollSpeed;
        private Rigidbody rb;

        public BBParameter<BtFixedUpdate> btFixedUpdateBBP;
        public float turnTimer;
        private float rollTime;
        public BBParameter<bool> goStraightBBP;
        
		//Use for initialization. This is called only once in the lifetime of the task.
        //Return null if init was successfull. Return an error string otherwise
        protected override string OnInit() {
            rb = agent.GetComponent<Rigidbody>();

            rollAccel = (maxRollSpeed - baseRollSpeed) / rollAccelTime;
            turnAccel = maxTurnSpeed / turnAccelTime;

            return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
            lineRenderedBBP.value.enabled = false;
            isRollingBBP.value = true;
            goStraightBBP.value = false;
            rollVelo = agent.transform.forward * baseRollSpeed;
            turnSpeed.value = baseTurnSpeed;
            btFixedUpdateBBP.value.fixedUpdateCall.AddListener(OnMyFixedUpdate);
            rollTime = 0f;
        }

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
            //Debug.Log(rollTime);
            rollTime += Time.deltaTime;

            if (rollTime > turnTimer)
            {
                goStraightBBP.value = true;
            }
           
        }

        //Called when the task is disabled.
        protected override void OnStop() {
            btFixedUpdateBBP.value.fixedUpdateCall.RemoveListener(OnMyFixedUpdate);
        }

		//Called when the task is paused.
		protected override void OnPause() {
			
		}

        public void OnMyFixedUpdate()
        {
            Vector3 addedTurnVector;
            if (goStraightBBP.value == false)
            {
                addedTurnVector = ((targetBBP.value.transform.position - agent.transform.position) * 0.5f).normalized;
            }
            else
            {
                addedTurnVector = Vector3.zero;
            }
                rollVelo += (agent.transform.forward + addedTurnVector) * rollAccel * Time.deltaTime;

            rollVelo = Vector3.ClampMagnitude(rollVelo, maxRollSpeed);

            //if (turnAccel < 0)
            //{
            //    turnSpeed.value += turnAccel * Time.deltaTime;
            //}
            //else
            //{
                turnSpeed.value = maxTurnSpeed;
            //}

            rb.linearVelocity = rollVelo;
           
        }

	}
}