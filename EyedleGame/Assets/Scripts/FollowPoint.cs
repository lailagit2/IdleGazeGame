/*
 * Written by Rua M. Williams
 * This script causes a player to follow, by gaze and by distance, an object transform.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPoint : MonoBehaviour
{
    public Transform followThis;
    public Transform immediateFollow;
    private Rigidbody rb;

    public float minDistance = 10f;
    public float walkSpeed = 2f;
    public float turnSpeed = 0.2f;

    // Start is called before the first frame update
    void Start()
    {

        if (followThis == null)
        {
            Debug.Log("No follow object assinged");
        }

        rb = this.transform.parent.gameObject.GetComponent<Rigidbody>();
        if (rb == null)
            rb = this.GetComponent<Rigidbody>();
        if (rb == null)
            Debug.Log("No rigid body found");
    }

    // Update is called once per frame
    void Update()
    {
        immediateFollow.LookAt(followThis, Vector3.up);

        this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, immediateFollow.rotation, turnSpeed);   
        //atharva the turn speed probably needs to change (get faster) the further to the left or right the gaze is

        if (Vector3.Distance(this.transform.position, followThis.position) > minDistance)
        {
            Debug.Log("The dot is far away and we are walking toward it: " + Vector3.Distance(this.transform.position, followThis.position));
            rb.AddForce(this.gameObject.transform.forward * walkSpeed);
        }
        else
        {
            Debug.Log("The dot is too close");
        }
    }
}
