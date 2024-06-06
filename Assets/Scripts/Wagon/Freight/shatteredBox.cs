using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;

public class shatteredBox : MonoBehaviour
{

    [SerializeField] private GameObject box;
    [SerializeField] private GameObject shattered;
<<<<<<< HEAD
    [SerializeField] private float time = 0.3f;
=======
    [SerializeField] float contactForce = 10;
    [Header("Despawn")]
    [SerializeField] private float timebeforDespawn = 3f;
    [SerializeField] private int DespawnTime = 4;
    [SerializeField] private float BlinkIntervall = 4;


    private float despawnTimer;
    private bool Despawned;

>>>>>>> inarbeit
    private shatteredPartList partList;
    private List<MeshRenderer> shatteredRendererList;
    [SerializeField] private float DespawnTime = 5f;
    private bool Despawned;
    private float despawnTimer;
    [SerializeField]
    float contactForce = 10;

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
       foreach(var part in List)
       {
            Rigidbody rb =part.GetComponent<Rigidbody>();

            //diffrenceVector = target - origin

            Vector3 diffrenceVector = rb.transform.position - position;
            Vector3 directionVector = diffrenceVector.normalized;

            rb.AddForce(directionVector * contactForce, ForceMode.Impulse);
            //rb.velocity = directionVector * contactForce;
            //Debug.Log("AddForce" + rb.transform.position);

       }

<<<<<<< HEAD

=======
>>>>>>> inarbeit
        StartCoroutine(Blink());
    }

    IEnumerator Blink()
    {
        yield return new WaitForSeconds(1f);
        //für jedes Mesh in der List renderer Aktivieren/Deaktivieren
        yield return new WaitForSeconds(timebeforDespawn);

<<<<<<< HEAD
        for (int i = 0; i < 5; i++)
        {
            HandelRenderaktivation(false);


            yield return new WaitForSeconds(0.2f);

            HandelRenderaktivation(true);

            yield return new WaitForSeconds(0.2f);
        }

            HandelRenderaktivation(false);



        //for (int i = 0; i <= 10; i++)
        //  {
        //      meshRenderer.enabled = false;
        //      yield return new WaitForSeconds(time);
        //      meshRenderer.enabled = true;
        //  }
    }



    private void HandelRenderaktivation(bool aktivParameter)
    {
        foreach (MeshRenderer renderer in partList.GetPartList)
        {
            renderer.enabled = aktivParameter;
        }

=======
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
>>>>>>> inarbeit
    }
} 
