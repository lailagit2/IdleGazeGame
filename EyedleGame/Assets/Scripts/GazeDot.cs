/*
 * Written by Rua M. Williams
 * This script controls a game object transform location by mouse location or gaze point location
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Tobii.Gaming;

public class GazeDot : MonoBehaviour
{
    public bool useMouse = true;    //set to false to use gaze
    public Vector3 gazeToWorldPosition;
    public GameObject gazeSample;

    //public PrintGazePosition printGazePosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        Ray ray;
        
        if(!useMouse)
        {
            // put the gaze screen location here somehow
            Vector2 gazeLoc = new Vector2(gazeSample.transform.position.x, gazeSample.transform.position.y);
            ray = Camera.main.ScreenPointToRay(gazeLoc);
        }
        else
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            gazeToWorldPosition = hit.point;
        }
        else
        {
            gazeToWorldPosition = ray.GetPoint(15f);
        }

        this.transform.position = gazeToWorldPosition;
        
    }
}
