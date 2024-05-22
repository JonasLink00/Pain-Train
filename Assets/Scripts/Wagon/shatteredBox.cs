using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class shatteredBox : MonoBehaviour
{
    [SerializeField] private GameObject box;
    [SerializeField] private GameObject shattered;
    [SerializeField] private float time = 0.3f;
    private shatteredPartList partList;
    private List<MeshRenderer> shatteredRendererList;
    [SerializeField] private float DespawnTime = 5f;
    private bool Despawned;
    private float despawnTimer;

    private void OnTriggerEnter(Collider other)
    {
        despawnTimer = DespawnTime;

        if (other.GetComponent<Rigidbody>() != null)
        {
            BreakTheThing();
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

    public void BreakTheThing()
    {
        shattered.SetActive(true);
        box.SetActive(false);
        GetComponent<BoxCollider>().enabled = false;
        partList = shattered.GetComponent<shatteredPartList>();



        StartCoroutine(Blink(shattered, time));
    }

    IEnumerator Blink(GameObject despawnGO, float time)
    {
        //für jedes Mesh in der List renderer Aktivieren/Deaktivieren



        foreach (MeshRenderer renderer in partList.GetPartList)
        {
            while (!Despawned)
            {
                renderer.enabled = false;
                yield return new WaitForSeconds(time);
                renderer.enabled = true;
            }
        }

        //for (int i = 0; i <= 10; i++)
        //  {
        //      meshRenderer.enabled = false;
        //      yield return new WaitForSeconds(time);
        //      meshRenderer.enabled = true;
        //  }
    }
    

    private void Despawn(GameObject spawnedGO)
    {
        spawnedGO.SetActive(false);

    }
} 
