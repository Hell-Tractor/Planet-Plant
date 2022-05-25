using UnityEngine;

public class MarketManager : MonoBehaviour {
    public RealMoneyShowManager RealMoneyShower = null;
    public MoneyScreeningManager MoneyScreeningManager = null;
    public GameObject Buyer = null;
    public ShopInventory Shop = null;
    public BubbleDialogController BubbleDialogController = null;
    private DialogManager _dialogManager;
    private int[] _jumpToRealMoneyShowDialogIDList = new int[] {
        39, 41, 61
    };
    private void Start() {
        Random.InitState(System.DateTime.Now.Millisecond);
        
        _dialogManager = DialogManager.Instance;
        Buyer?.SetActive(false);
        if (_dialogManager != null) {
            _dialogManager.AfterDialogChange += this._onMarketDialogChange;

            if (GlobalProperties.Instance.isFristTimeToMarket) {
                GlobalProperties.Instance.isFristTimeToMarket = false;

                _dialogManager.ShowDialog(7);
                _dialogManager.OnDialogEnd += (int dialogid) => {
                    if (dialogid == 7) {
                        _setupBeginnerGuide();
                        BubbleDialogController?.Show(0.3f);
                    }
                };
            }
        }

        // _dialogManager.ShowDialog(8);
    }

    private void _setupBeginnerGuide() {
        BubbleDialogController?.Clear();
        BubbleDialogController?.AddDialog("右边是情绪条，你现在的情绪值有点低，可以通过购买喜欢的物品来提高情绪值。情绪值若太低，有一定几率做出不理智事件哦！", () => {
            if (Input.GetMouseButton(0)) {
                Shop.Show(() => _dialogManager.ShowDialog(8));
                return true;
            }
            return false;
        });
    }

    private void OnDestory() {
        if (_dialogManager != null) {
            _dialogManager.AfterDialogChange -= this._onMarketDialogChange;
        }
    }

    private void _onMarketDialogChange(Data.DialogData dialogData) {
        if (dialogData.ID == 34) {
            Buyer?.SetActive(true);
            return;
        }
        
        // 跳转到真币展示
        foreach (var id in _jumpToRealMoneyShowDialogIDList) {
            if (dialogData.ID == id) {
                _dialogManager.ShowDialog(15);
                return;
            }
        }
        
        // 真币展示
        if (dialogData.ID == 63) {
            _dialogManager.UI.PreventKeyEventProcessing = true;
            RealMoneyShower?.gameObject.SetActive(true);
            RealMoneyShower?.Show(() => {
                _dialogManager.ShowNext();
                _dialogManager.UI.PreventKeyEventProcessing = false;
            });
            return;
        }

        // 假币甄别
        if (dialogData.ID == 64) {
            MoneyScreeningManager?.gameObject.SetActive(true);
            MoneyScreeningManager?.ShowMoney();
            return;
        }
    }
}
