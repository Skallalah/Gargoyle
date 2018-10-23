using UnityEngine;
using System.Collections;

public class FloatEffect : MonoBehaviour {

    Vector2 floatY;
    float originalY;

    public float floatStrength;

    void Start()
    {
        this.originalY = this.transform.position.y;
    }

    void Update()
    {
        /* Old code:
        floatY = transform.position;
        floatY.y = originalY + (Mathf.Sin(Time.time) * floatStrength);
        transform.position = floatY;
        */
        transform.position = new Vector3(transform.position.x,
            originalY + (Mathf.Sin(Time.time) * floatStrength), transform.position.z);
    }
}
