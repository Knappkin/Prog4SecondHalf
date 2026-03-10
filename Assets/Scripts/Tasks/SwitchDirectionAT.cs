using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions {

	public class SwitchDirectionAT : ActionTask {

		public BBParameter<Transform> pointA;
		public BBParameter<Transform> pointB;
		public BBParameter<Transform> targetPosition;

      
        //This is called once each time the task is enabled.
        //Call EndAction() to mark the action as finished, either in success or failure.
        //EndAction can be called from anywhere.
        protected override void OnExecute() {

			if (targetPosition == pointA)
			{
				targetPosition = pointB;
                EndAction();
            }
			else
			{
				targetPosition = pointA;
                EndAction();
            }
				
		}

		
	}
}