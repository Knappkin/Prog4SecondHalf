using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using System.Collections.Generic;


namespace NodeCanvas.Tasks.Actions {

	public class ChargeUpRollAT : ActionTask {

		public BBParameter<GameObject> ballMeshBBP;
		public BBParameter<Color> ballColor;
		public BBParameter<List<Color>> ballColourListBBP;
		public BBParameter<int> ballStageBBP;
		//Use for initialization. This is called only once in the lifetime of the task.
		//Return null if init was successfull. Return an error string otherwise
		protected override string OnInit() {

			
			return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {

			ballStageBBP.value++;

			Vector3 ballScale = ballMeshBBP.value.transform.localScale;
             ballScale *= 1.2f;
			ballMeshBBP.value.transform.localScale = ballScale;

			ballMeshBBP.value.GetComponent<Renderer>().material.color = ballColourListBBP.value[ballStageBBP.value];

			if (ballStageBBP.value == 4)
			{
				EndAction(true);
			}
			else
			{
				EndAction(false);
			}
			
		}

	}
}