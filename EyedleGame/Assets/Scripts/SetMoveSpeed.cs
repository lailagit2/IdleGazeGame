using UnityEditor;
using UnityEngine;

public class SetMoveSpeed : MonoBehaviour
{
    // Store the original player speed here.
    private Vector3 ogKnobPos;
    private float lowerBound, upperBound;
    [SerializeField] Camera mainCam;
    [SerializeField] private float offset = 0.5f;
    [SerializeField] private int knobLength;
    private int knobLowerBound
    {
        get => (int) ogKnobPos.x;
    }

    [SerializeField] private GameObject pMovement;

    private WADSMovement getPMove() => pMovement.GetComponent<WADSMovement>();

    // Start is called before the first frame update
    void Start()
    {
        ogKnobPos = transform.position; 
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
        Vector3 posOffset;
        Vector3 pointingAt;
        float rawVal;
        RaycastHit hit;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            print("keydown");
            Vector3 mousePos = Input.mousePosition;
            Ray mouseIntoMenu = mainCam.ScreenPointToRay(mousePos);
            if (Physics.Raycast(mouseIntoMenu, out hit))
            {
                print("hit it!");
                pointingAt = mousePos;

                posOffset = pointingAt - ogKnobPos;
            }
            else return;
        }
        // Add else branch for eyetracker.
        else return;

        rawVal = Mathf.Clamp(posOffset.x, ogKnobPos.x, ogKnobPos.x + knobLength);
        // The transform.position should stay on the relative x-axis
        // of the menu. Unfortunately, as the player will turn and rotate,
        // the relative x-axis is a complicated calculation. ASK ABOUT IT!
        /*
        transform.position = new Vector3(
            transform.position.x + 20f * Time.deltaTime, 
            transform.position.y,
            transform.position.z);
        */
        transform.position = hit.point;

        float normal = (rawVal - knobLowerBound) / (knobLength - knobLowerBound);

        getPMove().sensitivity = lowerBound + normal * (upperBound - lowerBound);
    }
}
