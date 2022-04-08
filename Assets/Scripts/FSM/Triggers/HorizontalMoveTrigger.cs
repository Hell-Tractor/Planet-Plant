using UnityEngine;

namespace AI.FSM {

    public class HorizontalMoveTrigger : FSMTrigger {
        protected override void init() {
            this.TriggerID = FSMTriggerID.HorizontalMove;
        }
        public override bool HandleTrigger(FSMBase fsm) {
            return !Mathf.Approximately(Input.GetAxis("Horizontal"), 0);
        }

    }

}