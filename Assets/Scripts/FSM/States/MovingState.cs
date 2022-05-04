using UnityEngine;

namespace AI.FSM {

    public class MovingState : FSMState {
        private Vector2 _direction;
        protected override void init() {
            this.StateID = FSMStateID.Moving;
        }

        public override void OnStateStay(FSMBase fsm) {
            base.OnStateStay(fsm);

            _direction.Set(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

            Vector2 lookDirection = _direction.normalized;
            fsm.animator.SetFloat("Look X", lookDirection.x);
            fsm.animator.SetFloat("Look Y", lookDirection.y);
        }

        public override void OnStateFixedStay(FSMBase fsm) {
            base.OnStateFixedStay(fsm);

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

            Rigidbody2D rb = fsm.GetComponent<Rigidbody2D>();
            if (rb != null) {
                rb.velocity = Vector2.zero;
            }
        }
    }
}