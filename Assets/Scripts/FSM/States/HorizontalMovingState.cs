using System;
using UnityEngine;

namespace AI.FSM {

    public class HorizontalMovingState : FSMState {
        private float _input;
        protected override void init() {
            this.StateID = FSMStateID.HorizontalMoving;   
        }

        public override void OnStateStay(FSMBase fsm) {
            base.OnStateStay(fsm);

            // get user input and flip sprite if need
            _input = Input.GetAxis("Horizontal");
            fsm.transform.localScale = new Vector3(
                Math.Sign(_input) * Math.Abs(fsm.transform.localScale.x), 
                fsm.transform.localScale.y,
                fsm.transform.localScale.z
            );
        }
        public override void OnStateFixedStay(FSMBase fsm) {
            base.OnStateEnter(fsm);
            
            // update player speed according to input
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