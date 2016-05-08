using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour {

    public GameObject pulse;
    public float speed;
    public Rigidbody2D rb;
    public Animator anim;
    public Transform pulseSpawn;

	void Update ()
    {
        if(Input.GetKeyDown(KeyCode.DownArrow))
            {anim.SetTrigger("attack");}
	}

    public void Attack()
    {
        GameObject p = Instantiate(pulse, pulseSpawn.position, pulseSpawn.rotation) as GameObject;
        p.GetComponent<Rigidbody2D>().velocity = transform.right * speed;
    }
}
