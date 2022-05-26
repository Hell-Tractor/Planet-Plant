using UnityEngine;

namespace AI.FSM {
    public class JumpingState : FSMState {
        protected override void init() {
            this.StateID = FSMStateID.Jumping;
        }

        public override void OnStateEnter(FSMBase fsm) {
            base.OnStateEnter(fsm);

            // give player a jump force
            Rigidbody2D rb = fsm.GetComponent<Rigidbody2D>();
            if (rb != null) {
                Minigame.GM.PlayerProperties properties = fsm.GetComponent<Minigame.GM.PlayerProperties>();
                if (properties != null) {
                    rb.velocity = new Vector2(rb.velocity.x, properties.JumpSpeed);
                }
            }
        }

        public override void OnStateExit(FSMBase fsm) {
            base.OnStateExit(fsm);
            
            // reset player vertical speed to zero
            Rigidbody2D rb = fsm.GetComponent<Rigidbody2D>();
            if (rb != null) {
                rb.velocity = new Vector2(rb.velocity.x, 0);
            }
        }
    }
}