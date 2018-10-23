using UnityEngine;
using System.Collections;

public class JumpController : MonoBehaviour {

    private PlayerController player;

	// Use this for initialization
	void Start () {
        player = this.GetComponent<PlayerController>();
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {

            Collider2D collider = other.collider;

            Vector3 contactPoint = other.contacts[0].point;
            Vector3 center = collider.bounds.center;

            if (contactPoint.y > center.y)
                player.jumping = false;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            Collider2D collider = other.collider;

            Vector3 contactPoint = other.contacts[0].point;
            Vector3 center = collider.bounds.center;

            if (contactPoint.y > center.y)
                player.jumping = true;
        }
    }
}
