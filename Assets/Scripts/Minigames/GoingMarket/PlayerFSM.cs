using UnityEngine;
using AI.FSM;

namespace Minigame.GM {

    public class PlayerFSM : FSMBase {
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
    }

}