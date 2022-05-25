using UnityEngine;

public class HomeManager : MonoBehaviour {
    public BubbleDialogController BubbleDialogController = null;
    private void Start() {
        if (GlobalProperties.Instance.isFirstTimeGoHome) {
            GlobalProperties.Instance.isFirstTimeGoHome = false;

            // DialogManager.Instance.ShowDialog(22);
            _setupBeginnerGuide();
            BubbleDialogController?.Show();
        }
    }

    private void _setupBeginnerGuide() {
        BubbleDialogController?.Clear();
        BubbleDialogController?.AddDialog("每天至少要上交家里1块钱 剩余的钱存起来当做学费 需要在九月开学之前攒够学费", () => {
            return Input.GetMouseButton(0);
        });
    }
}
