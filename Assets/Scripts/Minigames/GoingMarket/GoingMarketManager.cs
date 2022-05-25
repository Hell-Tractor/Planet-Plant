using UnityEngine;
using UnityEngine.SceneManagement;

public class GoingMarketManager : MonoBehaviour {
    public Transform Exit;
    public float ExitDectectionRadius = 0.1f;
    public BubbleDialogController BubbleDialogController = null;

    private void Start() {
        if (GlobalProperties.Instance.isFirstTimeToGoingMarket) {
            GlobalProperties.Instance.isFirstTimeToGoingMarket = false;

            // DialogManager.Instance.ShowDialog(20);
            _setupBeginnerGuide();
            BubbleDialogController?.Show();
        }
    }

    private void _setupBeginnerGuide() {
        BubbleDialogController?.Clear();
        BubbleDialogController?.AddDialog("去集市路途遥远\n需要玩一个小小的游戏\n<b>AD</b>左右移动\n<b>W</b>键跳跃\n收集金币可转化成你的资产哦！", () => false);
    }
    
    private void Update() {
        RaycastHit2D hit = Physics2D.CircleCast(Exit.position, ExitDectectionRadius, Vector2.zero, 0, LayerMask.GetMask("Player"));
        if (hit.collider != null) {
            SceneManager.LoadScene("Market");
        }
    }
}
