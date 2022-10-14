using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class FootSteps : MonoBehaviour
{
    CharacterController characterController;
    [SerializeField] AudioSource playerWALK; 

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        //Footsteps();
        //if (characterController.isGrounded == true && characterController.velocity.magnitude < 0.1f && playerWALK.isPlaying == true)
        //{
        //    StopFootsteps(); 
        //}
    }

    public void Footsteps()
    {
        if (characterController.isGrounded == true && playerWALK.isPlaying == false)
        {
            if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                playerWALK.volume = Random.Range(0f, 1f);
                playerWALK.pitch = Random.Range(0.8f, 1.1f);
                playerWALK.Play();
            }
        }
    }

    //private void StopFootsteps()
    //{
    //        playerWALK.Stop();
    //}
}
