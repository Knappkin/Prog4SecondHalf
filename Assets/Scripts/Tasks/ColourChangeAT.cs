using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace NodeCanvas.Tasks.Actions {

	public class ColourChangeAT : ActionTask {

		public BBParameter<Renderer> rendererBBP;
		public Color color = Color.white;
	
		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
			rendererBBP.value.material.color = color;
			EndAction(true);
		}
	}
}