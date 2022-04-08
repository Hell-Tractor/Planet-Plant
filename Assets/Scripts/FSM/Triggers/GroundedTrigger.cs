using UnityEngine;

namespace AI.FSM {

    public class GroundedTrigger : FSMTrigger {
        protected override void init() {
            this.TriggerID = FSMTriggerID.Grounded;
        }

        public override bool HandleTrigger(FSMBase fsm) {
            var transform = fsm.transform;
            RaycastHit2D leftHit = Physics2D.Raycast(
                transform.position + new Vector3(-transform.localScale.x / 2.0f, -transform.localScale.y / 2.0f, 0),
                Vector2.down,
                0.01f,
                ~LayerMask.GetMask("Player")
            );
            RaycastHit2D rightHit = Physics2D.Raycast(
                transform.position + new Vector3(transform.localScale.x / 2.0f, -transform.localScale.y / 2.0f, 0),
                Vector2.down,
                0.01f,
                ~LayerMask.GetMask("Player")
            );
            
            return
                leftHit.collider != null && leftHit.collider.CompareTag("Ground") ||
                rightHit.collider != null && rightHit.collider.CompareTag("Ground");
        }
    }

}