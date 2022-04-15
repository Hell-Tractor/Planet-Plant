using UnityEngine;

namespace AI.FSM {

    public class MoveStartTrigger : FSMTrigger {
        public override bool HandleTrigger(FSMBase fsm) {
            return !Mathf.Approximately(Input.GetAxis("Horizontal"), 0f) || !Mathf.Approximately(Input.GetAxis("Vertical"), 0f);
        }

        protected override void init() {
            this.TriggerID = FSMTriggerID.MoveStart;
        }
    }

}