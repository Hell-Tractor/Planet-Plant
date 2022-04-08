using UnityEngine;
using AI.FSM;

namespace Minigame.GM {

    public class PlayerFSM : FSMBase {
        protected override void setUpFSM() {
            base.setUpFSM();

            IdleState idleState = new IdleState();
            idleState.AddMap(FSMTriggerID.HorizontalMove, FSMStateID.HorizontalMoving);
            idleState.AddMap(FSMTriggerID.Jump, FSMStateID.Jumping);
            this._states.Add(idleState);

            HorizontalMovingState horizontalMovingState = new HorizontalMovingState();
            horizontalMovingState.AddMap(FSMTriggerID.Jump, FSMStateID.Jumping);
            horizontalMovingState.AddMap(FSMTriggerID.MoveStop, FSMStateID.Idle);
            this._states.Add(horizontalMovingState);

            JumpingState jumpingState = new JumpingState();
            jumpingState.AddMap(FSMTriggerID.Grounded, FSMStateID.Idle);
            this._states.Add(jumpingState);
        }
    }

}