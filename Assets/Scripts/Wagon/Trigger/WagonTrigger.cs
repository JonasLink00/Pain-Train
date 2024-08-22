using UnityEngine;

public class WagonTrigger : MonoBehaviour
{
    public bool GetAttacked = false;

    [SerializeField] private TrainManager TrainManager;

   
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerInput>())
        {
            GetAttacked = true;
            TrainManager.NormalTrainSound();
        }
    }
}
