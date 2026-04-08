using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace NodeCanvas.Tasks.Actions {

	public class ChasePlayerSE : ActionTask {

		public BBParameter<Transform> targetPointBBP;
		public float chaseSpeed;
		public float bufferDist;
		//Use for initialization. This is called only once in the lifetime of the task.
		//Return null if init was successfull. Return an error string otherwise
		protected override string OnInit() {
			return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
			//EndAction(true);
		}

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
			
			Vector3 directionToTarget = (targetPointBBP.value.transform.position - agent.transform.position).normalized;
			agent.transform.position += chaseSpeed * directionToTarget * Time.deltaTime;

			if ((targetPointBBP.value.transform.position - agent.transform.position).magnitude < bufferDist)
			{
				EndAction();
			}
		}

		//Called when the task is disabled.
		protected override void OnStop() {
			
		}

		//Called when the task is paused.
		protected override void OnPause() {
			
		}
	}
}