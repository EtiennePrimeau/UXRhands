using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventDebugger : MonoBehaviour
{

    [SerializeField] private bool _isEnabled = true;

    public void PrintLog(string str)
    {
        if (!_isEnabled)
            return;

        Debug.Log(str); 
    }

}
