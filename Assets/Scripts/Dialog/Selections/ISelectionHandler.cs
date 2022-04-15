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
            dialogManager.ShowDialog(9);
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