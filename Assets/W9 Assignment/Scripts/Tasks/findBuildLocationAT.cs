using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions {

	public class findBuildLocationAT : ActionTask {

		
		public BBParameter<BuildStats> selectedBuildStatsBBP;
		public BBParameter<Vector2> townDimensionsBBP;
		public BBParameter<float> buildPaddingBBP;
		public BBParameter<Vector3> buildCenterBBP;
		public BBParameter<GameObject> targetPositionBBP;
		public BBParameter<GameObject> builderBBP;
		public BBParameter<int> buildSelectionBBP;
		Vector2 spawnLimits;
		float checkCount;
		public float checkLimit;
		float buildingWidth;
		float buildingLength;
		BuilderScript builderScript;
        BuildStats usedBuildStats;
        //Use for initialization. This is called only once in the lifetime of the task.
        //Return null if init was successfull. Return an error string otherwise
        protected override string OnInit() {
			
			 builderScript = builderBBP.value.GetComponent<BuilderScript>();
			builderScript.ChangeSelectedBuilding(buildSelectionBBP.value);
			usedBuildStats = builderScript.selectedBuildStats;


			buildingWidth = usedBuildStats.buildWidth;
			buildingLength = usedBuildStats.buildLength;
			

            spawnLimits.x = townDimensionsBBP.value.x / 2 - buildingWidth / 2 + buildPaddingBBP.value;
			spawnLimits.y = townDimensionsBBP.value.y / 2 - buildingLength / 2 + buildPaddingBBP.value;
            //EndAction(true);
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
			//Loop checking for

			Vector3 randSpawnPoint = new Vector3();
			randSpawnPoint.x = Random.Range(-spawnLimits.x, spawnLimits.x);
			randSpawnPoint.y = 1;
			randSpawnPoint.z = Random.Range(-spawnLimits.y, spawnLimits.y); ;

            if (checkCount < checkLimit)
			{
				checkCount++;
				if (checkLocation(randSpawnPoint))
				{
					buildCenterBBP.value = randSpawnPoint;
					targetPositionBBP.value.transform.position = buildCenterBBP.value;
					Debug.Log("BAWWWAEAWA");
					EndAction(true);
				}
			}
			else
			{
				EndAction(false);
			}

        }

		//Called when the task is disabled.
		protected override void OnStop() {
			
		}

		//Called when the task is paused.
		protected override void OnPause() {
			
		}

		private bool checkLocation(Vector3 randLocation)
		{

			Collider[] hits = Physics.OverlapBox(randLocation, new Vector3(buildingWidth / 2, buildingLength / 2, 1));

            if (hits.Length != 0)
			{
				return false;
			}

			else
			{
                return true;
            }
				
				
		}
	}
}