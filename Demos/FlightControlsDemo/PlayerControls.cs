using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    bool speedUp;
    bool speedDown;
    private float pitchInput;
    private float yawInput;
    private float rollInput;
    private float speed = 0.5f;
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
        transform.position = new Vector3(1, 0, 0);
    }

    void FixedUpdate()
    {
        if (speed < 0.3f)      // If speed gets too low, plane starts droppin
        {
            fallingSpeed = -0.004f / Math.Max(speed, 0.01f);
        }
        else
        {
            fallingSpeed = 0f;
        }
        if (speedUp) speed += 0.01f;
        if (speedDown) speed -= 0.01f;

        transform.Translate(0f, 0f, speed);
        transform.Rotate(pitchSpeed * pitchInput, yawSpeed * yawInput, -rollSpeed * rollInput, Space.Self);
        transform.position += new Vector3(0f, fallingSpeed, 0f);
        //transform.rotation = Quaternion.Euler(x, y, z);   // x, y from accelerometer
    }

    // Update is called once per frame
    void Update()
    {
        speedUp = Input.GetKeyDown(KeyCode.X);
        speedDown = Input.GetKeyDown(KeyCode.Z);
        pitchInput = Input.GetAxis("Pitch");
        rollInput = Input.GetAxis("Roll");
        yawInput = Input.GetAxis("Yaw");
    }
}