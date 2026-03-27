using NodeCanvas.Framework;
using ParadoxNotion.Design;
using System.Collections;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions {

	public class PrepRollAT : ActionTask {

		public BBParameter<AnimationCurve> hopCurve;

		public BBParameter<Rigidbody> rbBBP;
		public Vector3 hopForce;
		public float jumpCD = 5;
		public float t = 0;
		//Use for initialization. This is called only once in the lifetime of the task.
		//Return null if init was successfull. Return an error string otherwise
		protected override string OnInit() {
			return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
			
		}

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
            t += Time.deltaTime;

            if (t > jumpCD)
            {
				Debug.Log("WAHWHAWE");
                hopForce = new Vector3(0, 5, 0);
                rbBBP.value.AddForce(hopForce, ForceMode.Force);
                t = 0;
                //EndAction(true);
            }
        }

		//Called when the task is disabled.
		protected override void OnStop() {
			
		}

		//Called when the task is paused.
		protected override void OnPause() {
			
		}

		private IEnumerator Hop()
		{
			yield return null;
		}
	}
}