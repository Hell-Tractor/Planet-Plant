using UnityEngine;

namespace AI.FSM {

    public class HorizontalMovingState : FSMState {
        private float _input;
        protected override void init() {
            this.StateID = FSMStateID.HorizontalMoving;   
        }

        public override void OnStateStay(FSMBase fsm) {
            base.OnStateStay(fsm);

            _input = Input.GetAxis("Horizontal");
        }
        public override void OnStateFixedStay(FSMBase fsm) {
            base.OnStateEnter(fsm);
            
            Rigidbody2D rb = fsm.GetComponent<Rigidbody2D>();
            if (rb != null) {
                Minigame.GM.PlayerProperties properties = fsm.GetComponent<Minigame.GM.PlayerProperties>();
                if (properties != null) {
                    rb.velocity = new Vector2(properties.Speed * _input, rb.velocity.y);
                }
            }
        }

    }

}