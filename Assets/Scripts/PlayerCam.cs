using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public float sencX;
    public float sencY;

    //public Transform orientation;
    public Transform playerTransform;
    public Transform cameraholder;

    float xRotation;
    float yRotation;

    //die Kamera folgt dem Cursor ab dem Start
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // nutz den Mous Input um die Kamera zu bewegen 
    private void Update()
    {
        //get Mouse Input
        float MouseX = Input.GetAxisRaw("Mouse X")  * sencX;
        float MouseY = Input.GetAxisRaw("Mouse Y")  * sencY;

        yRotation += MouseX;


        xRotation -= MouseY;
        xRotation = Mathf.Clamp(xRotation, -40f, 40f);

        //rotat camera and orientate
        //transform.rotation = Quaternion.Euler(xRotation, transform.rotation.y, 0);
        //orientation.rotation = Quaternion.Euler(0, yRotation, 0);

        cameraholder.transform.eulerAngles = new Vector3(xRotation, cameraholder.transform.eulerAngles.y, cameraholder.transform.eulerAngles.z);

        playerTransform.Rotate(Vector3.up * MouseX);
    }
    
}
