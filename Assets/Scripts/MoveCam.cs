using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MoveCam : MonoBehaviour
{
    public Transform cameraPosition;
    //Updated die Kamera Position
    void Update()
    {
        transform.position = cameraPosition.position;
        
    }
}
