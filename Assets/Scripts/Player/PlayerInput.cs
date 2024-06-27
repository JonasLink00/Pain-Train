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

    [SerializeField] private float smoothTime = 0.05f;
    private float _currentVelocity;

    [SerializeField] private float baseSpeed;
    private float currentSpeed;

    private float _gravity = -9.81f;
    [SerializeField] private float gravityMultiplier = 3.0f;
    private float _velocity;


    [SerializeField]
    Animator animator;
    private const string RightPunchString = "RightPunch";
    private const string LeftPunchString = "LeftPunch";
    private const string MoveString = "Move";

    bool rightpunch = false;
    bool leftpunch = false;

    [SerializeField] Collider rightHandCollider, leftHandCollider;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        ResetSpeed();
    }

    private void Update()
    {
        ApllyGravity();
        ApplyRotation();
        ApplyMovement();
        ApplyMoveAnimation();
        ManagePunchAnimation();

        //Debug.Log(leftpunch + " : " + rightpunch);
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

    private void ApplyMoveAnimation()
    {
        if (_direction.x == 0 && _direction.z == 0)
        {
            animator.SetBool(MoveString, false);
        }
        else
        {
            //Debug.Log(_direction);
            animator.SetBool(MoveString, true);

        }

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

    private void SetPunchAnimation(bool _rightpunch)
    {
        if (_rightpunch)
        {
            rightHandCollider.enabled = true;
            animator.SetBool(RightPunchString, true);
            currentSpeed = 0;
        }
        else
        {
            leftHandCollider.enabled = true;
            animator.SetBool(LeftPunchString, true);
            currentSpeed = 0;
        }

    }

    private void ManagePunchAnimation()
    {
        if(rightpunch)
        {
            SetPunchAnimation(true);

        }
        else if(leftpunch)
        {
            SetPunchAnimation(false);
        }
    }

    private void ResetPunchAnimation()
    {
        animator.SetBool(RightPunchString, false);
        rightpunch = false;
        rightHandCollider.enabled = false;

        animator.SetBool(LeftPunchString, false);
        leftpunch = false;
        leftHandCollider.enabled = false;

        ResetSpeed();
    }

    private void ResetSpeed()
    {
        currentSpeed = baseSpeed;
    }


}
