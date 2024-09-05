using System.Collections;
using UnityEngine;

public class TrainManager : MonoBehaviour
{
    [SerializeField] private PlayerInput player;
    [SerializeField] private WagonTrigger trigger;
    [SerializeField] private EnemyController WorkingEnemy;
    [SerializeField] private EnemyController PassangerEnemy;
    [SerializeField] private AudioSource TrainSound;

    [SerializeField] CameraShake camerashake;

    bool checkShake = false;
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
        Debug.Log("Shake?");

        int ShakeChance = Random.Range(0, 5);
        Debug.Log(ShakeChance);

        if (ShakeChance == 4)
        {
            Debug.Log("Shake!");
            CameraShake.Shake(2f, 1f);
            yield return new WaitForSeconds(5f);
            checkShake = false;
        }
        yield return new WaitForSeconds(1f);
        checkShake = false;
    }
    
}
