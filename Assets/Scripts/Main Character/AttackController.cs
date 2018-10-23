using UnityEngine;
using System.Collections;

public class AttackController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("TOUCHING");
            other.gameObject.GetComponent<EnemyHealth>().SendMessage("ApplyDamage", 1f);
        }
    }
}
