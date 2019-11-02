using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    private float boostInput;
    private float pitchInput;
    private float yawInput;
    private float rollInput;
    private float speed = 0.1f;
    private float pitchSpeed = 1f;
    private float rollSpeed = 1.5f;
    private float yawSpeed = 1f;
    private float x = 0f;
    private float y = 0f;
    private float z = 0f;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(1, 0, 0);
    }

    void FixedUpdate()
    {
        transform.Translate(0f, 0f, speed);
        //transform.Rotate(pitchSpeed*pitchInput, yawSpeed*yawInput, -rollSpeed*rollInput, Space.Self);
        transform.rotation = Quaternion.Euler(x, y, z);
    }

    // Update is called once per frame
    void Update()
    {
        x += 0.1f;
        y += 0.01f;
        z += 0.01f;
        boostInput = Input.GetAxis("Jump");
        pitchInput = Input.GetAxis("Pitch");
        rollInput = Input.GetAxis("Roll");
        yawInput = Input.GetAxis("Yaw");
    }
}
