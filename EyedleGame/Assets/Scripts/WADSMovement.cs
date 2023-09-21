using UnityEngine;

public class WADSMovement : MonoBehaviour
{

    public float sensitivity = 2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (Input.GetKey(KeyCode.A))
            rb.AddForce(-this.gameObject.transform.right * sensitivity);
        if (Input.GetKey(KeyCode.D))
            rb.AddForce(this.gameObject.transform.right * sensitivity);
        if (Input.GetKey(KeyCode.W))
            rb.AddForce(this.gameObject.transform.forward * sensitivity);
        if (Input.GetKey(KeyCode.S))
            rb.AddForce(-this.gameObject.transform.forward * sensitivity);

    }
}
