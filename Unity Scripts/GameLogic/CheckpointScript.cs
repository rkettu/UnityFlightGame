using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * 
 *  APPLY TO A CHECKPOINT OBJECT WITH A TRIGGER AND A LIGHT OBJECT IN ITS CHILDREN
 *  PLAYER OBJECT REQUIRES RIGIDBODY [uncheck "Use Gravity" and check "Is Kinematic"] AND TAG "Player" FOR TRIGGER TO WORK
 *  
 */
public class CheckpointScript : MonoBehaviour
{

    private RaceLogic objectRaceLogic;
    private Light currentCheckpointEffect;

    void Start()
    {
        currentCheckpointEffect = GetComponentInChildren<Light>();
        objectRaceLogic = GetComponentInParent<RaceLogic>();
    }

    private void Update()
    {
        // Lighting effect for next checkpoint
        if (transform == RaceLogic.checkpointArray[RaceLogic.currentCheckpoint].transform) 
        {
            currentCheckpointEffect.intensity = 4 + Mathf.Sin(Time.time * 2) * 2;
            currentCheckpointEffect.color = Color.red;
        }
        else        // Lighting fades when not checkpoint anymore..
        {
            currentCheckpointEffect.intensity -= 0.1f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // If triggered checkpoint is current checkpoint and object that triggered it has tag "Player"
        if (transform == RaceLogic.checkpointArray[RaceLogic.currentCheckpoint].transform && other.CompareTag("Player"))
        {
            currentCheckpointEffect.intensity = 8.0f;
            currentCheckpointEffect.color = Color.green;
            objectRaceLogic.checkpointReached();
        }
    }
}
