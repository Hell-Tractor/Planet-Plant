using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveMovement : MonoBehaviour
{
    public Vector3[] Points = new Vector3[3];
    public List<GameObject> ObjectList = new List<GameObject>();
    public int fittingPointsCount = 100;
    public float Duration = 1f;
    public bool AutoStart = false;
    public bool Pause = false;

    private List<Vector3> _curvePoints = new List<Vector3>();

    private void Start() {
        if (AutoStart) {
            StartCoroutine(Move());
        }
    }

    public IEnumerator Move() {
        if (ObjectList.Count != 0) {
            if (_curvePoints.Count == 0) {
                GeneratePath();
            }
            if (_curvePoints.Count != 0) {
                for (int i = 0; i < _curvePoints.Count; ++i) {
                    if (Pause) {
                        yield return new WaitUntil(() => !Pause);
                    }
                    for (int j = 0; j < ObjectList.Count; ++j) {
                        ObjectList[j].GetComponent<RectTransform>().position = _curvePoints[i];
                        // ObjectList[j].transform.localPosition = _curvePoints[i];
                    }
                    yield return new WaitForSeconds(Duration / fittingPointsCount);
                }
            }
        }
    }

    protected void GeneratePath() {
        _curvePoints.Clear();
        for (int i = 0; i < fittingPointsCount; i++) {
            float t = i / (float)fittingPointsCount;
            _curvePoints.Add(GetPoint(t));
        }
    }

    protected Vector3 GetPoint(float t) {
        Vector3 temp = (1 - t) * (1 - t) * Points[0] + 2 * (1 - t) * t * Points[1] + t * t * Points[2];
        return temp;
    }
    
    private void OnDrawGizmos() {
        if (Points.Length != 3) {
            return;
        }
        GeneratePath();
        Gizmos.color = Color.red;
        for (int i = 1; i < _curvePoints.Count; ++i) {
            Gizmos.DrawLine(_curvePoints[i - 1], _curvePoints[i]);
        }
    }
}
