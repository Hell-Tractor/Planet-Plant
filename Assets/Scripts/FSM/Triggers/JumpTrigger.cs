using UnityEngine;

namespace AI.FSM {
    public class JumpTrigger : FSMTrigger {
        protected override void init() {
            this.TriggerID = FSMTriggerID.Jump;
        }

        public override bool HandleTrigger(FSMBase fsm) {
            return Input.GetButton("Jump");
        }
    }
}