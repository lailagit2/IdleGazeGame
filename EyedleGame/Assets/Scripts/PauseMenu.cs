using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public FollowPoint followComponent;

    public void Toggle()
    {
        var renderer = GetComponent<Renderer>();
        renderer.enabled = !renderer.enabled;
        followComponent.enabled = !renderer.enabled;
    }

    // Start is called before the first frame update
    void Start()
    {
        var renderer = GetComponent<Renderer>();
        renderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
