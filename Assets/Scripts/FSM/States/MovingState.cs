using UnityEngine;

namespace AI.FSM {

    public class MovingState : FSMState {
        private Vector2 _direction;
        protected override void init() {
            this.StateID = FSMStateID.Moving;
        }

        public override void OnStateStay(FSMBase fsm) {
            base.OnStateStay(fsm);

            // get user input
            _direction.Set(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

            // update animator
            Vector2 lookDirection = _direction.normalized;
            fsm.animator.SetFloat("Look X", lookDirection.x);
            fsm.animator.SetFloat("Look Y", lookDirection.y);
        }

        public override void OnStateFixedStay(FSMBase fsm) {
            base.OnStateFixedStay(fsm);

            // update player speed according to input
            Rigidbody2D rb = fsm.GetComponent<Rigidbody2D>();
            if (rb != null) {
                CharacterFSM characterFSM = fsm.GetComponent<CharacterFSM>();
                if (characterFSM != null) {
                    rb.velocity = _direction * characterFSM.Speed;
                }
            }
        }

        public override void OnStateExit(FSMBase fsm) {
            base.OnStateExit(fsm);

            // reset player velocity to zero
            Rigidbody2D rb = fsm.GetComponent<Rigidbody2D>();
            if (rb != null) {
                rb.velocity = Vector2.zero;
            }
        }
    }
}