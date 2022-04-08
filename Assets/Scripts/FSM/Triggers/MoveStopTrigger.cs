using UnityEngine;

namespace AI.FSM {

    public class MoveStopTrigger : FSMTrigger {
        protected override void init() {
            this.TriggerID = FSMTriggerID.MoveStop;
        }
        public override bool HandleTrigger(FSMBase fsm) {
            return Mathf.Approximately(Input.GetAxis("Horizontal"), 0) && Mathf.Approximately(Input.GetAxis("Vertical"), 0);
        }

    }

}