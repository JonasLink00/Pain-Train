using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;

public class shatteredBox : MonoBehaviour
{
    [Header("Destroy")]
    [SerializeField] private GameObject box;
    [SerializeField] private GameObject shattered;
    [SerializeField] float contactForce = 2.5f;
    [Header("Despawn")]
    [SerializeField] private float timebeforDespawn = 3f;
    [SerializeField] private int DespawnTime = 5;
    [SerializeField] private float BlinkIntervall = 0.1f;


    private float despawnTimer;
    private bool Despawned;

    private shatteredPartList partList;

    private void OnTriggerEnter(Collider other)
    {
        despawnTimer = DespawnTime;

        if (other.gameObject.GetComponent<PlayerInput>())
        {
            BreakTheThing(other.transform.position);
        }
    }

   
    private void Update()
    {
        if (shattered.activeSelf)
        {
            despawnTimer -= Time.deltaTime;

            if (despawnTimer <= 0)
            {
                despawnTimer = 0;
                Despawned = true;
                //Despawn(shattered);
                shattered.SetActive(false);
            }
        }
    }

    public void BreakTheThing(Vector3 position)
    {
       
        shattered.SetActive(true);
        box.SetActive(false);
        GetComponent<BoxCollider>().enabled = false;
        partList = shattered.GetComponent<shatteredPartList>();
        List<MeshRenderer> List = partList.GetPartList;
        foreach (var part in List)
       {
            Rigidbody rb =part.GetComponent<Rigidbody>();

            //diffrenceVector = target - origin

            Vector3 diffrenceVector = rb.transform.position - position;
            Vector3 directionVector = diffrenceVector.normalized;

            rb.AddForce(directionVector * contactForce, ForceMode.Impulse);
            //rb.velocity = directionVector * contactForce;
            //Debug.Log("AddForce" + rb.transform.position);

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

        //ItterateShatterdObjects(false);
        shattered.SetActive(false);

    }


    private void ItterateShatterdObjects(bool SetaktivPerameta)
    {
        foreach (MeshRenderer renderer in partList.GetPartList)
        {
            renderer.enabled = SetaktivPerameta;
        }
    }
} 
