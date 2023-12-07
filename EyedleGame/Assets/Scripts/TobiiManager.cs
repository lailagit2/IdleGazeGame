using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.Gaming;

public class TobiiManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        TobiiAPI.Start(null);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
