using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlaneControls : MonoBehaviour
{
    // variables for sensor rotation
    [SerializeField] private TcpConnector connector;
    private float[] kulmadata = new float[2]; // array to hold the most recent sensor data

    [SerializeField] private float defaultSpeed = 1f;
    private float speed; // current airplane travel speed
    private float speedAdjustFrames = 100f; // the number of iterations it takes for the plane speed to reach its target speed

    //private float fallingSpeed = 0f;

    public float planeAngleX { get; private set; }

    public float planeAngleZ { get; private set; }
    //private Vector3 previousPosition;
    //private Vector3 speedMod;


    // Start is called before the first frame update
    void Start()
    {
        connector = FindObjectOfType<TcpConnector>(); // find the TCP connection manager in the scene
        speed = defaultSpeed;
        //previousPosition = transform.position;
    }

    // FixedUpdate is called every 0.02 sec (50 times per sec) on default settings:
    void FixedUpdate()
    {

        //transform.Rotate(pitchSpeed * pitchInput, yawSpeed * yawInput, -rollSpeed * rollInput, Space.Self);
        //transform.position += new Vector3(0f, fallingSpeed, 0f);


        // rotate the plane using angles calculated from raspi sensor data:
        // remove excess sensor data and convert it to floating point numbers
        string cropped_str = connector.RaspiData.Remove(connector.RaspiData.IndexOf('\n'));
        string[] str_data = cropped_str.Split('s'); // 's' is the denominator of the different sensor axis values
        for (int i = 0; i < 2; i++)
        {
            kulmadata[i] = float.Parse(str_data[i], System.Globalization.CultureInfo.InvariantCulture);
        }
        transform.Rotate(-kulmadata[0] /22.5f, 0, kulmadata[1] /22.5f);
        //speedMod = (previousPosition + transform.position).normalized;
        //previousPosition = transform.position;

        // adjust airplane speed based on the plane angle to simulate gravitational pull
        UpdatePlaneAngleVariables();
        //speed += GetSpeedIncrementation(defaultSpeed, speed, GetTargetSpeedMod(planeAngleX, .5f, 2f), 100);
        // adjust the speed of the plane:
        speed += (defaultSpeed * GetMultiplier(planeAngleX, .5f, 2f) - speed) / speedAdjustFrames;
        transform.Translate(0f, 0f, speed);
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
        Debug.Log("Target Speed: " + GetMultiplier(planeAngleX, .5f, 2f) * defaultSpeed + "  Current speed: " + speed);
        
    }
    /*
    private float CalculateFallSpeed(float fallSpeed, float angle)
    {
        float newFallSpeed = fallSpeed;


    }
    */

    private void UpdatePlaneAngleVariables()
    {
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

        //Debug.Log("Plane rotation: " + planeAngleX + ", " + planeAngleZ);
    }

    /// <summary>
    /// Returns a multiplier based on the plane's X-axis (in degrees) angle relative to the ground.
    /// minMultiplier = when the plane is facing 90 degrees downwards.
    /// maxMultiplier = when the plane is facing (-) 90 degrees upwards.
    /// </summary>
    private float GetMultiplier(float angle, float minMultiplier, float maxMultiplier)
    {
        float returnValue = 1f;
        if (angle < 90f && angle > -90f)
        {
            // alaspäin on +angle
            // ylöspäin on -angle
            if (angle < 0f)
            {
                // decrease target speed
                returnValue =  Mathf.Lerp(1f, minMultiplier, Mathf.InverseLerp(0f, 90f, Math.Abs(angle)));
            }
            if (angle > 0f)
            {
                // increase target speed
                returnValue = Mathf.Lerp(1f, maxMultiplier, Math.Abs(angle / 90f));
            }
        }
        else
        {
            Debug.LogError("Angle out of bounds. Must be < 90 and > -90.");
        }

        return returnValue;
    }

}

