using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions {

	public class RunToPosAT : ActionTask {

		public BBParameter<Vector3> destinationBBP;
		public BBParameter<float> runMaxSpeed;
		public BBParameter<float> runAccelTime;
		public BBParameter<float> runDecelTime;
		public BBParameter<BtFixedUpdate> myFixedUpdateBBP;
		public BBParameter<bool> hasCheeseBBP;
		public BBParameter<float> feedRangeBBP;
		public BBParameter<bool> feedRotundBBP;

		public float stopBuffer;
		
		private Rigidbody rb;
		private float accel;
		private float decel;
		private float slowingDist;
		float runSpeed;
		public BBParameter<Cheese> cheeseScriptBBP;

		private bool withinSlowDist;
		//Use for initialization. This is called only once in the lifetime of the task.
		//Return null if init was successfull. Return an error string otherwise
		protected override string OnInit() {


			rb = agent.GetComponent<Rigidbody>();

            return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
			myFixedUpdateBBP.value.fixedUpdateCall.AddListener(MyFixedUpdate);
			accel = runMaxSpeed.value / runAccelTime.value;
			decel = runMaxSpeed.value / runDecelTime.value;
			runSpeed = 0;
			Vector3 resetRotation = new Vector3(0f,rb.rotation.y,0f);
			rb.transform.localEulerAngles = resetRotation;
			slowingDist = runMaxSpeed.value / 2 * runDecelTime.value; //Used https://math.stackexchange.com/questions/2820689/calculate-stopping-distance-from-deceleration-time-and-speed#:~:text=Since%20we%20know%20that%20after,%E2%88%921m/s2.
            //EndAction(true);
        }

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
			if ((destinationBBP.value - agent.transform.position).magnitude <= slowingDist)
			{
				withinSlowDist = true;
			}

			if ((destinationBBP.value - agent.transform.position).magnitude <= stopBuffer)
			{
				rb.linearVelocity = Vector3.zero;
				EndAction(true);
			}

			if (hasCheeseBBP.value && (destinationBBP.value - agent.transform.position).magnitude < feedRangeBBP.value)
			{

				cheeseScriptBBP.value.cheeseIsEaten = true;
				hasCheeseBBP.value = false;
			}
        }

		//Called when the task is disabled.
		protected override void OnStop() {
            myFixedUpdateBBP.value.fixedUpdateCall.RemoveListener(MyFixedUpdate);
        }

		public void MyFixedUpdate()
		{
			
			Vector3 directionToTarget = (destinationBBP.value - agent.transform.position).normalized;

			//if (!withinSlowDist)
			//{
				runSpeed += accel * Time.deltaTime;
			//}
			//else
			//{
			//	runSpeed -= decel * Time.deltaTime;
			//}
			if (runSpeed > runMaxSpeed.value)
			{
				runSpeed = runMaxSpeed.value;
			}

			if (runSpeed < 0)
			{
				runSpeed = 0;
			}

			//rb.angularVelocity = Vector3.zero;
			rb.linearVelocity = runSpeed * directionToTarget;

		
		}
		//Called when the task is paused.
		protected override void OnPause() {
			
		}
	}
}