using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 * PLAYER OBJECT REQUIRES RIGIDBODY [uncheck "Use Gravity" and check "Is Kinematic"] AND TAG "Player" FOR TRIGGER TO WORK
 * 
 */
public class CollectableScript : MonoBehaviour
{
    private float max;
    private float min;
    public static int collectedCoins = 0;

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

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            collectedCoins++;
            Debug.Log("Coins collected: " + collectedCoins);
        }
    }
}
