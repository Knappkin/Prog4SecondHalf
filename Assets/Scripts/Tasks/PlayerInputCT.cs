using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace NodeCanvas.Tasks.Conditions {

	public class PlayerInputCT : ConditionTask {

	
		protected override bool OnCheck() {
			return Input.anyKey;
		}
	}
}