using UnityEngine;

namespace AI.FSM {

    public class GroundedTrigger : FSMTrigger {
        protected override void init() {
            this.TriggerID = FSMTriggerID.Grounded;
        }

        public override bool HandleTrigger(FSMBase fsm) {
            foreach (Collider2D collider in fsm.GetComponents<Collider2D>()) {
                if (collider.IsTouchingLayers(LayerMask.GetMask("Ground"))) {
                    return true;
                }
            }
            return false;
        }
    }

}