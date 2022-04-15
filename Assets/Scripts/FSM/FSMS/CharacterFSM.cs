using UnityEngine;

namespace AI.FSM {

public class CharacterFSM : FSMBase {

    protected override void setUpFSM() {
        base.setUpFSM();

        IdleState idle = new IdleState();
        idle.AddMap(FSMTriggerID.MoveStart, FSMStateID.Moving);
        _states.Add(idle);

        MovingState moving = new MovingState();
        moving.AddMap(FSMTriggerID.MoveEnd, FSMStateID.Idle);
        _states.Add(moving);
    }
}

}