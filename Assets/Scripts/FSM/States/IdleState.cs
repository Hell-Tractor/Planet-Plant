using UnityEngine;

namespace AI.FSM {

    public class IdleState : FSMState {
        protected override void init() {
            this.StateID = FSMStateID.Idle;
        }
    }

}