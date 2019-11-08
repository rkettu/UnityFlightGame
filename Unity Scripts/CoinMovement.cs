using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinMovement : MonoBehaviour
{
    private float max;
    private float min;

    // Start is called before the first frame update
    void Start()
    {
        float movement_height = 0.75f;
        max = transform.position.y + movement_height;
        min = max - movement_height * 2;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, Mathf.PingPong(Time.time * 0.75f, max - min) + min, transform.position.z);
        transform.Rotate(new Vector3(0, 80, 0) * Time.deltaTime);
    }

}
