using UnityEngine;

/// <summary>
/// This should be on a child game object of the grabbableObject
/// TODO : I haven't checked for a way to incorporate it to the grabbableObject 
/// </summary>
public class GrabbableReleaseTrigger : MonoBehaviour
{
    private GrabbableObject _grabbableObject;

    private void Start()
    {
        _grabbableObject = GetComponentInParent<GrabbableObject>();
        if (_grabbableObject == null)
            Debug.LogError("grabbable not found");
    }


    private void OnTriggerExit(Collider other)
    {
        Fingertip fingertip = other.gameObject.GetComponent<Fingertip>();
        if (fingertip == null)
            return;

        bool isLeftHand = (fingertip.HandSide == Fingertip.EHandSide.Left);

        _grabbableObject.RemoveFingertipFromList(isLeftHand, fingertip);
    }

}
