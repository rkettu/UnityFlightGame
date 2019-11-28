using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlaneControls : MonoBehaviour
{
    // variables for sensor rotation
    [SerializeField] private TcpConnector connector;
    private float[] kulmadata = new float[2]; // array to hold the most recent sensor data

    private float speed = 0.1f;

    private float fallingSpeed = 0f;
    
    private float planeAngleX, planeAngleY, planeAngleZ;
    private Vector3 previousPosition;
    private Vector3 speedMod;


    // Start is called before the first frame update
    void Start()
    {
        connector = FindObjectOfType<TcpConnector>();
        //connector = GameObject.Find("ConnectionManager").GetComponent<TcpConnector>(); // find the TCP connection manager in the scene
        transform.position = new Vector3(1, 0, 0);
        previousPosition = transform.position;
    }

    void FixedUpdate()
    {

        //transform.Rotate(pitchSpeed * pitchInput, yawSpeed * yawInput, -rollSpeed * rollInput, Space.Self);
        transform.position += new Vector3(0f, fallingSpeed, 0f);


        // rotate the plane using angles calculated from raspi sensor data:
        // remove excess sensor data and convert it to floating point numbers
        string cropped_str = connector.RaspiData.Remove(connector.RaspiData.IndexOf('\n'));
        string[] str_data = cropped_str.Split('s'); // 's' is the denominator of the different sensor axis values
        for (int i = 0; i < 2; i++)
        {
            kulmadata[i] = float.Parse(str_data[i], System.Globalization.CultureInfo.InvariantCulture);
        }
        //transform.rotation = Quaternion.Euler(-kulmadata[0], 0, kulmadata[1]);
        transform.Rotate(-kulmadata[0] /22.5f, 0, kulmadata[1] /22.5f);
        //speedMod = (previousPosition + transform.position).normalized;
        //previousPosition = transform.position;
        
        transform.Translate(0f, 0f, speed);
        
    }

    // Update is called once per frame
    void Update()
    {

        /*
        planeAngleX = transform.rotation.eulerAngles.x;
        planeAngleZ = transform.rotation.eulerAngles.z;
        if (planeAngleX > 180f)
        {
            planeAngleX -= 360f;
            if (planeAngleX > 90f) planeAngleX -= 180f;
        }
        if (planeAngleZ > 180f)
        {
            planeAngleZ -= 360f;
            if (planeAngleZ > 90f) planeAngleZ -= 180f;
        }

        Debug.Log("Plane rotation: " + planeAngleX + ", " + planeAngleZ);
        */
    }
}