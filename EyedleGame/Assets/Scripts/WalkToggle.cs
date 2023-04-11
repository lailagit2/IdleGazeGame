using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.Gaming;

public class WalkToggle : MonoBehaviour
{
    public FollowPoint followComponent;

    // Start is called before the first frame update
    void Start()
    {
        TobiiAPI.Start(null);
    }

    // Update is called once per frame
    void Update()
    {
        GameObject focusedObject = TobiiAPI.GetFocusedObject();
        if (null != focusedObject)
        {
            print("The focused game object is: " + focusedObject.name + " (ID: " + focusedObject.GetInstanceID() + ")");
            followComponent.isWalking = !followComponent.isWalking;
        }
    }
}
