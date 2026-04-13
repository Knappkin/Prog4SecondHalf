using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions {

	public class ChooseTrapLocationAT : ActionTask {


		public BBParameter<Vector3> trapPlacementPosBBP;
		public BBParameter<GameObject> trapPlacementSphereBBP;
		public float trapPlacementRange;
		public float overlapCheckRadius;
		
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

            Vector3 testTrapLocation;
			testTrapLocation.x = Random.Range(-trapPlacementRange, trapPlacementRange);
			testTrapLocation.z = Random.Range(-trapPlacementRange, trapPlacementRange);
			testTrapLocation.y = 0.5f;
			trapPlacementSphereBBP.value.transform.position = testTrapLocation;

			Collider[] hits = Physics.OverlapSphere(testTrapLocation, overlapCheckRadius/2, 11);
			
            if (hits.Length != 0)
			{
				trapPlacementPosBBP.value = testTrapLocation;
				EndAction(true);
			}
        }

	
	}
}