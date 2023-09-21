using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private List<GameObject> knobs;

    public FollowPoint followComponent;

    private void onVisible()
    {
    }

    public void Toggle()
    {
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
