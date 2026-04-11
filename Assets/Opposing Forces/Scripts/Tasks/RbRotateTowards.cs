using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions {

	public class RbRotateTowards : ActionTask {

		public BBParameter<Transform> targetBBP;
		public BBParameter<Rigidbody> rbBBP;

		public float turnSpeed;
		public float turnThreshold;

		public bool successOnFinish;
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

			Vector3 targetDirection = (targetBBP.value.transform.position - rbBBP.value.transform.position).normalized;
			//Vector3 newRot = rbBBP.value.transform.localEulerAngles;

			float angle = Mathf.Atan2(targetDirection.z, targetDirection.x) *Mathf.Rad2Deg;
			//Quaternion.RotateTowards()
			Quaternion targetQuaternion = Quaternion.Euler(0f, angle, 0f);

			//;

        }

		//Called when the task is disabled.
		protected override void OnStop() {
			
		}

		//Called when the task is paused.
		protected override void OnPause() {
			
		}
		
	}
}