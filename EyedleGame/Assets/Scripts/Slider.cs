using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slider : MonoBehaviour
{
    public GameObject sliderObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if focused
        //if gaze point
        if (Input.mousePosition.x > Camera.main.WorldToScreenPoint(sliderObject.transform.position).x)
        {
            //increment the slider controlled value
            sliderObject.transform.localPosition = new Vector3(Mathf.Min(sliderObject.transform.localPosition.x + 0.01f, 0.5f), sliderObject.transform.localPosition.y, sliderObject.transform.localPosition.z);
        }

        if (Input.mousePosition.x < Camera.main.WorldToScreenPoint(sliderObject.transform.position).x)
        {
            //increment the slider controlled value
            sliderObject.transform.localPosition = new Vector3(Mathf.Max(sliderObject.transform.localPosition.x - 0.01f, -0.5f), sliderObject.transform.localPosition.y, sliderObject.transform.localPosition.z);
        }
    }
}
