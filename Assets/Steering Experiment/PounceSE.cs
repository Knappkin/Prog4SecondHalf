using NodeCanvas.Framework;
using ParadoxNotion.Design;
using Unity.VisualScripting;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions {

	public class PounceSE : ActionTask {

		public BBParameter<Transform> pounceTargetBBP;

		private Vector3 pounceDestination;
		public float pounceSpeed;
		public float pounceAccelTime;
		private float pounceAcceleration;
		public float maxPounceSpeed;
		private float pounceVelo;
		public float basePounceVelo;
		//Use for initialization. This is called only once in the lifetime of the task.
		//Return null if init was successfull. Return an error string otherwise
		protected override string OnInit() {

			pounceAcceleration = maxPounceSpeed / pounceAccelTime;
			
			return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
            //pounceDirection = (pounceTargetBBP.value.transform.position - agent.transform.position).normalized;
			pounceVelo = basePounceVelo;
            //EndAction(true);
        }

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
			pounceVelo += pounceAcceleration * Time.deltaTime;

			if (pounceVelo > maxPounceSpeed)
			{
				pounceVelo = maxPounceSpeed;
			}

			agent.transform.position += agent.transform.forward * pounceVelo * Time.deltaTime;
		}

		//Called when the task is disabled.
		protected override void OnStop() {
			
		}

		//Called when the task is paused.
		protected override void OnPause() {
			
		}

		private void OnCollisionEnter(Collision collision)
		{
			if (collision.gameObject.layer == 8)
			{
				EndAction (true);
			}
		}
	}
}