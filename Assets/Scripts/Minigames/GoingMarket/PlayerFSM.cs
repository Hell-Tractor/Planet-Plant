using UnityEngine;
using AI.FSM;
using System;

namespace Minigame.GM {

    public class PlayerFSM : FSMBase {
        public PlayerProperties Properties { get; private set; }
        
        protected override void init() {
            this.transform.localScale = new Vector3(
                Math.Abs(this.transform.localScale.x),
                this.transform.localScale.y,
                this.transform.localScale.z
            );

            Properties = this.GetComponent<PlayerProperties>();
        }
        
        protected override void setUpFSM() {
            base.setUpFSM();

            IdleState idleState = new IdleState();
            idleState.AddMap(FSMTriggerID.HorizontalMoveStart, FSMStateID.HorizontalMoving);
            idleState.AddMap(FSMTriggerID.JumpStart, FSMStateID.Jumping);
            this._states.Add(idleState);

            HorizontalMovingState horizontalMovingState = new HorizontalMovingState();
            horizontalMovingState.AddMap(FSMTriggerID.JumpStart, FSMStateID.Jumping);
            horizontalMovingState.AddMap(FSMTriggerID.MoveEnd, FSMStateID.Idle);
            this._states.Add(horizontalMovingState);

            JumpingState jumpingState = new JumpingState();
            jumpingState.AddMap(FSMTriggerID.Grounded, FSMStateID.Idle);
            this._states.Add(jumpingState);
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if (other.CompareTag("Coin")) {
                this.Properties.CoinCount++;
                Destroy(other.gameObject);
            }
        }
    }
}