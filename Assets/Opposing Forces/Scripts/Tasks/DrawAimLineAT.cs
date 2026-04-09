using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions {

	public class DrawAimLineAT : ActionTask {
        public BBParameter<LineRenderer> lineRendererBBP;
        public float lineLength;
        //Use for initialization. This is called only once in the lifetime of the task.
        //Return null if init was successfull. Return an error string otherwise
        protected override string OnInit()
        {
            return null;
        }

        //This is called once each time the task is enabled.
        //Call EndAction() to mark the action as finished, either in success or failure.
        //EndAction can be called from anywhere.
        protected override void OnExecute()
        {

            lineRendererBBP.value.enabled = true;
            //EndAction(true);
        }

        //Called once per frame while the action is active.
        protected override void OnUpdate()
        {
            Vector3[] points;

            points = new Vector3[2];
            points[0] = agent.transform.position;
            points[0].y -= 0.4f;
            points[1] = agent.transform.position + lineLength * agent.transform.forward;
            points[1].y -= 0.4f;

            lineRendererBBP.value.SetPositions(points);
        }
    }
}