using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;

    public Vector3 PlayerMovmentInput;
    public float groundDrag;   

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatisGround;
    [SerializeField]
    private bool grounded;
    [SerializeField]
    private Transform GroundCheckPosition;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;
    float SphereCheck = 0.5f;

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
        GroundCheck();
        SpeedControl();
    }

    //Check ob der Spieler auf dem Boden ist und implementiert einen Drag effekt wenn es ist
    private void GroundCheck()
    {
        //ground check
        grounded = Physics.CheckSphere(GroundCheckPosition.position, SphereCheck, whatisGround);

        //handel drag
        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;

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
    //Zeigt den GroundCheck in der Szene
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(GroundCheckPosition.position, SphereCheck);
    }

}
