using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shatteredPartList : MonoBehaviour
{
    [SerializeField] List<MeshRenderer> partList;

    public List<MeshRenderer> GetPartList { get => partList; }

    private void Start()
    {
        PopulateList();
    }

    private void PopulateList()
    {
        int childCount = gameObject.transform.childCount;

        for (int i = 0; i < childCount; i++)
        {
            if (gameObject.transform.GetComponentInChildren<MeshRenderer>())
            {
                partList.Add(gameObject.transform.GetComponentInChildren<MeshRenderer>());
            }
        }

        gameObject.SetActive(false);
    }
}
