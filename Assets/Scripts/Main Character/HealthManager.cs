using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthManager : MonoBehaviour {

    public float maxHP = 10;

    private float currentHealth;
    public float terrainLimit;

    private float sizeHPBlock;

    private GameObject healthBar;
    
    private bool alive { get; set; }

    private GameManager gameManager;
    private PlayerController player;
    

	// Use this for initialization
	void Start () {
        player = this.GetComponent<PlayerController>();
        gameManager = Object.FindObjectOfType<GameManager>();
        healthBar = GameObject.FindGameObjectWithTag("HealthBar");
        sizeHPBlock = healthBar.GetComponent<RectTransform>().sizeDelta.x;
        ResetHP();
        alive = true;
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(currentHealth);
        if (currentHealth <= 0 && alive)
        {
            alive = false;
            StartCoroutine(KillPlayer());
        }
        if (this.gameObject.transform.position.y < terrainLimit && alive)
        {
            alive = false;
            StartCoroutine(KillPlayer());
        }
	}

    public void ApplyDamage(float damage)
    {
        currentHealth += player.GetResistance() - damage;
        ApplyLifeChange();
    }

    public void ApplyLifeChange()
    {
        healthBar.GetComponent<RectTransform>().sizeDelta =
            new Vector2(sizeHPBlock * (currentHealth / maxHP), 
                healthBar.GetComponent<RectTransform>().sizeDelta.y);
    }

    public void ResetHP()
    {
        currentHealth = maxHP;
    }

    private IEnumerator KillPlayer()
    {
        yield return StartCoroutine(gameManager.KilledPlayer());
        alive = true;
        ResetHP();
        ApplyLifeChange();
    }
}
