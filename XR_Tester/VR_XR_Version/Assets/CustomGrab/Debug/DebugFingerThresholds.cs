using UnityEngine;

/// <summary>
/// 
///  SET UP
///     Finger Tip
///         DebugFingerThreshold (with .cs)
///     Wrist
///         Decoy (further possible)
///         Sphere (debugGO)
/// 
/// </summary>


public class DebugFingerThresholds : MonoBehaviour
{

    [SerializeField] private Fingertip _fingertip;
    [SerializeField] private Transform _decoy;
    [SerializeField] private GameObject _debugGO;

    private Vector3 _closedFingerThreshold;

    private float _thresholdDist = 100f;


    private void Start()
    {
        _closedFingerThreshold = _fingertip.transform.localPosition;
    }

    private void Update()
    {

        float minDist = Vector3.Distance(_closedFingerThreshold, _decoy.position);
        float currentDist = Vector3.Distance(_fingertip.transform.position, _decoy.position);

        if (currentDist < _thresholdDist)
        {
            DebugLogManager.Instance.PrintLog("New threshold :   " + currentDist);

            _thresholdDist = currentDist;
            _debugGO.transform.position = _fingertip.transform.position;
        }
    }
}
