using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    [SerializeField] private float speed;
    //[SerializeField] private float rotationSpeed;

    
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontalInput,0, verticalInput);
        moveDirection.Normalize();

        transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);

        //if (moveDirection != Vector3.zero)
        //{
        //    Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);

        //    transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);

        //}
    }
}
