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
    private float playerSpeed = 10f;
    private float runSpeed = 24f; 
    private float gravity = 20f; 
    Vector3 playerInput;

    [Header("Camara")]
    [Space]
    Camera cam;
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
        rotarse();
        moverse();
    }

    private void moverse()
    {
        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");

        playerInput = new Vector3(horizontalMove, 0 ,verticalMove);

        if(Input.GetKey(KeyCode.LeftShift))
        {
            playerInput = transform.TransformDirection(playerInput) * runSpeed;
        }
        else
        {
            playerInput = transform.TransformDirection(playerInput) * playerSpeed;
        }

        player.Move( playerInput * Time.deltaTime);
    }

    private void rotarse()
    {
        h_mouse = mouseHorizontal * Input.GetAxis("Mouse X");
        v_mouse += mouseVertical * Input.GetAxis("Mouse Y"); 


        v_mouse = Mathf.Clamp(v_mouse, minRotation, maxRotation);

        cam.transform.localEulerAngles =  new Vector3(-v_mouse, 0, 0);

        transform.Rotate(0, h_mouse, 0);
    }
}
