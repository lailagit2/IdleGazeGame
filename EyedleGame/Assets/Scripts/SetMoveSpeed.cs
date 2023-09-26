using UnityEngine;
using UnityEngine.UIElements;

public class SetMoveSpeed : MonoBehaviour
{
    // Store the original player speed here.
    private Vector3 ogKnobPos;
    private float lowerBound, upperBound;
    [SerializeField] Camera mainCam;
    [SerializeField] private float offset = 0.5f;
    [SerializeField] private float knobLength;
    private bool isPressing;
    private float knobLowerBound
    {
        get => ogKnobPos.x;
    }

    [SerializeField] private GameObject pMovement;

    private WADSMovement getPMove() => pMovement.GetComponent<WADSMovement>();

    // Start is called before the first frame update
    void Start()
    {
        ogKnobPos = transform.localPosition; 
        float ogPlayerSpeed = getPMove().sensitivity;
        float plusMinus50 = ogPlayerSpeed * offset;
        lowerBound = ogPlayerSpeed - plusMinus50;
        upperBound = ogPlayerSpeed + plusMinus50; 

        var renderer = GetComponent<Renderer>();
        renderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        float rawVal;
        RaycastHit hit;

        if (Input.GetKeyDown(KeyCode.Mouse0)) 
            isPressing = true;
        if (Input.GetKeyUp(KeyCode.Mouse0))
            isPressing = false;

        if (isPressing)
        {
            //print("keydown");
            Vector3 mousePos = Input.mousePosition;
            Ray mouseIntoMenu = mainCam.ScreenPointToRay(mousePos);
            if (Physics.Raycast(mouseIntoMenu, out hit))
            {
                //print("hit it!");
            }
            else return;
        }
        // Add else branch for eyetracker.
        else return;

        // The transform.position should stay on the relative x-axis
        // of the menu. Unfortunately, as the player will turn and rotate,
        // the relative x-axis is a complicated calculation. ASK ABOUT IT!
        /*
        transform.position = new Vector3(
            transform.position.x + 20f * Time.deltaTime, 
            transform.position.y,
            transform.position.z);
        */
        var localPos = transform.localPosition;

        float inWorldMouseX = mainCam.ScreenToWorldPoint(Input.mousePosition).x;

        float lerpedMouseX = (inWorldMouseX * 9 + localPos.x) / 10;
        lerpedMouseX = inWorldMouseX;
        rawVal = knobLowerBound + Mathf.Max(0, lerpedMouseX - ogKnobPos.x);
        rawVal = knobLowerBound;
        print("rawVal " + rawVal);
        print("maybe the inWorldMouseX is changing? " + inWorldMouseX 
            + " even tho mouseX " + Input.mousePosition.x);
        float val = Mathf.Clamp(rawVal, 0.05f, (knobLength + ogKnobPos.x) * 0.95f);
        print("val " + val);
        localPos.x = val;
        transform.localPosition = localPos;

        float normal = (rawVal - knobLowerBound) / knobLength;

        getPMove().sensitivity = lowerBound + normal * (upperBound - lowerBound);
    }
}
