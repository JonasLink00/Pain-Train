using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private float TrainSpeed;
     
    // Update is called once per frame
    void Update()
    {
        Vector3 trainmove = new Vector3(0f, 0f, TrainSpeed);
        gameObject.transform.position += trainmove * Time.deltaTime;
    }
}
