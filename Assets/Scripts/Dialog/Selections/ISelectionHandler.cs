using UnityEngine;

public interface ISelectionHandler {
    public abstract void HandleSelection(int selection, DialogManager dialogManager);
}
