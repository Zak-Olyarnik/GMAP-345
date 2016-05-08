using UnityEngine;
using System.Collections;

public class PlayerMover : MonoBehaviour 
{
    public Rigidbody rb;
    public float speed;
    public float jumpHeight;

	void Update () 
    {
        // jump
        float yVelocity = rb.velocity.y;
        if (Input.GetButtonDown("Jump")) yVelocity += jumpHeight;
        Vector3 jump = new Vector3(0, yVelocity, 0);

        // rotation
        float rotate = Input.GetAxis("Horizontal") * speed;
        transform.Rotate(0, rotate, 0);

        // final movement calculation
        float move = Input.GetAxis("Vertical");
        Vector3 forward = transform.forward * move * speed;
        rb.velocity = forward + jump;
    }
}
