using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float maxForce;


    //public Transform orientation;

    Vector2 moveDirection;

    public Rigidbody rb;

    


    //Script holt sich biem Start infos über den Rigidbody und verhinter die Rotation des Rigidbodys
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
 
    }

    //Bewegt den Spieler je nach Input
    private void FixedUpdate()
    {
        

        MovePlayer();
        //Rotat();
        Roatetion();




    }

    private void Roatetion()
    {
        Debug.Log("Rotate?");
        Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 10 * Time.deltaTime);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveDirection = context.ReadValue<Vector2>();
        Debug.Log(moveDirection);
    }


    //Bewegt den Spieler 
    private void MovePlayer()
    {
        Vector3 currentVel = rb.velocity;
        Vector3 targetVel = new Vector3(moveDirection.x,0f,moveDirection.y);

        targetVel *= moveSpeed;

        targetVel = transform.TransformDirection(targetVel);

        Vector3 velChange = (targetVel - currentVel);

        velChange = Vector3.ClampMagnitude(velChange, maxForce);

        rb.AddForce(new Vector3(velChange.x, 0f, velChange.z),ForceMode.VelocityChange);

        
    }

    //private void  Rotat()
    //{
    //    Vector3 dir = new Vector3(moveDirection.x, 0f, moveDirection.y);
    //    Quaternion lookRotation = Quaternion.LookRotation(new Vector3(dir.x,0,dir.z));
    //    transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 5 * Time.deltaTime);
    //}
}

   

