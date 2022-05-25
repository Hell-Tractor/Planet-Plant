using UnityEngine;

public class GameManager : MonoBehaviour {
    public AI.FSM.CharacterFSM CharacterFSM;
    private void Start() {
        TimeManager.Instance.OnDayChange += () => {
            // 母亲每日收入
            GlobalProperties.Instance.FamilyAsset += 100;
            // 每日零花钱
            GlobalProperties.Instance.FamilyAsset -= 2;
            GlobalProperties.Instance.PlayerAsset += 2;
        };
    }
}
