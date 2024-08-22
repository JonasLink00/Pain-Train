using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RBPlayerMove : MonoBehaviour
{
    private float speed = 200f;
    private float rotationSpeed = 100f;

    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");

        _rigidbody.velocity = (transform.forward * vAxis) * speed * Time.deltaTime;
        transform.Rotate((transform.up * hAxis) * rotationSpeed * Time.deltaTime);
    }


}
