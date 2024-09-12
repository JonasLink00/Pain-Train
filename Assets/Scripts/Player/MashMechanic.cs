using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MashMechanic : MonoBehaviour
{
    [SerializeField] private int mash_counter;
    [SerializeField] private float timer;
    private float resetTimer = 7;
    private int mash_max = 5;

    [SerializeField] ParticleSystem Dropp;
    // Start is called before the first frame update
    void Start()
    {
        timer = resetTimer;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            timer = resetTimer;
            mash_counter = 0;
        }
        else if (mash_counter == mash_max)
        {
            mash_counter = 0;
            Dropp.Play();
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Mouse1))
        {
            mash_counter++;
        }
    }
}
