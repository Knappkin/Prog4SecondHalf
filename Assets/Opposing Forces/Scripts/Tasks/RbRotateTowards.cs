using NodeCanvas.Framework;
using ParadoxNotion.Design;
using Unity.VisualScripting;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions {

	public class RbRotateTowards : ActionTask {

		public BBParameter<Transform> targetBBP;
		public BBParameter<Rigidbody> rbBBP;

		public float turnSpeed;
		public float turnThreshold;

		private Rigidbody targetRB;
		public bool successOnFinish;
		//Use for initialization. This is called only once in the lifetime of the task.
		//Return null if init was successfull. Return an error string otherwise
		protected override string OnInit() {
			targetRB = targetBBP.value.GetComponent<Rigidbody>();
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

			Vector3 targetDirection = (targetBBP.value.transform.position  - agent.transform.position).normalized;
			//Vector3 newRot = rbBBP.value.transform.localEulerAngles;
			//Vector3 currentDirection 
			float angle = Mathf.Atan2(targetDirection.z, targetDirection.x) *Mathf.Rad2Deg;

			//float rotundDotTarget = Vector3.Dot(agent.transform.forward, targetDirection);

			//if (rotundDotTarget < 0)
			//{
			//	rbBBP.value.angularVelocity = new Vector3(0f, -turnSpeed * Mathf.Deg2Rad, 0f);
			//}

			//if (rotundDotTarget > 0)
			//{
			//             rbBBP.value.angularVelocity = new Vector3(0f, turnSpeed * Mathf.Deg2Rad, 0f);
			//         }


			//Debug.Log(rbBBP.value.angularVelocity);

			rbBBP.value.MoveRotation(Quaternion.Euler(0f, angle, 0f));
			

        }

		//Called when the task is disabled.
		protected override void OnStop() {
			
		}

		//Called when the task is paused.
		protected override void OnPause() {
			
		}
		
	}
}