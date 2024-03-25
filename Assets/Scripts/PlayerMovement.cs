using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;

    public Vector3 PlayerMovmentInput;
    public float groundDrag;   

    public Transform orientation;

    float horizontalInput;
    float verticalInput;
    

    Vector3 moveDirection;

    public Rigidbody rb;


    //Script holt sich biem Start infos über den Rigidbody und verhinter die Rotation des Rigidbodys
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    //Updated die Infos des GroundChecks, den Sprung und der Bewegunsgeschwindigkeit
    private void Update()
    {
        SpeedControl();
    }

    
    //Bewegt den Spieler je nach Input
    private void FixedUpdate()
    {
        MovePlayer(); 
    }
   

    //Bewegt den Spieler 
    private void MovePlayer()
    {
        PlayerMovmentInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        Vector3 moveMent = orientation.TransformDirection(PlayerMovmentInput) * moveSpeed;
        rb.velocity = new Vector3(moveMent.x, rb.velocity.y, moveMent.z);


        //calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

    }

    //Regulerit die Geschwindigkeit des Spielers
    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        Vector3 limitedVel = flatVel.normalized * moveSpeed;
        rb.velocity = new Vector3(limitedVel.x,rb.velocity.y, limitedVel.z);
    }

}
