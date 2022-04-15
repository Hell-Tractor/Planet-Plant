using UnityEngine;

namespace AI.FSM {
    public class JumpStartTrigger : FSMTrigger {
        protected override void init() {
            this.TriggerID = FSMTriggerID.JumpStart;
        }

        public override bool HandleTrigger(FSMBase fsm) {
            return Input.GetButton("Jump");
        }
    }
}