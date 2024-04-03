using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;

    private Vector3 PlayerMovmentInput;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    public Rigidbody rb;

    private float rotationR = 1f;
    private float rotationL = -1f;
    private float rotateAround = 180f;
    public float rotateSpeed;


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
        turnAround();
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
        if(Input.GetKey(KeyCode.E)) 
        {
            transform.Rotate(0f, rotationR * rotateSpeed, 0f);
        }
        else if(Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(0f, rotationL * rotateSpeed, 0f);
        }
        else if (Input.GetKey(KeyCode.R))
        {
            transform.rotation = Quaternion.identity ;
        }
        
    }

    private void turnAround()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("180");
            transform.Rotate(0f, rotateAround, 0f);
        }
       
    }

}