public class TurnSpeedSlider : Slider 
{
    void Start() 
    {
        init(getFollowPoint().turnSpeed);
    }

    public override void SetSliderValue() 
    {
        float normalized = 0.5f + sliderObject.transform.localPosition.x; // now between 0 and 1 instead of -0.5 and 0.5

        float sensPercent = (upperPercent - lowerPercent) * normalized + lowerPercent;
        getFollowPoint().turnSpeed = ogSens * sensPercent;
        print("slider " + getFollowPoint().turnSpeed);
    }
}

/*
public class TurnSpeedSlider : ToggleButton
{
    [SerializeField] private float lowerPercent, upperPercent;
    [SerializeField] private GameObject pMovement;
    [SerializeField] private GameObject sliderObject;
    private float ogSens;
    private bool myFocused = false;

    private FollowPoint getPMove() => pMovement.GetComponent<FollowPoint>();

    // Start is called before the first frame update
    void Start()
    {
        init(1.5f, 1.5f, FocusedMode.DEACTIVE);
        ogSens = getPMove().turnSpeed;
    }

    public override GameObject GetGameObject()
    {
        print("!! we will return " + gameObject);
        return gameObject;
    }
    public override void OnActivate()
    {
        print("!! activate");
        myFocused = true;
    }
    public override void OnDeactivate()
    {
        print("!! deactivate");
        myFocused = false; 
    }

    // Update is called once per frame
    void Update()
    {
        UpdateFocus();
        if (!myFocused || !GetComponent<Renderer>().enabled)
            return;

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

    public void SetSliderValue()
    {
        float normalized = 0.5f + sliderObject.transform.localPosition.x; // now between 0 and 1 instead of -0.5 and 0.5

        float sensPercent = (upperPercent - lowerPercent) * normalized + lowerPercent;
        getPMove().turnSpeed = ogSens * sensPercent;
        print("slider " + getPMove().turnSpeed);
    }
}
*/