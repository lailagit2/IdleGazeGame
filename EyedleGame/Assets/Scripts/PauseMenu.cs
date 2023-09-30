using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] Camera followingPointCamera;
    [SerializeField] GameObject gazeLocation;
    [SerializeField] private List<GameObject> knobs;

    public FollowPoint followComponent;

    private void onVisible()
    {
    }
    // UI -> Unlit -> Transparent is how you get transparent materials

    public void Toggle()
    {
        bool unpaused = GetComponent<Renderer>().enabled;
        gazeLocation.GetComponent<MeshRenderer>().enabled = GetComponent<Renderer>().enabled;
        followingPointCamera.GetComponent<FollowPoint>().enabled = unpaused;

        List<GameObject> knobsAndMenu = knobs;
        knobsAndMenu.Add(gameObject);
        foreach (var obj in knobs)
        {
            var renderer = obj.GetComponent<Renderer>();
            renderer.enabled = !renderer.enabled;
            if (obj == gameObject) 
            {
                followComponent.enabled = !renderer.enabled;
                if (renderer.enabled) onVisible();
            }
        }
        knobsAndMenu.Remove(gameObject);
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
