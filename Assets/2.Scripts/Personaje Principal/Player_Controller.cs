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
    private float playerSpeed = 12f; 
    private float runSpeed = 30f; 
    Vector3 playerInput;

    [Header ("Gravedad")]
    [Space]
    public Transform groundCheck;
    public LayerMask groundMask;
    public float groundDistance = 0.3f;
    private float gravity = -9.8f; 
    Vector3 velocity; 
    bool isGrounded; 

    [Header("Camara")]
    [Space]
    [SerializeField] Camera cam; 
    float mouseHorizontal = 3f; 
    float mouseVertical = 2f; 
    float minRotation = -65f; 
    float maxRotation = 60f; 
    float h_mouse, v_mouse; 
    
    void Start()
    {
        player = GetComponent<CharacterController>();
        cam = FindObjectOfType<Camera>();
    }

    void Update()
    {
        SetGravity();
        Moverse();
        Mirar();
    }

    private void Moverse()
    {
        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");

        playerInput = new Vector3(horizontalMove, 0 ,verticalMove);
        playerInput = Vector3.ClampMagnitude(playerInput, 1);

        if(Input.GetKey(KeyCode.LeftShift))
        {
            playerInput = transform.TransformDirection(playerInput) * runSpeed;
        }
        else
        {
            playerInput = transform.TransformDirection(playerInput) * playerSpeed;
        }

        player.Move(playerInput * Time.deltaTime);
    }

    private void Mirar()
    {
        h_mouse = mouseHorizontal * Input.GetAxis("Mouse X");
        v_mouse += mouseVertical * Input.GetAxis("Mouse Y"); 


        v_mouse = Mathf.Clamp(v_mouse, minRotation, maxRotation);

        cam.transform.localEulerAngles =  new Vector3(-v_mouse, 0, 0);

        transform.Rotate(0, h_mouse, 0);
    }

    private void SetGravity()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;
        player.Move(velocity*Time.deltaTime);   
    }
}
