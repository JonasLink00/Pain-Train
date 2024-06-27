using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BoxColliderList : MonoBehaviour
{
    [SerializeField] List<BoxCollider> colliderList;
    public List<BoxCollider> GetColliderList { get => colliderList; }


    private void Start()
    {
        PopulateList();
    }
    private void PopulateList()
    {
        colliderList = GetComponents<BoxCollider>().ToList<BoxCollider>();
    }
}
