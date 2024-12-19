using UnityEngine;

public class Fingertip : MonoBehaviour
{
    public enum EHandSide { Left, Right, Invalid };
    public enum EFinger { Thumb, Index, Middle, Ring, Pinky, Invalid };
    
    //[SerializeField] private OVRHand.Hand _hand;
    //[SerializeField] private OVRSkeleton.BoneId _boneId;

    [SerializeField] private EHandSide _handSide;
    [SerializeField] private EFinger _finger;
    [SerializeField] private FixedJoint _fixedJoint;
    [SerializeField] private Transform _grabPoint;
    [SerializeField] private GameObject _boneCollider;

    //public OVRHand.Hand Hand { get { return _hand; } }
    //public OVRSkeleton.BoneId BoneId { get { return _boneId; } }

    public EHandSide HandSide { get { return _handSide; } }
    public EFinger Finger { get { return _finger; } }
    public FixedJoint FixedJoint { get { return _fixedJoint; } }
    public bool IsGrabbing { get { return _isGrabbing; } }
    public bool IsReleasing { get { return _isReleasing; } }


    //private float _previousDistance = 0f;
    private const float THRESHOLD = 10f; // default = 12
    //private const float MIN_DIST = 0.04f;
    //private const float MAX_DIST = 0.12f;
    //private const float RANGE = MAX_DIST - MIN_DIST;

    private GrabInteraction _interactor;

    private float _previousDistance = 0f;

    private bool _isGrabbing = false;
    private bool _isReleasing = false;


    //For velocity version
    private Vector3 _previousPosition;
    private Vector3 _velocity;
    private float _velocityMaxMagnitude = 0;



    private void FixedUpdate()
    {
        CalculateMovement();
    }

    private void CalculateMovement()
    {
        _isGrabbing = false;
        _isReleasing = false;
        
        // Calculate absolute distance between fingertip and center of hand (palm)
        float distance = Vector3.Distance(transform.position, _grabPoint.position);
        float frameDistance = Mathf.Abs(_previousDistance - distance);

        // Checks if finger is moving towards palm (aka closing)
        bool isClosing = false;
        if (distance < _previousDistance) { isClosing = true; }

        _previousDistance = distance;

        // Ratio is the % of movement in a frame for the full range potential (finger-palm)
        float range = Mathf.Max(distance, 0.01f);
        float ratio = frameDistance / range * 100f;

        if (isClosing)
        {
            _isGrabbing = ratio > _interactor.GrabbingThreshold;

            if (IsGrabbing)
                DebugLogManager.Instance.PrintLog(_handSide + " " + _finger + " is grabbing");
            //DebugLogManager.Instance.PrintLog(ratio.ToString());
        }
        else
        {
            _isReleasing = ratio > _interactor.ReleasingThreshold;

            if(IsReleasing)
                DebugLogManager.Instance.PrintLog(_handSide + " " + _finger + " is releasing");
        }


        //if (ratio > THRESHOLD)
        //{
        //    if (isClosing)
        //    {
        //        _isGrabbing = true;
        //        
        //        //DebugLogManager.Instance.PrintLog(_handSide + " " + _finger + " is grabbing");
        //        //DebugLogManager.Instance.PrintLog(ratio.ToString());
        //    }
        //    else
        //    {
        //        _isReleasing = true;
        //
        //        //DebugLogManager.Instance.PrintLog(OVRSkeleton.BoneLabelFromBoneId(Hand, BoneId) + " is releasing");
        //    }
        //}


    }

    // Not currently used but could end up being useful
    private void CalculateVelocity()
    {
        _velocity = (transform.position - _previousPosition) / Time.fixedDeltaTime;
        _previousPosition = transform.position;

        Vector3 grabPointDirection = _grabPoint.transform.position - transform.position;

        float dotProduct = Vector3.Dot(grabPointDirection.normalized, _velocity.normalized);

        //if (Hand == OVRHand.Hand.HandLeft && BoneId == OVRSkeleton.BoneId.Hand_IndexTip)
        if (HandSide == EHandSide.Left && Finger == EFinger.Index)
        {
            //DebugLogManager.Instance.PrintLog(dotProduct.ToString());
            //DebugLogManager.Instance.PrintLog(_velocity.magnitude.ToString());
        }

        if (_velocity.magnitude > _velocityMaxMagnitude)
        {
            _velocityMaxMagnitude = _velocity.magnitude;
        }
    }

    // Used only to evaluate what the thresholds should be
    //private void OnDisable()
    //{
    //    if (Hand == OVRHand.Hand.HandLeft && BoneId == OVRSkeleton.BoneId.Hand_IndexTip)
    //    {
    //        Debug.Log("Max velocity : " + _velocityMaxMagnitude);
    //    }
    //
    //}

    public void ToggleBoneCollider(bool value)
    {
        if(_boneCollider == null)
            return;

        _boneCollider.SetActive(value);
    }

    public void ConnectToInteractor(GrabInteraction interactor)
    {
        _interactor = interactor;
    }
}
