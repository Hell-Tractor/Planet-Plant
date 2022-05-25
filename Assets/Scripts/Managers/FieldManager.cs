using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FieldManager : MonoBehaviour {
    public BubbleDialogController _bubbleDialog;

    private void _setupBeginnerGuide() {
        _bubbleDialog.Clear();

        _bubbleDialog.AddDialog("鼠标左键点击右上角背包图标打开物品栏", () => {
            if (Input.GetMouseButtonDown(0)) {
                List<RaycastResult> hits = new List<RaycastResult>();
                EventSystem.current.RaycastAll(new PointerEventData(EventSystem.current) {
                    position = Input.mousePosition
                }, hits);
                
                if (hits.Count == 0)
                    return false;

                if (hits[0].gameObject.name == "bag") {
                    return true;
                }

                return false;
            }
            return false;
        });

        _bubbleDialog.AddDialog("左键点击物品栏中的种子", () => {
            if (Input.GetMouseButtonDown(0)) {
                List<RaycastResult> hits = new List<RaycastResult>();
                EventSystem.current.RaycastAll(new PointerEventData(EventSystem.current) {
                    position = Input.mousePosition
                }, hits);
                
                foreach (var hit in hits) {
                    if (hit.gameObject.CompareTag("InventorySlot")) {
                        Slot slot = hit.gameObject.GetComponent<Slot>();
                        if (slot?.GetItem()?.Id == "planetplant:seed") {
                            return true;
                        }
                    }
                }

                return false;
            }
            return false;
        });

        int count = Inventory._itemFollowMouse?.Count ?? 0;

        _bubbleDialog.AddDialog("右键点击要种下的地块（只有左边第一列可种，其他田地需要花钱开垦）", () => {
            if (Input.GetMouseButtonDown(1)) {
                var hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 0f, 1 << LayerMask.NameToLayer("Farmland"));
                return hit.collider?.GetComponent<Farmland>() != null;
            }
            return false;
        });

        _bubbleDialog.AddDialog("种植成功！随时间推移，种植的作物将会成熟，收割后可以出售，是非常重要的经济来源哦~", () => {
            return Input.GetMouseButtonDown(0);
        });
    }
    
    private void Start() {
        if (GlobalProperties.Instance.isFirstTimeToField) {
            GlobalProperties.Instance.isFirstTimeToField = false;

            // DialogManager.Instance.ShowDialog(19);
            this._setupBeginnerGuide();
            _bubbleDialog.Show();
        }

        TimeManager.Instance.Resume();
    }
}