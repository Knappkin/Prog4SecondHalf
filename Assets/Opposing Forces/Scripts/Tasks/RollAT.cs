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

        public float baseRollSpeed;
        private Rigidbody rb;

        public BBParameter<BtFixedUpdate> btFixedUpdateBBP;
        
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
            isRollingBBP.value = true;
            rollVelo = agent.transform.forward * baseRollSpeed;
            turnSpeed.value = 0;
            btFixedUpdateBBP.value.fixedUpdateCall.AddListener(OnMyFixedUpdate);
        }

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
           

           
        }

        //Called when the task is disabled.
        protected override void OnStop() {
			
		}

		//Called when the task is paused.
		protected override void OnPause() {
			
		}

        public void OnMyFixedUpdate()
        {
            rollVelo += agent.transform.forward * rollAccel * Time.deltaTime;

            rollVelo = Vector3.ClampMagnitude(rollVelo, maxRollSpeed);

            if (turnAccel < 0)
            {
                turnSpeed.value += turnAccel * Time.deltaTime;
            }
            else
            {
                turnSpeed.value = maxTurnSpeed;
            }

            rb.linearVelocity = rollVelo;
           
        }

	}
}