using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Class for managing the minigame: Going market.
/// </summary>
public class GoingMarketManager : MonoBehaviour {
    /// <summary>
    /// Tranform of exit point
    /// </summary>
    public Transform Exit;
    /// <summary>
    /// Distance of exit point dectection
    /// </summary>
    public float ExitDectectionRadius = 0.1f;
    public BubbleDialogController BubbleDialogController = null;

    private void Start() {
        // if first time playing minigame, show beginner's guide.
        if (GlobalProperties.Instance.isFirstTimeToGoingMarket) {
            GlobalProperties.Instance.isFirstTimeToGoingMarket = false;

            // DialogManager.Instance.ShowDialog(20);
            _setupBeginnerGuide();
            BubbleDialogController?.Show();
        }
    }

    /// <summary>
    /// Init the beginner's guide.
    /// </summary>
    private void _setupBeginnerGuide() {
        BubbleDialogController?.Clear();
        BubbleDialogController?.AddDialog("去集市路途遥远\n需要玩一个小小的游戏\n<b>AD</b>左右移动\n<b>W</b>键跳跃\n收集金币可转化成你的资产哦！", () => false);
    }
    
    private void Update() {
        // detect if player is near the exit point
        RaycastHit2D hit = Physics2D.CircleCast(Exit.position, ExitDectectionRadius, Vector2.zero, 0, LayerMask.GetMask("Player"));
        if (hit.collider != null) {
            SceneManager.LoadScene("Market");
        }
    }
}
