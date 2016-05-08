using UnityEngine;
using System.Collections;

public class PlayerMover : MonoBehaviour {

    public Rigidbody2D rb;
    public float speed;         // movement speed
    public float xMin, xMax, yMin, yMax;    // screen boundaries
    static public PlayerMover instance;     // instance of the PlayerMover

    // sets instance of the PlayerMover
    void Start()
        {instance = this; }

    // gets current instance of the gameController
    public static PlayerMover getInstance()
        { return instance; }

	void Update ()
    {
        // basic movement
        Vector2 movement = new Vector2();
        movement.x = Input.GetAxis("Horizontal") * speed;
        movement.y = Input.GetAxis("Vertical") * speed;

        if (movement.y > 0)     // sets direction sprite is facing
            { transform.localRotation = Quaternion.Euler(0, 0, 0); }
        else if (movement.y < 0)
            { transform.localRotation = Quaternion.Euler(180, 0, 0); }
        else if (movement.x > 0)
            { transform.localRotation = Quaternion.Euler(0, 0, 270); }
        else if (movement.x < 0)
            { transform.localRotation = Quaternion.Euler(0, 0, 90); }

        rb.velocity = movement;
        rb.position = new Vector2(Mathf.Clamp(rb.position.x, xMin, xMax), Mathf.Clamp(rb.position.y, yMin, yMax));   // stops ship from leaving screen
	}

    // item collection
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Item" || other.tag == "SpItem")
        {
            if (other.tag == "SpItem")  // "freeze" power-up
                { GameController.getInstance().Freeze(); }
            
            Destroy(other.gameObject);
            GameController.getInstance().UpdateScore(100);
            GameController.itemCount += 1;
        }
    }

    // ship shrinks when attacked by an enemy
    public void Downscale()
        { transform.localScale = new Vector3(.15f, .15f, .15f); }

    // restore size
    public void Upscale()
        { transform.localScale = new Vector3(.25f, .25f, .25f); }
}
