using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

    public float maxSpeed = 0.3f;
    public float moveForce = 10f;

    public Vector2 moveDirection;
    private Rigidbody2D rigidbody;
    public GameObject enemy;

    private GameObject ground;
    private GameObject oldGround;
    private float xPosition = 0;
    private float lengthGround = 0;



    public float maxDistance = 150;
    public float currentDistance = 0;
    public bool right = true;





    // Use this for initialization
    void Start () {
        rigidbody = GetComponent<Rigidbody2D>();
        moveDirection = transform.right;
	}
	
	// Update is called once per frame
	void Update () {
        //if (ground)
        //    Round();
        if (Mathf.Abs(rigidbody.velocity.x) < maxSpeed)
        {
            rigidbody.AddForce(moveDirection * moveForce);
        }
        else
        {
            rigidbody.velocity = new Vector2(Mathf.Sign(rigidbody.velocity.x) * maxSpeed, rigidbody.velocity.y);
        }
	}

    void Round()
    {
        currentDistance = enemy.transform.position.x;
        float distRight = xPosition + (lengthGround / 2);
        float distLeft = xPosition - (lengthGround / 2);

        bool condition1 = (currentDistance >= distRight) && right;
        bool condition2 = (currentDistance <= distLeft) && !right;

        /*Debug.Log("Positon enemy " + currentDistance + " xPosition "
          + xPosition + " lengthGround  " + lengthGround  + " right " + right
          + " test1 " + condition1
          + " test2 " + condition2);*/


        if (condition1 || condition2)
            changeDirectionAI();
        
    }

    void changeDirectionAI()
    {
        if (right)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            moveDirection = -transform.right;
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
            moveDirection = transform.right;
        }
        right = !right;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //if (other.gameObject.tag == "Ground")
        //{
        //    oldGround = ground;
        //    ground = other.gameObject;
        //    lengthGround = other.collider.bounds.size.x;
        //    xPosition = ground.transform.parent.localPosition.x;

        //}

        if (other.gameObject.tag == "Limit")
        {
            changeDirectionAI();
        }
    }

}
