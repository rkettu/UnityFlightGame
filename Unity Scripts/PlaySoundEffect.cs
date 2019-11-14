using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 *  CAN BE USED TO PLAY SOUNDS, CALL FUNCTIONS FROM ANOTHER SCRIPT
 *  USEFUL FOR PLAYING SOUNDS EMITTED FROM DESTROYING OBJECTS
 *  ATTACH TO AN EMPTY GAMEOBJECT
 *  ADD SOUND FILE TO AudioClip IN INSPECTOR
 *  ADD AudioSource COMPONENT
 */
public class PlaySoundEffect : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private AudioClip coinSound;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void playCoinSound()
    {
        audioSource.PlayOneShot(coinSound, 1f);
    }
}
