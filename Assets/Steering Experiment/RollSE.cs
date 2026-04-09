using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace NodeCanvas.Tasks.Actions {

	public class RollSE : ActionTask {

		public BBParameter<Transform> targetBBP;
		public float maxRollSpeed;
		public float rollAccelTime;
		private float rollAccel;
		public Vector3 rollVelo;
		public BBParameter<float> turnSpeed;

		public float baseTurnSpeed;
		private float turnAccel;
		public float turnAccelTime;
		public float maxTurnSpeed;

		public float baseRollSpeed;

		//Use for initialization. This is called only once in the lifetime of the task.
		//Return null if init was successfull. Return an error string otherwise
		protected override string OnInit() {

			rollAccel = (maxRollSpeed-baseRollSpeed) / rollAccelTime;
			turnAccel = maxTurnSpeed / turnAccelTime;

			return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
			rollVelo = agent.transform.forward * baseRollSpeed;
			turnSpeed.value = 0;
			//EndAction(true);
		}

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
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

				agent.transform.position += rollVelo * Time.deltaTime;

			
		}

		//Called when the task is disabled.
		protected override void OnStop() {
			
		}

		//Called when the task is paused.
		protected override void OnPause() {
			
		}
	}
}