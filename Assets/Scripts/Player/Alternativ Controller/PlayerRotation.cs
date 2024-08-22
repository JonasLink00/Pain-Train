using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRotation : MonoBehaviour
{
    private float rotationDirectionInput;
    private float rotateAround = 180f;
    public float rotateSpeed;

    private void Update()
    {
        RotatePlayer();
    }

    private void RotatePlayer()
    {
<<<<<<< HEAD
        transform.Rotate(0f, rotationDirectionInput * rotateSpeed * Time.deltaTime, 0f);
=======
        transform.Rotate(0f, rotation * rotateSpeed * Time.deltaTime, 0f);
>>>>>>> inarbeit
    }

    public void ResetRotation(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            RotationReset();
        }
    }

    private void RotationReset()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.rotation = Quaternion.identity;
        }

    }
    public void OnRotate(InputAction.CallbackContext context)
    {
        rotationDirectionInput = context.ReadValue<float>();
    }

    public void OnTurn(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            turnAround();
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
