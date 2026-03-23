using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions {

	public class ConstructBuildingAT : ActionTask {

		public BBParameter<GameObject> buildPrefabBBP;
		public BBParameter<GameObject> builderBBP;
		public BBParameter<float> buildTiersBBP;
		public BBParameter<Transform> buildPositionBBP;
		

		BuilderScript builderScript;
		float buildTime;
		Vector3 buildScale;
		BuildStats buildingStats;
		bool Walkable;

		public GameObject buildInstance;
        float tierHight;
		float timePerTier;
		//Use for initialization. This is called only once in the lifetime of the task.
		//Return null if init was successfull. Return an error string otherwise
		protected override string OnInit() {
			builderScript = builderBBP.value.GetComponent<BuilderScript>();
			buildingStats = builderScript.selectedBuildStats;
			 //buildPrefab = buildPrefabBBP.value;
			
			return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
			buildTime = buildingStats.buildTime;
			buildScale.x = buildingStats.buildWidth;
			buildScale.y = buildingStats.buildHeight;
			buildScale.z = buildingStats.buildLength;

			tierHight = buildScale.y / buildTiersBBP.value;
			timePerTier = buildTime / buildTiersBBP.value;

			buildInstance = builderScript.InstantiateBuilding(buildPrefabBBP.value, buildPositionBBP.value);
			
			
			//GameObject buildingInstance =
			EndAction(true);
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