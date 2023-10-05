using Tobii.Gaming;
using UnityEngine;

public class ToggleButton : MonoBehaviour
{
    [SerializeField] bool mouseHoverEnabled = true;
    [SerializeField] protected FollowPoint followComponent;

    private bool focused = false;
    public enum FocusedMode 
    {
        ACTIVATING,
        DEACTIVATING,
        ACTIVE,
        DEACTIVE
    }

    private FocusedMode focusedMode = FocusedMode.DEACTIVE;
    float lastFocusChange = 0;
    float DEACTIVATE_TIME = 1.5f;
    float ACTIVATE_TIME = 1.5f;
    protected GameObject inheritingButton;
    public void init(float DEACTIVATE_TIME, float ACTIVATE_TIME, FocusedMode focusedMode, GameObject inheritingButton)
    {
        this.DEACTIVATE_TIME = DEACTIVATE_TIME;
        this.ACTIVATE_TIME = ACTIVATE_TIME;
        this.focusedMode = focusedMode;
        this.inheritingButton = inheritingButton;
    }

    void Start()
    {
        TobiiAPI.Start(null);
    }

    // Please override these methods.
    public virtual void OnActivate() {}
    public virtual void OnDeactivate() {}

    void Update()
    {
        bool newFocused = IsFocused(); 
        lastFocusChange += Time.deltaTime;

        bool recur = true;
        while (recur)
        {
            recur = false;
            switch (focusedMode)
            {
                case FocusedMode.DEACTIVATING:
                {
                    if (!focused)
                    {
                        lastFocusChange = 0;
                        focusedMode = FocusedMode.ACTIVE;
                        break;
                    }
                    if (lastFocusChange > DEACTIVATE_TIME)
                    {
                        focusedMode = FocusedMode.DEACTIVE;
                        followComponent.isWalking = false;
                        var renderer = GetComponent<Renderer>();
                        renderer.material.color = Color.red;
                        recur = true;
                    }
                }
                break;
                case FocusedMode.ACTIVATING:
                {
                    if (!focused)
                    {
                        lastFocusChange = 0;
                        focusedMode = FocusedMode.DEACTIVE;
                        break;
                    }
                    if (lastFocusChange > ACTIVATE_TIME)
                    {
                        focusedMode = FocusedMode.ACTIVE;
                        followComponent.isWalking = true;
                        var renderer = GetComponent<Renderer>();
                        renderer.material.color = Color.green;
                        recur = true;
                    }
                }
                break;
                case FocusedMode.DEACTIVE:
                {
                    if (focused)
                    {
                        lastFocusChange = 0;
                        focusedMode = FocusedMode.ACTIVATING;
                    }
                }
                break;
                case FocusedMode.ACTIVE:
                {
                    if (focused)
                    {
                        lastFocusChange = 0;
                        focusedMode = FocusedMode.DEACTIVATING;
                    }
                }
                break;
            }
        }

        focused = newFocused;
    }

    private bool IsFocused()
    {
        var focused = TobiiAPI.GetFocusedObject();
        bool tobiiFocus = focused != null && focused == gameObject;
        if (!mouseHoverEnabled) return tobiiFocus;
        if (tobiiFocus) return true;

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, float.PositiveInfinity, layerMask: LayerMask.GetMask("UI")))
        {
            print("euwww " + hit.point);
            if (hit.collider.gameObject == inheritingButton)
            print("eurkea");
            return hit.collider.gameObject == inheritingButton;
        }
        return false;
    }
}