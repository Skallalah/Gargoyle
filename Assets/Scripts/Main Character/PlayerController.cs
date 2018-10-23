using UnityEngine;
using UnityEditor.Animations;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float maxSpeed = 3;
    public float jumpForce = 150f;
    public float moveForce = 350f;
    public Vector2 dashForce = new Vector2(100f, 50f);

    public float timeBeforeNewDash = 0.05f;

    public bool jumping = true;
    private bool jump = false;

    private bool dashing = false;
    private int dash = 1;
    private int maxDash = 1;

    private bool punching = true;
    private float lastpunch;

    private float direction;

    private float timerGlobal = 0.0f;
    private float timerDashRecharge = 0.0f;
    private float lastdash;

    public string currentForm = "base";
    private float resistance = 1;
    private float mass = 1;

    private Rigidbody2D rigibody;
    private Animator anim;

	// Use this for initialization
	void Start () {
        rigibody = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

        timerGlobal += Time.deltaTime;
        anim.SetBool("jumping", jumping);

        if (Input.GetAxis("Horizontal") != 0)
        {
            transform.localScale = new Vector3(direction, 1, 1);
        }
        
        if (Input.GetButtonDown("Jump") && !jumping)
        {
            jump = true;
        }

        if (timerGlobal >= lastdash + 0.2f)
        {
            dashing = false;
            anim.SetBool("dashing", dashing);
            this.gameObject.transform.Find("DashRange").gameObject.SetActive(false);
        }

        if (timerGlobal >= lastpunch + 0.2f)
        {
            punching = false;
            if (currentForm == "base")
                this.gameObject.transform.Find("HandRange").gameObject.SetActive(false);
            else
                this.gameObject.transform.Find("SwordRange").gameObject.SetActive(false);
        }

        if (timerGlobal >= timerDashRecharge + timeBeforeNewDash && dash < maxDash)
        {
            dash++;
            timerDashRecharge = timerGlobal;
            
        }
        anim.SetBool("dashing", dashing);
	
	}

    void FixedUpdate ()
    {
        float input = Input.GetAxis("Horizontal");

        if (!dashing)
        {

            // Move the player by adding a force to its rigidbody
            anim.SetFloat("speed", Mathf.Abs(input));

            if (input * rigibody.velocity.x < maxSpeed)
            {
                rigibody.AddForce(Vector2.right * input * moveForce);
            }

            if (Mathf.Abs(rigibody.velocity.x) > maxSpeed)
                rigibody.velocity = new Vector2(Mathf.Sign(rigibody.velocity.x) * maxSpeed, rigibody.velocity.y);

            if (jump)
            {
                rigibody.AddForce(new Vector2(0f, jumpForce));
                jump = false;
            }

            if (Input.GetButton("Attack") && !jumping && !punching)
            {
                lastpunch = timerGlobal;
                punching = false;
                anim.SetTrigger("punching");
                if (currentForm == "base")
                    this.gameObject.transform.Find("HandRange").gameObject.SetActive(true);
                else
                    this.gameObject.transform.Find("SwordRange").gameObject.SetActive(true);
            }
        }

        if (input != 0)
            direction = (input < 0f ? -1 : 1);

        if (Input.GetButton("Dash") && dash > 0)
        {
            lastdash = timerGlobal;
            timerDashRecharge = timerGlobal;
            dash--;
            dashing = true;
            this.gameObject.transform.Find("DashRange").gameObject.SetActive(true);
            rigibody.AddForce(direction * dashForce, ForceMode2D.Impulse);
        }

    }

    public void ApplyCostume(string newState, AnimatorController newAnim, float newResist, float newMass)
    {
        anim.runtimeAnimatorController = newAnim;
        currentForm = newState;
        resistance = newResist;
        mass = newMass;
        ApplyForm();
    }

    public void ApplyForm()
    {
        gameObject.GetComponent<Rigidbody2D>().mass = mass;
    }

    public float GetResistance()
    {
        return resistance;
    }
}
