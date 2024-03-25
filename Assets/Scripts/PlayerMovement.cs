using JetBrains.Annotations;
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

    //Bewegt den Spieler je nach Input
    private void Update()
    {
        MovePlayer();
        RotatePlayer();
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
    
    private void RotatePlayer()
    {
        Vector3  mouseposition = Input.mousePosition;
        mouseposition = Camera.main.ScreenToWorldPoint(mouseposition);

        Vector2 direction = new Vector2(mouseposition.x - transform.position.x,mouseposition.y - transform.position.y);

        transform.up = direction;
    }
}
