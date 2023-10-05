using UnityEngine;

public class Slider : MonoBehaviour
{
    [SerializeField] private float lowerPercent, upperPercent;
    [SerializeField] private GameObject pMovement;
    [SerializeField] private GameObject sliderObject;
    private float ogSens;

    private FollowPoint getPMove() => pMovement.GetComponent<FollowPoint>();
    private float ogSliderPosX
    {
        get => sliderObject.transform.position.x;
    }

    // Start is called before the first frame update
    void Start()
    {
        ogSens = getPMove().turnSpeed;
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

    public void SetPMove()
    {
        float normalized = 0.5f + sliderObject.transform.localPosition.x; // now between 0 and 1 instead of -0.5 and 0.5

        float sensPercent = (upperPercent - lowerPercent) * normalized + lowerPercent;
        getPMove().turnSpeed = ogSens * sensPercent;
        print("slider " + getPMove().turnSpeed);
    }
}
