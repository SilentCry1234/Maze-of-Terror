using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    [Header("CharacterController")]
    [Space]
    [SerializeField] CharacterController player; 

    [Header ("Movimientos")]
    [Space]
    [SerializeField] private float horizontalMove; 
    [SerializeField] private float verticalMove;
    private float playerSpeed = 25; 

    void Start()
    {
        player = GetComponent<CharacterController>();
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        moverse();
    }

    private void moverse()
    {
        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");

        player.Move(new Vector3(horizontalMove, 0 , verticalMove) * playerSpeed * Time.deltaTime);
    }
}
