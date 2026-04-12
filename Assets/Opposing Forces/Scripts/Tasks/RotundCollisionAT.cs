using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using System.Collections;

namespace NodeCanvas.Tasks.Actions {

	public class RotundCollisionAT : ActionTask {

		public BBParameter<GameObject> collisionObjectBBP;

		public BBParameter<bool> isRollingBBP;
		public BBParameter<bool> isStunnedBBP;

		private LayerMask coLayer;
		private Rigidbody rb;

		//Use for initialization. This is called only once in the lifetime of the task.
		//Return null if init was successfull. Return an error string otherwise
		protected override string OnInit() {
			rb = agent.GetComponent<Rigidbody>();
			
			//Debug.Log(collisionObjectBBP.value.gameObject.layer);
			//Debug.Log(collisionObjectBBP.value.tag);
			return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
			coLayer = collisionObjectBBP.value.layer;
			Debug.Log(coLayer);
			if (collisionObjectBBP.value.tag == "Trap")
			{
				TrapBehaviour trapScript = collisionObjectBBP.value.GetComponent<TrapBehaviour>();
				trapScript.BreakTrap(0);
			}

			if (coLayer == 12)
			{
				StartCoroutine(HitWall());
			}
			//EndAction(true);
		}

		private IEnumerator HitWall()
		{
			Debug.Log("Started Wall Coroutine");
			float knockbackSpeed = 5f;
			Vector3 knockbackDirection = GetReboundDirection();

			rb.linearVelocity = knockbackSpeed * knockbackDirection;

			yield return new WaitForSeconds(2);
            Debug.Log("Ended Wall Coroutine");
            isStunnedBBP.value = true;
			EndAction(true);
		}

		//public void 
		private Vector3 GetReboundDirection()
		{
			Vector3 incomingDirection = agent.transform.forward;
            Vector3 reboundDirection = -incomingDirection;
			return reboundDirection;
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
	}
}