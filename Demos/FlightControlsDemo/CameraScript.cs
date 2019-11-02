using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject PlayerCylinder;

    private float height = 2.0f;
    private float distance = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = PlayerCylinder.transform.TransformPoint(0f, height, -distance);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = PlayerCylinder.transform.TransformPoint(0f, height, -distance);
        transform.rotation = PlayerCylinder.transform.rotation;
    }
}
