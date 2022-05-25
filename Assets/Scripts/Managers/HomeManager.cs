using UnityEngine;

public class HomeManager : MonoBehaviour {
    public BubbleDialogController BubbleDialogController = null;
    private bool _isAfterMarketDialogShown = false;
    private void Start() {
        bool bubbleDialogExists = false;
        
        if (GlobalProperties.Instance.isFirstTimeGoHome) {
            GlobalProperties.Instance.isFirstTimeGoHome = false;

            _setupBeginnerGuide();
            BubbleDialogController?.Show();
            bubbleDialogExists = true;
        }

        if (!_isAfterMarketDialogShown && !bubbleDialogExists) {
            DialogManager.Instance.ShowDialog(13);
            _isAfterMarketDialogShown = true;
        }
    }

    private void _setupBeginnerGuide() {
        BubbleDialogController?.Clear();
        BubbleDialogController?.AddDialog("每天至少要上交家里1块钱 剩余的钱存起来当做学费 需要在九月开学之前攒够学费", () => {
            if (Input.GetMouseButton(0)) {
                if (GlobalProperties.Instance.isFristTimeToMarket == false) {
                    DialogManager.Instance.ShowDialog(13);
                    _isAfterMarketDialogShown = true;
                } else {
                    DialogManager.Instance.ShowDialog(4);
                }
                return true;
            }
            return false;
        });
    }
}
