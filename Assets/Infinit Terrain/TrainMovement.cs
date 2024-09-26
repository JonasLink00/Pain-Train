using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainMovement : MonoBehaviour
{
    [SerializeField] private float TrainSpeed;

    void Update()
    {
        Vector3 trainmove = new Vector3(0f, 0f, TrainSpeed);
        gameObject.transform.position += trainmove * Time.deltaTime;
    }
}
