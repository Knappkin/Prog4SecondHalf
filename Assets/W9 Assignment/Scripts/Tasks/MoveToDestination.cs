using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using UnityEngine.AI;


namespace NodeCanvas.Tasks.Actions {

	public class MoveToDestination : ActionTask {

		public BBParameter<NavMeshAgent> navAgentBBP;
		public BBParameter<Transform> builderTargetPosBBP;
		public Blackboard blackboard;
		public float stopDist;

		//Use for initialization. This is called only once in the lifetime of the task.
		//Return null if init was successfull. Return an error string otherwise
		protected override string OnInit() {
			navAgentBBP.value.stoppingDistance = stopDist;
			return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
			navAgentBBP.value.SetDestination(builderTargetPosBBP.value.transform.position);
			
		}

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
			if (navAgentBBP.value.remainingDistance < navAgentBBP.value.stoppingDistance)
			{
				EndAction(true);
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