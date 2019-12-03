using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovementScript : MonoBehaviour
{
    
    private float movementX = 30f;  
    private float movementFreq = 0.5f;    // Try values under 1f
    
    // Start is called before the first frame update
    void Start()
    {

    }      

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * Mathf.Sin(movementFreq * Time.time) * movementX);
        //transform.position = new Vector3(originalPos + Mathf.Lerp(0, movementX, (Mathf.Sin(movementSpeed * Time.time) + 1.0f) / 2.0f), transform.position.y, transform.position.z);
    }
}
