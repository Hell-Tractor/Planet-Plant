using UnityEngine;

namespace AI.FSM {

    public class GroundedTrigger : FSMTrigger {
        protected override void init() {
            this.TriggerID = FSMTriggerID.Grounded;
        }

        public override bool HandleTrigger(FSMBase fsm) {
            foreach (Collider2D collider in fsm.GetComponents<Collider2D>()) {
                ContactFilter2D filter = new ContactFilter2D();
                filter.SetLayerMask(LayerMask.GetMask("Ground"));
                if (collider.Cast(Vector2.down, filter, new RaycastHit2D[1], 0.1f, true) > 0) {
                    return true;
                }
            }
            return false;
        }
    }

}