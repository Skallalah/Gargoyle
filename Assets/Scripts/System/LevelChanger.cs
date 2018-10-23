using UnityEngine;
using System.Collections;

public class LevelChanger : MonoBehaviour {

    public string nextLevel;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (nextLevel != "")
                Application.LoadLevel(nextLevel);
            else
                Application.LoadLevel("Main Menu");
        }
    }
}
