using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shatteredBox : MonoBehaviour
{
    [Header("Destroy")]
    [SerializeField] private GameObject Object;
    [SerializeField] private GameObject shattered;
    [SerializeField] private bool isBox = true;
    [SerializeField] float contactForce = 2.5f;
    [Header("Despawn")]
    [SerializeField] private float timebeforDespawn = 3f;
    [SerializeField] private int DespawnTime = 5;
    [SerializeField] private float BlinkIntervall = 0.1f;


    private float despawnTimer;

    private shatteredPartList partList;
    

    private void OnTriggerEnter(Collider other)
    {
        despawnTimer = DespawnTime;

        if (other.gameObject.GetComponent<CapsuleCollider>())
        {
            BreakTheThing(other.transform.position);
        }
    }


    public void BreakTheThing(Vector3 position)
    {
       //swapt normal Onject with shattered Parts

        shattered.SetActive(true);
        Object.SetActive(false);
        
        partList = shattered.GetComponent<shatteredPartList>();
        List<MeshRenderer> shatteredList = partList.GetPartList;
       foreach (var part in shatteredList)
       {
            Rigidbody rb = part.GetComponent<Rigidbody>();

            //diffrenceVector = target - origin

            Vector3 diffrenceVector = rb.transform.position - position;
            Vector3 directionVector = diffrenceVector.normalized;

            rb.AddForce(directionVector * contactForce, ForceMode.Impulse);
           
            //Debug.Log("AddForce" + rb.transform.position);

       }

        if(isBox)
        {
             BoxColliderList colliderList;

            colliderList = GetComponent<BoxColliderList>();
            List<BoxCollider> collidersList = colliderList.GetColliderList;

            foreach (var collider in collidersList)
            {
                collider.enabled = false;
            }
        }
        

        StartCoroutine(Blink());
    }

    IEnumerator Blink()
    {
        //für jedes Mesh in der List renderer Aktivieren/Deaktivieren
        yield return new WaitForSeconds(timebeforDespawn);

        for (int i = 0; i < 4; i++)
        {

            ItterateShatterdObjects(false);

            yield return new WaitForSeconds(BlinkIntervall);

            ItterateShatterdObjects(true);

            yield return new WaitForSeconds(BlinkIntervall);

        }

        
        shattered.SetActive(false);

        Destroy(gameObject);
    }


    private void ItterateShatterdObjects(bool SetaktivPerameta)
    {
        foreach (MeshRenderer renderer in partList.GetPartList)
        {
            renderer.enabled = SetaktivPerameta;
        }
    }

} 
