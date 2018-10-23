using UnityEngine;
using System.Collections;

public class ParallaxLayer : MonoBehaviour {

	// Use this for initialization
    public float parallaxFactor;
    public void Move(float delta)
    {
        Vector3 newPos = transform.localPosition;
        newPos.x -= delta * parallaxFactor;
        transform.localPosition = newPos;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
