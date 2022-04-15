using UnityEngine;

namespace AI.FSM {

    public class HorizontalMoveStartTrigger : FSMTrigger {
        protected override void init() {
            this.TriggerID = FSMTriggerID.HorizontalMoveStart;
        }
        public override bool HandleTrigger(FSMBase fsm) {
            return !Mathf.Approximately(Input.GetAxis("Horizontal"), 0);
        }

    }

}