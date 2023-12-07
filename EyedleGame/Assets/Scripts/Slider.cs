using UnityEngine;

public abstract class Slider : ToggleButton
{
    [SerializeField] protected float lowerPercent, upperPercent;
    [SerializeField] protected GameObject player;
    [SerializeField] protected GameObject sliderObject;
    protected float ogSens;
    protected bool myFocused = false;
    protected FollowPoint getFollowPoint() => player.GetComponent<FollowPoint>();

    public void init(float ogSens)
    {
        init(1.5f, 1.5f, FocusedMode.DEACTIVE);
        this.ogSens = ogSens;
    }

    public override GameObject GetGameObject()
    {
        return gameObject;
    }

    public override void OnActivate() => myFocused = true;
    public override void OnDeactivate() => myFocused = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    public void Update()
    {
        UpdateFocus();
        if (!myFocused) return;

        //if focused
        //if gaze point
        if (Camera.main.WorldToScreenPoint(followComponent.followThis.position).x > Camera.main.WorldToScreenPoint(sliderObject.transform.position).x)
        {
            Debug.Log("Eye is greater than bar");
            //increment the slider controlled value
            sliderObject.transform.localPosition = new Vector3(Mathf.Min(sliderObject.transform.localPosition.x + 0.001f, 0.5f), sliderObject.transform.localPosition.y, sliderObject.transform.localPosition.z);
        }

        if (Camera.main.WorldToScreenPoint(followComponent.followThis.position).x < Camera.main.WorldToScreenPoint(sliderObject.transform.position).x)
        {
            Debug.Log("Eye is less than bar");
            //increment the slider controlled value
            sliderObject.transform.localPosition = new Vector3(Mathf.Max(sliderObject.transform.localPosition.x - 0.001f, -0.5f), sliderObject.transform.localPosition.y, sliderObject.transform.localPosition.z);
        }
    }

    public abstract void SetSliderValue();
}