using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Hands;

public class test : MonoBehaviour
{

    [SerializeField] private XRHandSkeletonDriver hand;
    
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        string g = hand.jointTransformReferences[5].jointTransform.name;

        Debug.Log(g);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
