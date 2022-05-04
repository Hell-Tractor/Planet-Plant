using UnityEngine;

namespace AI.FSM {

public class CharacterFSM : FSMBase {
    [Header("属性")]
    public float Speed = 5.0f;
    public int Asset = 30;
    
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