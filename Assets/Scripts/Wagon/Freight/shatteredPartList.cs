using System.Collections.Generic;
using System.Linq;
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
        partList = GetComponentsInChildren<MeshRenderer>().ToList<MeshRenderer>();
        gameObject.SetActive(false);
    }
}
