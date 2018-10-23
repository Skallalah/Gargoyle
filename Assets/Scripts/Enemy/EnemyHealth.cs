using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    public float maxHealth = 2f;
    public float currentHealth;
    bool damaged;

    private Rigidbody2D rigibody;
    private Animator anim;

    // Use this for initialization
    void Start()
    {
        rigibody = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update () {
        //if (damaged)
        //    // do something
        //else
        //    // do something
        //damaged = false;
    }

    void ApplyDamage(float damage)
    {
        damaged = true;

        if (currentHealth - damage > 0)
            this.currentHealth = this.currentHealth - damage;
        else
            Object.Destroy(this.gameObject);
    }
}
