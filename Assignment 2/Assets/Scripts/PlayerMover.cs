using UnityEngine;
using System.Collections;

public class PlayerMover : MonoBehaviour {

    public Rigidbody2D rb;
    public float speed;
    public float jump;
    public Animator anim;

    void Update()
    {
        Vector2 movement = new Vector2();
        movement.x = Input.GetAxis("Horizontal") * speed;
        if (movement.x < 0)     // sets direction sprites are facing
        { transform.localRotation = Quaternion.Euler(0, 180, 0); }
        else if (movement.x > 0)
        { transform.localRotation = Quaternion.Euler(0, 0, 0); }
        movement.y = 0;

        if (Input.GetKeyDown(KeyCode.UpArrow))  // jump
        {
            movement.y = movement.y + jump;
            anim.SetBool("jump", true);
        }
        else 
            {anim.SetFloat("speed", movement.magnitude);}   // this is in the else because otherwise it triesto play both animations at once

        rb.velocity = movement;
    }

   void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground" || other.gameObject.tag == "Item")
            { anim.SetBool("jump", false); }
    }
}
