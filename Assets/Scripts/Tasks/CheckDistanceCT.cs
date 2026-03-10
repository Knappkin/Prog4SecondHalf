using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Conditions {

	public class CheckDistanceCT : ConditionTask {

		public Vector3 positionA;
		public BBParameter<Transform> targetPosition;
		public float distanceTrigger;
		
		//Called once per frame while the condition is active.
		//Return whether the condition is success or failure.
		protected override bool OnCheck() {
			positionA = agent.transform.position;
			float dist = Vector3.Distance(targetPosition.value.transform.position, positionA);
			if (dist < distanceTrigger)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
	}
}