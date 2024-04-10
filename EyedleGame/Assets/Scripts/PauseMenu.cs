using UnityEngine;

public class PauseMenu : ToggleButton
{
    [SerializeField] private GameObject gazeLocation;
    [SerializeField] private GameObject uiCanvas;
    [SerializeField] private GameObject walkUI;
    [SerializeField] private GameObject UIWalkToggle;


    private bool paused = false;

    void Start()
    {
        setRender(false);
        init(0.5f, 0.5f, FocusedMode.DEACTIVE);
    }

    public override GameObject GetGameObject()
    {
        return gameObject;
    }

    public override void OnActivate() => Toggle();
    public override void OnDeactivate() => Toggle();

    // UI -> Unlit -> Transparent is how you get transparent materials
    private void setRender(bool val) 
    {
        uiCanvas.SetActive(val);

        foreach (var comp in this.transform.parent.GetComponentsInChildren<MeshRenderer>())
        {
            if (comp == GetComponent<MeshRenderer>()) continue;
            comp.enabled = val;
        }
    }

    public void Toggle()
    {
        /*     bool unpaused = !paused;
             paused = unpaused;
             print("unpaused " + unpaused);*/

        walkUI.GetComponent<Renderer>().enabled = !paused;
        Debug.Log(UIWalkToggle.name, UIWalkToggle);

        UIWalkToggle.SetActive(paused);


        //gazeLocation.GetComponent<Renderer>().enabled = !unpaused;
        //Camera.main.GetComponent<FollowPoint>().enabled = !unpaused;

        setRender(!paused);
        followComponent.isLooking = paused;

        

        if (paused) saveChanges();
    }

    private void saveChanges()
    {
        foreach (var slider in GetComponentsInChildren<Slider>())
        {
            slider.SetSliderValue(); 
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateFocus();
        if (Input.GetKeyDown(KeyCode.Escape)) 
            Toggle();
    }
}
