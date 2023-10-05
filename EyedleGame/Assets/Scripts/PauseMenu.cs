using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject gazeLocation;

    [SerializeField] private FollowPoint followComponent;

    // UI -> Unlit -> Transparent is how you get transparent materials
    private void toggleRender() 
    {
        foreach (var comp in GetComponentsInChildren<MeshRenderer>())
        {
            if (comp == GetComponent<MeshRenderer>()) continue;
            comp.enabled = !comp.enabled;
        }
    }

    public void Toggle()
    {
        bool unpaused = GetComponent<MeshRenderer>().enabled;
        GetComponent<MeshRenderer>().enabled = !unpaused;
        print("unpaused " + unpaused);
        gazeLocation.GetComponent<Renderer>().enabled = !unpaused;
        Camera.main.GetComponent<FollowPoint>().enabled = !unpaused;

        toggleRender();
        followComponent.enabled = !unpaused;

        if (!unpaused) saveChanges();
    }

    private void saveChanges()
    {
        foreach (var slider in GetComponentsInChildren<Slider>())
        {
            slider.SetPMove(); 
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        toggleRender();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
