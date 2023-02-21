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

    //public PrintGazePosition printGazePosition;

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
            GazePoint gazePoint = TobiiAPI.GetGazePoint();
            if(gazePoint.IsValid)
            {
                Vector2 gazeLoc = gazePoint.Viewport;
                ray = Camera.main.ViewportPointToRay(new Vector3(gazeLoc.x, gazeLoc.y, 0f));
            }
        }

        if (Physics.Raycast(ray, out hit))
        {
            gazeToWorldPosition = hit.point;
        }
        else
        {
            gazeToWorldPosition = ray.GetPoint(50f);
        }

        this.transform.position = gazeToWorldPosition;
        
    }
}
