using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animations : MonoBehaviour
{
    [SerializeField]
    Animator animator;
    private const string RightPunchString = "RightPunch";
    private const string LeftPunchString = "LeftPunch";
    private const string MoveString = "Move";

    

    //[SerializeField] Collider rightHandCollider, leftHandCollider;

    [SerializeField] Collider attackRangefield;

    PlayerInput playerInput;


    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        ResetSpeed();
    }

    private void Update()
    {
        ApplyMoveAnimation();
        ManagePunchAnimation();
    }
    private void ApplyMoveAnimation()
    {
        if (playerInput.Direction.x == 0 && playerInput.Direction.z == 0)
        {
            animator.SetBool(MoveString, false);
        }
        else
        {
            //Debug.Log(_direction);
            animator.SetBool(MoveString, true);

        }

    }

    //depending on Animation

    //private void SetPunchAnimation(bool _rightpunch)
    //{
    //    if (_rightpunch)
    //    {
    //        rightHandCollider.enabled = true;
    //        animator.SetBool(RightPunchString, true);
    //        playerInput.currentSpeed = 0;
    //    }
    //    else
    //    {
    //        leftHandCollider.enabled = true;
    //        animator.SetBool(LeftPunchString, true);
    //        playerInput.currentSpeed = 0;
    //    }

    //}

    //not depending on Animations 1
    //private void SetPunchAnimation(bool _rightpunch)
    //{
    //    if (_rightpunch)
    //    {
    //        animator.SetBool(RightPunchString, true);
    //        playerInput.currentSpeed = 0;
    //    }
    //    else
    //    {
    //        animator.SetBool(LeftPunchString, true);
    //        playerInput.currentSpeed = 0;
    //    }

    //}

    //not depending on Animations 2

    private void SetPunchAnimation(bool _rightpunch)
    {
        

        if (_rightpunch)
        {
            animator.SetBool(RightPunchString, true);
            playerInput.currentSpeed = 0;
        }
        else
        {
            animator.SetBool(LeftPunchString, true);
            playerInput.currentSpeed = 0;
        }

    }

    private void ManagePunchAnimation()
    {
        if (playerInput.rightpunch)
        {
            SetPunchAnimation(true);

        }
        else if (playerInput.leftpunch)
        {
            SetPunchAnimation(false);
        }
    }

    //Animation Event
    private void ResetPunchAnimation()
    {
        attackRangefield.enabled = false;

        animator.SetBool(RightPunchString, false);
        playerInput.rightpunch = false;
        //rightHandCollider.enabled = false;

        animator.SetBool(LeftPunchString, false);
        playerInput.leftpunch = false;
        //leftHandCollider.enabled = false;

        ResetSpeed();
    }

    private void ResetSpeed()
    {
        playerInput.currentSpeed = playerInput.baseSpeed;
    }

    //Animation Event
    //private void AktivateRightPunch()
    //{
    //    rightHandCollider.enabled = true;
    //}

    ////Animation Event
    //private void AktivateLeftPunch()
    //{
    //    leftHandCollider.enabled = true;
    //}

    private void SpawnAttackRangeField()
    {
        attackRangefield.enabled = true;
    }
}
