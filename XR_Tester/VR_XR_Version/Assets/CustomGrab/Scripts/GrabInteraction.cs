using UnityEngine;

/// <summary>
/// This is the manager of the grabbable interactions from one hand
/// </summary>
public class GrabInteraction : MonoBehaviour
{
    [SerializeField] private GrabbableObject.EHandSide _handSide = GrabbableObject.EHandSide.None;
    [SerializeField] private Fingertip[] _fingertips = new Fingertip[5];
    [SerializeField] private HandHitBox _handHitBox;
    [SerializeField] private FixedJoint _fixedJoint;
    [SerializeField] private GameObject _palmCollider;
    [SerializeField] private float _colliderReactivationDelay = 0.5f;

    [Tooltip("represents the % of movement of a fingertip in a single frame")]
    [Range(0f, 100f)]
    [SerializeField] private float _grabbingThreshold, _releasingThreshold = 10f;

    public float GrabbingThreshold { get { return _grabbingThreshold; } }
    public float ReleasingThreshold { get { return _releasingThreshold; } }

    private bool _isHolding = false;
    private bool _isReactivatingColliders = false;
    private float _colliderTimer = 0f;

    private GrabbableObject _closestGrabbable;
    private GrabbableObject _currentlyHeldGrabbable;


    private void Start()
    {
        foreach (var fingertip in _fingertips)
        {
            fingertip.ConnectToInteractor(this);
        }
    }

    private void Update()
    {
        HandleColliderTimer();
    }

    private void FixedUpdate()
    {
        if (_isHolding)
        {
            CheckForRelease();
        }
        else
        {
            _closestGrabbable = _handHitBox.ClosestGrabbable;
            CheckForGrab();
        }
    }

    private void CheckForRelease()
    {
        foreach (var fingertip in _currentlyHeldGrabbable.AttachedFingertips)
        {
            if (fingertip.IsReleasing)
            {
                _closestGrabbable.Detach(fingertip);

                _currentlyHeldGrabbable = null;
                _isHolding = false;
                _isReactivatingColliders = true;
                return;
            }
        }
    }

    private void CheckForGrab()
    {
        if (_closestGrabbable == null)
            return;

        if (!_closestGrabbable.CanBeGrabbed(_handSide))
            return;

        //DebugLogManager.Instance.PrintLog(gameObject.name + " is calling Attach");
        if (_closestGrabbable.TryAttach(_fixedJoint, _handSide))
        {
            _isHolding = true;
            ToggleBoneColliders(false);
            _currentlyHeldGrabbable = _closestGrabbable;
        }
        //else
        //    DebugLogManager.Instance.PrintLog("Failed to Attach");
    }

    private void HandleColliderTimer()
    {
        if (_isReactivatingColliders)
        {
            _colliderTimer += Time.deltaTime;
            if (_colliderTimer > _colliderReactivationDelay)
            {
                ToggleBoneColliders(true);
                _colliderTimer = 0f;
                _isReactivatingColliders = false;
            }
        }
    }

    private void ToggleBoneColliders(bool value)
    {
        //DebugLogManager.Instance.PrintLog("Toggling Bone Colliders : " + value);

        foreach (var fingertip in _fingertips)
        {
            fingertip.ToggleBoneCollider(value);
        }
        _palmCollider.SetActive(value);
    }

}
