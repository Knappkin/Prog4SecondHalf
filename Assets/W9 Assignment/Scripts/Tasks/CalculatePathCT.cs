using NodeCanvas.Framework;
using ParadoxNotion.Design;
using Unity.AI.Navigation;
using Unity.Android.Gradle;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;


namespace NodeCanvas.Tasks.Conditions {

	public class CalculatePathCT : ConditionTask {

		public BBParameter<Transform> builderTargetPosBBP;
		public BBParameter<Transform> targetPosBBP;
		public BBParameter<float> paddingBBP;
		public BBParameter<NavMeshAgent> navAgentBBP;
        public BBParameter<GameObject> builderBBP;
        public BBParameter<NavMeshPath> navPathBBP;
		public NavMeshSurface navMeshSurface;
		public Blackboard blackboard;
		BuilderScript builderScript;
		

        //Use for initialization. This is called only once in the lifetime of the task.
        //Return null if init was successfull. Return an error string otherwise
        protected override string OnInit(){
            builderScript = builderBBP.value.GetComponent<BuilderScript>();
           
            return null;

		}

		//Called whenever the condition gets enabled.
		protected override void OnEnable() {

            navMeshSurface.BuildNavMesh();
            navPathBBP.value = new NavMeshPath();

        }

		//Called whenever the condition gets disabled.
		protected override void OnDisable() {
			
		}

		//Called once per frame while the condition is active.
		//Return whether the condition is success or failure.
		protected override bool OnCheck() {
            if (navAgentBBP.value.CalculatePath(builderTargetPosBBP.value.transform.position, navPathBBP.value))
            {
               
                return true;
            }

			else
			{
				Debug.Log("BIG FAILURE");
				return false;
			}
		}
	}
}