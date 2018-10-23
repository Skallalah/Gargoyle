using UnityEngine;
using System.Collections;
using UnityEditor.Animations;

public class EnemyAttack : MonoBehaviour {

	// Use this for initialization

    private EnemyAI enemy;
    private Animator anim;

	void Start () {
        enemy = this.gameObject.GetComponent<EnemyAI>();
        anim = this.gameObject.GetComponent<Animator>();
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Vector3 contactPoint = other.contacts[0].point;
            Vector3 center = other.collider.bounds.center;
            int directionImpulse = (contactPoint.x > center.x ? -1 : 1);
            if ((enemy.right ? 1 : -1) == directionImpulse)
            {
                anim.SetTrigger("hit");
                other.gameObject.GetComponent<HealthManager>().SendMessage("ApplyDamage", 3f);
                Vector2 force = other.transform.position - transform.position;
                force.Normalize();
                other.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(directionImpulse * 200f, 1f), ForceMode2D.Force);
            }
        }
    }
}
