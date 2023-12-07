using UnityEngine;
using Tobii.Gaming;

public class WalkToggleButton : ToggleButton
{
    // Start is called before the first frame update
    void Start()
    {
        init(1.5f, 1.5f, FocusedMode.DEACTIVE);
    }

    void Update() => UpdateFocus();

    public override GameObject GetGameObject() => gameObject;
    public override void OnActivate() => followComponent.isWalking = true;
    public override void OnDeactivate() => followComponent.isWalking = false;
}
