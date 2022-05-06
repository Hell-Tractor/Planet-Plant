using System.Threading.Tasks;
using UnityEngine;

public interface ISelectionHandler {
    public virtual void HandleSelection(int selection, DialogManager dialogManager) {
        dialogManager.ShowNext();
    }
}

public class DefaultSelectionHandler : ISelectionHandler {
}

public class SelectionHandler2 : ISelectionHandler {
    public void HandleSelection(int selection, DialogManager dialogManager) {
        if (selection == 0) {
            RandomController _random = dialogManager.UI.RandomController;
            _random.gameObject.SetActive(true);
            
            dialogManager.UI.PreventKeyEventProcessing = true;
            _random.Roll((result) => {
                if (result + GlobalProperties.Instance.PlayerIntelligence > 10) {
                    dialogManager.ShowDialog(14);
                } else {
                    dialogManager.ShowDialog(9);
                }
                _random.gameObject.SetActive(false);
                dialogManager.UI.PreventKeyEventProcessing = false;
            });
        } else if (selection == 1) {
            dialogManager.ShowDialog(10);
        }
    }
}

public class SelectionHandler3 : ISelectionHandler {
    public void HandleSelection(int selection, DialogManager dialogManager) {
        if (selection == 0) {
            dialogManager.ShowDialog(11);
        } else if (selection == 1) {
            dialogManager.ShowDialog(12);
        }
    }
}

public class SelectionHandler4 : ISelectionHandler {
    public void HandleSelection(int selection, DialogManager dialogManager) {
        // TODO 对话序号需要修正
        var instance = MoneyScreeningManager.Instance;
        if (instance == null) {
            dialogManager.ShowNext();
            return;
        }
        if (instance.IsRealMoney) {
            dialogManager.ShowDialog(16);
        } else {
            if (instance.IsSomeFakePointFinded()) {
                dialogManager.ShowDialog(18);
            } else {
                dialogManager.ShowDialog(17);
            }
        }

        instance.Hide();
    }
}