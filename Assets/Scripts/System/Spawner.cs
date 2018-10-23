using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

    public GameObject objectToSpawn;

	// Use this for initialization
	void Start () {
        spawn();
	}

    public void spawn()
    {
        GameObject.Instantiate(objectToSpawn, this.transform.position, Quaternion.identity);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
