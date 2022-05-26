// UNUSED
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class for generating Bezier curves.
/// </summary>
public class CurveMovement : MonoBehaviour
{
    /// <summary>
    /// Way points of the Bezier curve.
    /// </summary>
    public Vector3[] Points = new Vector3[3];
    /// <summary>
    /// The objects under control
    /// </summary>
    public List<GameObject> ObjectList = new List<GameObject>();
    /// <summary>
    /// Interpolation point count
    /// </summary>
    public int fittingPointsCount = 100;
    /// <summary>
    /// Time of moving
    /// </summary>
    public float Duration = 1f;
    public bool AutoStart = false;
    public bool Pause = false;

    private List<Vector3> _curvePoints = new List<Vector3>();

    private void Start() {
        if (AutoStart) {
            StartCoroutine(Move());
        }
    }

    /// <summary>
    /// Move all the objects in list along the curve.(Coroutine)
    /// </summary>
    public IEnumerator Move() {
        if (ObjectList.Count != 0) {
            // if curve not generated, generate it
            if (_curvePoints.Count == 0) {
                GeneratePath();
            }
            if (_curvePoints.Count != 0) {
                // move all the objects
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

    /// <summary>
    /// Generate the Bezier curve.
    /// </summary>
    protected void GeneratePath() {
        _curvePoints.Clear();
        for (int i = 0; i < fittingPointsCount; i++) {
            float t = i / (float)fittingPointsCount;
            _curvePoints.Add(GetPoint(t));
        }
    }

    /// <summary>
    /// Get the point with parameter t.
    /// </summary>
    /// <param name="t">Range [0, 1]</param>
    /// <returns>The interpolation point</returns>
    protected Vector3 GetPoint(float t) {
        Vector3 temp = (1 - t) * (1 - t) * Points[0] + 2 * (1 - t) * t * Points[1] + t * t * Points[2];
        return temp;
    }
    
    /// <summary>
    /// Draw the curve in the editor.
    /// </summary>
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
