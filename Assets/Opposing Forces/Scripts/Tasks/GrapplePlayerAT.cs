using NodeCanvas.Framework;
using ParadoxNotion.Design;
using System.Collections;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions {

	public class GrapplePlayerAT : ActionTask {

		public BBParameter<Transform> playerBBP;
		public BBParameter<float> grappleTimeBBP;
		public BBParameter<GameObject> LatchSpotBBP;

		private PlayerController playerScript;
		//Use for initialization. This is called only once in the lifetime of the task.
		//Return null if init was successfull. Return an error string otherwise
		protected override string OnInit() {
			playerScript = playerBBP.value.GetComponent<PlayerController>();
			return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
			playerScript.isGrappled = true;
			agent.transform.parent = LatchSpotBBP.value.transform;
			agent.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
            agent.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
			StartCoroutine(HoldOnToPlayer());
            //agent.transform.position = LatchSpotBBP.value.transform.position;

        }

		private IEnumerator HoldOnToPlayer()
		{
			yield return new WaitForSeconds(grappleTimeBBP.value);
			agent.transform.parent = null;
			playerScript.isGrappled = false;
			EndAction();
		}
	}
}