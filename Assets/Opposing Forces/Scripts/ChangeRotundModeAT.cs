using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using System.Collections.Generic;


namespace NodeCanvas.Tasks.Actions {

	public class ChangeRotundModeAT : ActionTask {

		public BBParameter<int> rotundModeBBP;
		public BBParameter<GameObject> ballMeshBBP;
		public BBParameter<GameObject> ratMeshBBP;
		public BBParameter<int> ballStageBBP;
		public BBParameter<List<Color>> colourListBBP;
		public BBParameter<float> baseBallScaleBBP;
		

		//Use for initialization. This is called only once in the lifetime of the task.
		//Return null if init was successfull. Return an error string otherwise
		protected override string OnInit() {
			return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
			////SWAP TO BALL
			if (rotundModeBBP.value == 0)
			{

				rotundModeBBP.value = 1;
				ballMeshBBP.value.SetActive(true);
				ratMeshBBP.value.SetActive(false);
				
				ballStageBBP.value = 0;
				
				ballMeshBBP.value.GetComponent<MeshRenderer>().material.color = colourListBBP.value[ballStageBBP.value];

				ballMeshBBP.value.transform.localScale = baseBallScaleBBP.value * Vector3.one;

			}
			
			EndAction(true);
		}

		//Called once per frame while the action is active.
	
	}
}