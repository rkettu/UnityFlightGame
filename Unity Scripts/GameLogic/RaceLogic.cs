using UnityEngine;
using System.Collections.Generic;
using System.Collections;



/*
 * 
 * APPLY TO A PARENT OBJECT CONTAINING ALL CHECKPOINTS AS CHILDREN
 * 
 */
public class RaceLogic : MonoBehaviour
{
    public int childrenAmount = 0;
    public static Transform[] checkpointArray = new Transform[20];
    public static int currentCheckpoint = 0;    // Static so the same value can be accessed by all checkpoint objects

    void Start()
    {
        childrenAmount = transform.childCount;
        for (int i = 0; i < childrenAmount; i++)
        {
            checkpointArray[i] = transform.GetChild(i);
        }
    }

    void Update()
    {
        //Debug.Log(currentCheckpoint);
    }

    public void checkpointReached()
    {
        currentCheckpoint++;
        Debug.Log("Checkpoint " + currentCheckpoint + " reached!");
        if(currentCheckpoint == transform.childCount)
        {
            currentCheckpoint = 0;  // Set first checkpoint active again..
            // Code for player victory here
        }
    }
}

