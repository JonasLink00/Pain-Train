using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEditor.Rendering.LookDev;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.ProBuilder.Shapes;
using static UnityEditor.Timeline.TimelinePlaybackControls;

[RequireComponent(typeof(CharacterController))]
public class PlayerInput : MonoBehaviour
{
    
    private Vector2 _input;
    private CharacterController _characterController;
    private Vector3 _direction;

    public Vector3 Direction {  get { return _direction; } }

    [SerializeField] private float smoothTime = 0.05f;
    private float _currentVelocity;

    [SerializeField] public float baseSpeed;
    public float currentSpeed;

    private float _gravity = -9.81f;
    [SerializeField] private float gravityMultiplier = 3.0f;
    private float _velocity;

    public bool rightpunch = false;
    public bool leftpunch = false;


    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        
    }

    private void Update()
    {
        ApllyGravity();
        ApplyRotation();
        ApplyMovement();
       
    }

    private void ApllyGravity()
    {
        if (_characterController.isGrounded && _velocity < 0.0f)
        {
            _velocity = -1.0f;
        }
        else
        {
            _velocity += _gravity * gravityMultiplier * Time.deltaTime;
        }

        _direction.y = _velocity;


    }

    private void ApplyRotation()
    {
        if (_input.sqrMagnitude == 0) return;

        var targetAngle = Mathf.Atan2(_direction.x, _direction.z) * Mathf.Rad2Deg;
        var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _currentVelocity, smoothTime);
        transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);

    }

    private void ApplyMovement()
    {

        _characterController.Move(_direction * currentSpeed * Time.deltaTime);

    }
    public void Move(InputAction.CallbackContext context)
    {
        _input = context.ReadValue<Vector2>();
        _direction = new Vector3(_input.x, 0.0f, _input.y);
    }

    

    public void RightPunch(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            rightpunch = true;
        }

    }

    public void LeftPunch(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            leftpunch = true;
        }
    }

   


}
