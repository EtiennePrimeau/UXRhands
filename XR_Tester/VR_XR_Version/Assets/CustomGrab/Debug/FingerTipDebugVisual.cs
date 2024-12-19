using UnityEngine;

public class FingerTipDebugVisual : MonoBehaviour
{
    
    public static FingerTipDebugVisual Instance;

    [SerializeField] private GameObject _indexCube;
    [SerializeField] private GameObject _middleCube;
    [SerializeField] private GameObject _ringCube;
    [SerializeField] private GameObject _pinkyCube;
    [SerializeField] private GameObject _thumbCube;



    private void Awake()
    {
        Instance = this;
    }


    public void ChangeDebugVisual(Fingertip.EFinger finger, bool value)
    {
        if (finger == Fingertip.EFinger.Index)
        {
            Material mat = _indexCube.GetComponent<MeshRenderer>().material;
            if (value)
            {
                mat.color = Color.green;
            }
            else
            {
                mat.color = Color.red;  
            }
        }

        if (finger == Fingertip.EFinger.Middle)
        {
            Material mat = _middleCube.GetComponent<MeshRenderer>().material;
            if (value)
            {
                mat.color = Color.green;
            }
            else
            {
                mat.color = Color.red;  
            }
        }

        if (finger == Fingertip.EFinger.Ring)
        {
            Material mat = _ringCube.GetComponent<MeshRenderer>().material;
            if (value)
            {
                mat.color = Color.green;
            }
            else
            {
                mat.color = Color.red;  
            }
        }

        if (finger == Fingertip.EFinger.Pinky)
        {
            Material mat = _pinkyCube.GetComponent<MeshRenderer>().material;
            if (value)
            {
                mat.color = Color.green;
            }
            else
            {
                mat.color = Color.red;  
            }
        }

        if (finger == Fingertip.EFinger.Thumb)
        {
            Material mat = _thumbCube.GetComponent<MeshRenderer>().material;
            if (value)
            {
                mat.color = Color.green;
            }
            else
            {
                mat.color = Color.red;  
            }
        }
    }
}
