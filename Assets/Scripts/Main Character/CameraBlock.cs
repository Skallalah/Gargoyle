using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraBlock : MonoBehaviour {

    public float minimumY;
    public float maximumY;
    public GameObject backgroundImg;

    //private bool followXAxis { get; set; }

    private Camera cam;

	// Use this for initialization
	void Start () {
        cam = gameObject.GetComponentInChildren<Camera>();
        Debug.Log("Cam :" + cam);
        
        
	}

	void Update () {

        //float camSize = cam.orthographicSize;
        if (this.gameObject.transform.position.y < minimumY)
        {
            //Debug.Log("STOP CAMERA");
            cam.transform.position = new Vector3(this.gameObject.transform.position.x, minimumY, cam.transform.position.z);
        }
	
	}
}
