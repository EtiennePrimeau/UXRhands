using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GrabbableDebug : MonoBehaviour
{
    #region Singleton
    private static GrabbableDebug instance;
    public static GrabbableDebug Instance
    {
        get
        {
            if (instance != null) return instance;

            Debug.LogError("GrabbableDebug is null");
            return null;
        }
    }
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        instance = this;
    }
    #endregion

    private enum EFingers
    { Thumb, Index, Middle, Ring, Pinky }

    [SerializeField] private GrabbableObject _grabbableObject;

    [SerializeField] private Image[] _leftHand = new Image[5];
    [SerializeField] private Image[] _rightHand = new Image[5];

    [SerializeField] private TextMeshProUGUI _preferredHandTMP;

    private List<Fingertip> _leftHandFingertips = new List<Fingertip>();
    private List<Fingertip> _rightHandFingertips = new List<Fingertip>();

    private GrabbableObject.EHandSide _preferredHand;




    private void Start()
    {
        if (_grabbableObject == null)
            return;

        LinkElements();
    }

    private void Update()
    {
        if (_grabbableObject == null)
            return;
        
        UpdateImages(_leftHand, _leftHandFingertips);
        UpdateImages(_rightHand, _rightHandFingertips);

        _preferredHandTMP.text = "Preferred Hand : " + _grabbableObject.PreferredHand.ToString();
    }

    public void AssignGrabbableObject(GrabbableObject obj)
    {
        _grabbableObject = obj;
        LinkElements();
    }

    private void LinkElements()
    {
        _leftHandFingertips = _grabbableObject.LeftFingertips;
        _rightHandFingertips = _grabbableObject.RightFingertips;

        _preferredHand = _grabbableObject.PreferredHand;
    }

    private void UpdateImages(Image[] hand, List<Fingertip> fingertips)
    {
        foreach (var image in hand)
        {
            image.color = Color.red;
        }
        
        foreach (var fingertip in fingertips)
        {
            switch (fingertip.Finger)
            {
                case Fingertip.EFinger.Thumb:
                    hand[(int)EFingers.Thumb].color = Color.green;
                    break;
                case Fingertip.EFinger.Index:
                    hand[(int)EFingers.Index].color = Color.green;
                    break;
                case Fingertip.EFinger.Middle:
                    hand[(int)EFingers.Middle].color = Color.green;
                    break;
                case Fingertip.EFinger.Ring:
                    hand[(int)EFingers.Ring].color = Color.green;
                    break;
                case Fingertip.EFinger.Pinky:
                    hand[(int)EFingers.Pinky].color = Color.green;
                    break;
                default:
                    break;
            }
        }
    }

}
