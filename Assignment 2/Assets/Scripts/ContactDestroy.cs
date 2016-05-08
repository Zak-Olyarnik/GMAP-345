using UnityEngine;
using System.Collections;

public class ContactDestroy : MonoBehaviour {

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Item")
            {Destroy(gameObject);}
    }
}
