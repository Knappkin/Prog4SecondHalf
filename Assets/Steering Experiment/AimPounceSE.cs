using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions {

	public class AimPounceSE : ActionTask {

		public BBParameter<Transform> playerBBP;
		public BBParameter<Transform> pounceTargetBBP;

		private Vector3 playerPosLastFrame;
		private Vector3 playerPosThisFrame;
		private Vector3 playerMoveDirection;

		private Vector3 playerDirectionLastFrame;
		private Vector3 playerDirectionThisFrame;

		private float playerAngleLastFrame;
		private float playerAngleThisFrame;

		public float distAhead;
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
			playerPosLastFrame = playerPosThisFrame;
			playerPosThisFrame = playerBBP.value.transform.position;

			playerDirectionLastFrame = playerDirectionThisFrame;
			playerDirectionThisFrame = playerBBP.value.transform.forward;

			playerAngleLastFrame = playerAngleThisFrame;
			//playerAngleThisFrame = Mathf.Atan2()
			playerMoveDirection = (playerPosThisFrame - playerPosLastFrame).normalized;

			pounceTargetBBP.value.transform.position = playerPosThisFrame + (playerMoveDirection * distAhead);
		}

		//Called when the task is disabled.
		protected override void OnStop() {
			
		}

		//Called when the task is paused.
		protected override void OnPause() {
			
		}
	}
}