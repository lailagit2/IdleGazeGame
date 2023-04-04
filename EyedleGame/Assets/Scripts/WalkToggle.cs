using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.Gaming;

public class WalkToggle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnEnable()
    {
        print(this.GetComponent<GazeAware>().HasGazeFocus ? "has gaze focus" : "no gaze focus");
    }

    // Update is called once per frame
    void Update()
    {
        GameObject focused = TobiiAPI.GetFocusedObject();
        print(focused != null ? focused.name : "nothing is focused object");

        print(this.GetComponent<GazeAware>().HasGazeFocus ? "has gaze focus" : "no gaze focus");
    }
}
