using UnityEngine;
using Tobii.Gaming;

public class WalkToggle : MonoBehaviour
{
    public FollowPoint followComponent;
    
    bool focused = false;
    enum FocusedMode
    {
        FOCUSING,
        STOPPING,
        FOCUSED,
        STOPPED
    }
    FocusedMode focusedMode = FocusedMode.STOPPED; 
    float lastFocusChange = 0; 
    const float STOP_TIME = 1.5f;
    const float FOCUS_TIME = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        TobiiAPI.Start(null);
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            var pauseMenuPlane = GameObject.Find("PauseMenu"); 
            var pauseMenu = pauseMenuPlane.GetComponent<PauseMenu>();
            pauseMenu.Toggle();
        }
        */

        // Toggle the walktoggle button by looking for long enough in a row
        bool newFocused = IsFocused();
        lastFocusChange += Time.deltaTime;

        bool recur = true;

        while (recur)
        {
            recur = false;
            switch (focusedMode)
            {
                case FocusedMode.STOPPING:
                {
                    if (!focused)
                    {
                        // Debug.Log("stopped but now not");
                        lastFocusChange = 0;
                        focusedMode = FocusedMode.FOCUSED;
                        break;
                    }
                    if (lastFocusChange > STOP_TIME)
                    {
                        focusedMode = FocusedMode.STOPPED;
                        followComponent.isWalking = false;
                        Renderer renderer = GetComponent<Renderer>();
                        renderer.material.color = Color.red; 
                        recur = true;
                    }
                    break;
                }
                case FocusedMode.FOCUSING:
                {
                    if (!focused)
                    {
                        // Debug.Log("focusing but now not");
                        lastFocusChange = 0;
                        focusedMode = FocusedMode.STOPPED;
                        break;
                    }
                    if (lastFocusChange > FOCUS_TIME)
                    {
                        focusedMode = FocusedMode.FOCUSED;
                        followComponent.isWalking = true;
                        Renderer renderer = GetComponent<Renderer>();
                        renderer.material.color = Color.green;
                        recur = true;
                    }
                    break;
                }
                case FocusedMode.STOPPED:
                {
                    if (focused)
                    {
                        lastFocusChange = 0;
                        focusedMode = FocusedMode.FOCUSING;
                    }
                    break;
                }
                case FocusedMode.FOCUSED:
                {
                    if (focused)
                    {
                        lastFocusChange = 0;
                        focusedMode = FocusedMode.STOPPING;
                    }
                    break;
                }
            }
        }

        focused = newFocused;
    }

    bool IsFocused()
    {
        GameObject focusedObject = TobiiAPI.GetFocusedObject();
        return focusedObject != null && focusedObject == gameObject; 
    }
}
