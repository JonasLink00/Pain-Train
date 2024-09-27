using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainManager : MonoBehaviour
{
    [SerializeField] private PlayerInput player;
    [SerializeField] private WagonTrigger trigger;
    [SerializeField] private EnemyController WorkingEnemy;
    [SerializeField] private EnemyController PassangerEnemy;
    [SerializeField] private AudioSource TrainSound;
    [SerializeField] private AudioSource TrainShakeSound;


    [SerializeField] CameraShake camerashake;

    bool checkShake = false;

    [SerializeField] private float Shake_duration = 2f;
    [SerializeField] private float Shake_strength = 3f;
    private void Update()
    {
        //Reduce the aktivation of the coroutine 
        if (!checkShake)
        {
            StartCoroutine(TrainShake());
        }
    }

    public void Playerspotted()
    {
        //Enemy gets the position on the player

        if (WorkingEnemy.gameObject.active == true)
        {
            WorkingEnemy.agent.destination = player.transform.position;
        }
        if (PassangerEnemy.gameObject.active == true)
        {
            PassangerEnemy.agent.destination = player.transform.position;
        }
    }


    public void IncreaseTrainSound()
    {
        TrainSound.volume = 1f;
    }

    public void NormalTrainSound()
    {
        TrainSound.volume = 0.15f;
    }


    public IEnumerator TrainShake()
    {
        checkShake = true;
        //Debug.Log("Shake?");

        int ShakeChance = Random.Range(0, 5);
        

        //Debug.Log(ShakeChance);

        if (ShakeChance == 4)
        {
            //Debug.Log("Shake!");
            Shake_duration = Random.Range(1, 4);
            Shake_strength = Random.Range(1, 5);

            MakeTrainShakeSound();
            CameraShake.Shake(Shake_duration, Shake_strength);

            yield return new WaitForSeconds(5f);
            checkShake = false;
        }
        yield return new WaitForSeconds(3f);
        checkShake = false;
    }

    public void MakeTrainShakeSound()
    {
        TrainShakeSound.Play();
    }


}
