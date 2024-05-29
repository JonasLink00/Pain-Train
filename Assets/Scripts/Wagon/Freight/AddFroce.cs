using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddFroce : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField]
    float magnitude = 500;
    // Start is called before the first frame update
    void OnEnable()
    {
        Debug.Log("Force");
        rb = GetComponent<Rigidbody>();

       
    }

    
}
