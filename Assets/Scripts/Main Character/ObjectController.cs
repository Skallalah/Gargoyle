using UnityEngine;
using System.Collections;

public class ObjectController : MonoBehaviour {

    private PlayerController player;
    private GameManager manager;

    // Use this for initialization
    void Start()
    {
        player = this.GetComponent<PlayerController>();
        manager = Object.FindObjectOfType<GameManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Coin")
        {
            manager.AddCoin();
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "PowerUp")
        {
            PowerUp powerUp = other.gameObject.GetComponent<PowerUp>();
            player.ApplyCostume(powerUp.baseForm, powerUp.baseAnim, powerUp.baseResistance, powerUp.baseMass);
            Destroy(other.gameObject);
        }
    }
}
