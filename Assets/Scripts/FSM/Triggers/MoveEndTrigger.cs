using UnityEngine;

namespace AI.FSM {

    public class MoveEndTrigger : FSMTrigger {
        protected override void init() {
            this.TriggerID = FSMTriggerID.MoveEnd;
        }
        public override bool HandleTrigger(FSMBase fsm) {
            return Mathf.Approximately(Input.GetAxis("Horizontal"), 0) && Mathf.Approximately(Input.GetAxis("Vertical"), 0);
        }

    }

}