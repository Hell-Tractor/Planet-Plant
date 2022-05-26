using UnityEngine;

namespace BUFF {
    public class WalkSpeedDown : BuffBase {
        public float SpeedDownRate = 0.8f;
        private float _initSpeed;
        public override void OnBuffEnter(bool isEndByEmo, GameObject target) {
            base.OnBuffEnter(isEndByEmo, target);

            AI.FSM.CharacterFSM fsm = target.GetComponent<AI.FSM.CharacterFSM>();
            if (fsm != null) {
                // record initial speed
                _initSpeed = fsm.Speed;
                fsm.Speed = fsm.Speed * SpeedDownRate;
            }
        }

        public override void OnBuffExit(GameObject target) {
            base.OnBuffExit(target);

            AI.FSM.CharacterFSM fsm = target.GetComponent<AI.FSM.CharacterFSM>();
            if (fsm != null) {
                // recover initial speed
                fsm.Speed = _initSpeed;
            }
        }

        public override void Init() {
            this.BuffID = BuffID.WalkSpeedDown;
            this.BuffType = BuffType.Debuff;
        }
    }
}