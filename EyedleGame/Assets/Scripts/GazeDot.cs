/*
 * Written by Rua M. Williams
 * This script controls a game object transform location by mouse location or gaze point location
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.Gaming;

public class GazeDot : MonoBehaviour
{
    public bool useMouse = true;    //set to false to use gaze
    public Vector3 gazeToWorldPosition;
    public float middleDistance = 50f;  //where to project the dot if there is no surface

    //public PrintGazePosition printGazePosition;

    private const int GAZE_RUNNING_AVG_SIZE = 50; // Collect an array of gaze points and avg them or create an array of the raycasts, etc. 
    //  Probably keep an array of gaze points, avg their location as you are forming the array
    private Vector3[] gazeRunningAvg = new Vector3[GAZE_RUNNING_AVG_SIZE];

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (!useMouse)
        {
            GazePoint gazePoint = TobiiAPI.GetGazePoint(); // Returns a position that you are looking at
            if(gazePoint.IsValid)
            {
                Vector2 gazeLoc = gazePoint.Viewport; // Possibly where you want to do an average of the gazepoint array viewport projection
                ray = Camera.main.ViewportPointToRay(new Vector3(gazeLoc.x, gazeLoc.y, 0f));
            }
        }

        if (Physics.Raycast(ray, out hit))
        {
            gazeToWorldPosition = hit.point;
        }
        else
        {
            gazeToWorldPosition = ray.GetPoint(middleDistance);
        }

        // this.transform.position = gazeToWorldPosition; // The thing to change to a moving average
       
        // Presumably if we reset gazeRunningAvg for whatever reason, repopulate it by assuming there is a couple of nulls in a row at the front
        if (gazeRunningAvg[0] == null)
        {
            for (int i = 0; i < gazeRunningAvg.Length; i++)
            {
                if (gazeRunningAvg[i] == null)
                {
                    gazeRunningAvg[i] = gazeToWorldPosition;
                }
            }
        }

        // Shift the running average to the front to make way for the new data
        for (int i = 0; i < gazeRunningAvg.Length - 1; i++)
            gazeRunningAvg[i] = gazeRunningAvg[i + 1];
        gazeRunningAvg[gazeRunningAvg.Length - 1] = gazeToWorldPosition;
        
        Vector3 sum = new Vector3(0, 0, 0);
        foreach (var gaze in gazeRunningAvg)
        {
            sum += gaze;
        }

        this.transform.position = sum / (float) gazeRunningAvg.Length;
    }
}
