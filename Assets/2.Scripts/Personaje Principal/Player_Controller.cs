using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Controller : MonoBehaviour
{
    [Header("CharacterController")]
    [Space]
    [SerializeField] CharacterController player;

    [Header("Movimientos")]
    [Space]
    [SerializeField] private float horizontalMove;
    [SerializeField] private float verticalMove;
    public float playerSpeed = 12f;
    public float runSpeed = 30f;
    [SerializeField] private float stamina = 10f, timeToRecover = 5f, staminaMAX;
    public Text staminaText; 
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

    [Header("Correr con linterna")]
    [Space]
    public Transform Flashlight; 
    Vector3 xyz;


    public float PlayerSpeed { get => playerSpeed; } //Utilizado en Enter de PatrolMinion y PatrolBoss script

    void Awake()
    {
        player = GetComponent<CharacterController>();
        cam = FindObjectOfType<Camera>();

        staminaMAX = stamina; 
    }

    void Update()
    {
        staminaText.text = "STAMINA: " + stamina.ToString("0"); 
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

        if(Input.GetKey(KeyCode.LeftShift) && stamina >= 0f)
        {
            xyz = new Vector3(25, 0, 0);
            Flashlight.localEulerAngles = Vector3.Lerp(Flashlight.localEulerAngles, xyz, Time.deltaTime * 2.5f);   

            playerInput = transform.TransformDirection(playerInput) * runSpeed;

            stamina -= Time.deltaTime;  

            if(stamina <= 0f)
            {
                stamina = -timeToRecover; 
            }
        }
        else
        {
            xyz = new Vector3(0, 0, 0);
            Flashlight.localEulerAngles = Vector3.Lerp(Flashlight.localEulerAngles, xyz, Time.deltaTime * 2.5f);

            playerInput = transform.TransformDirection(playerInput) * playerSpeed;

            if(stamina < staminaMAX)
            {
                stamina += Time.deltaTime;
            }
            
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