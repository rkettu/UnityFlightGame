using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlaneControls : MonoBehaviour
{
    // variables for sensor rotation
    [SerializeField] private TcpConnector connector;
    private float[] kulmadata = new float[2];

    bool speedUp;
    bool speedDown;
    private float pitchInput;
    private float yawInput;
    private float rollInput;
    private float speed = 0.1f;
    private float pitchSpeed = 1f;
    private float rollSpeed = 1.5f;
    private float yawSpeed = 1f;
    private float fallingSpeed = 0f;
    /*
    private float x = 0f;
    private float y = 0f;
    private float z = 0f;
    */

    // Start is called before the first frame update
    void Start()
    {
        connector = GameObject.Find("ConnectionManager").GetComponent<TcpConnector>();
        transform.position = new Vector3(1, 0, 0);
    }

    void FixedUpdate()
    {
        if (speed < 0.03f)      // If speed gets too low, plane starts droppin
        {
            fallingSpeed = -0.004f / Math.Max(speed, 0.01f);
        }
        else
        {
            fallingSpeed = 0f;
        }
        if (speedUp) speed += 0.01f;
        if (speedDown) speed -= 0.01f;

        
        //transform.Rotate(pitchSpeed * pitchInput, yawSpeed * yawInput, -rollSpeed * rollInput, Space.Self);
        transform.position += new Vector3(0f, fallingSpeed, 0f);
        //transform.rotation = Quaternion.Euler(x, y, z);   // x, y from accelerometer

        // rotate the plane using angles calculated from raspi sensor data:
        string cropped_str = connector.RaspiData.Remove(connector.RaspiData.IndexOf('\n'));
        string[] str_data = cropped_str.Split('s');
        for (int i = 0; i < 2; i++)
        {
            kulmadata[i] = float.Parse(str_data[i], System.Globalization.CultureInfo.InvariantCulture);
        }
        //transform.rotation = Quaternion.Euler(-kulmadata[0], 0, kulmadata[1]);
        transform.Rotate(-kulmadata[0] /22.5f, 0, kulmadata[1] /22.5f);
        
        transform.Translate(0f, 0f, speed);
    }

    // Update is called once per frame
    void Update()
    {
        /*
        speedUp = Input.GetKeyDown(KeyCode.X);
        speedDown = Input.GetKeyDown(KeyCode.Z);
        pitchInput = Input.GetAxis("Vertical");
        rollInput = Input.GetAxis("Horizontal");
        //yawInput = Input.GetAxis("Yaw");
        */
    }
}