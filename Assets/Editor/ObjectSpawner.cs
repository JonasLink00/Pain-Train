using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Web;
using System.Linq;

public class ObjectSpawner : EditorWindow
{
    private Vector2 _position;

    private List<GameObject> ObjectToSpawnList = new();

    private List<GameObject> FreightPositionList = new();

    private List<GameObject> PlacedFreightList = new();

    [MenuItem("Tools/ObjectSpawner")]

    public static void ShowWindow()
    {
        GetWindow(typeof(ObjectSpawner));
    }

    private void OnGUI()
    {
        //ObjectToSpawn = EditorGUILayout.ObjectField("Freight", ObjectToSpawn, typeof(GameObject), false) as GameObject;


        GUILayout.Label("List of Freight", EditorStyles.boldLabel);

        EditorGUILayout.BeginScrollView(_position);
        LoadAllObjects();


        for(int i = 0; i < ObjectToSpawnList.Count; i++)
        {
            ObjectToSpawnList[i] = (GameObject)EditorGUILayout.ObjectField(ObjectToSpawnList[i], typeof(GameObject), false);
        }

        


        GUILayout.Label("List of Positions", EditorStyles.boldLabel);

        LoadAllPositions();


        for (int i = 0; i < FreightPositionList.Count; i++)
        {
            FreightPositionList[i] = (GameObject)EditorGUILayout.ObjectField(FreightPositionList[i], typeof(GameObject), false);
        }

        EditorGUILayout.EndScrollView();


        if(GUILayout.Button("Place"))
        {
            PlaceFreightRandom();
        }

        if(GUILayout.Button("Destroy"))
        {
            RemovePlacedFreight();
        }

        
    }

    

    private void LoadAllObjects()
    {
        ObjectToSpawnList.Clear();

        GameObject[] objSP = Resources.LoadAll<GameObject>("Box_Prefabs");

        foreach (var item in objSP)
        {
            ObjectToSpawnList.Add(item);
        }

        

    }

    private void LoadAllPositions()
    {
        FreightPositionList.Clear();

        //GameObject[] FrPos = Resources.LoadAll<GameObject>("FreightPositions");

        GameObject[] FrPos = GameObject.FindGameObjectsWithTag("FreightPosition");

        foreach (var item in FrPos)
        {
            FreightPositionList.Add(item);
        }
    }


    private void PlaceFreightRandom()
    {
        RemovePlacedFreight();

        
            for (int i = 0; i < FreightPositionList.Count; i++)
            {

                int ranFreinum = Random.Range(0, ObjectToSpawnList.Count);


                GameObject spawnPosition = FreightPositionList[i];

                GameObject randomFreight = ObjectToSpawnList[ranFreinum];

                GameObject placedFreight = Instantiate(randomFreight, spawnPosition.transform.position, Quaternion.identity);

                PlacedFreightList.Add(placedFreight);

            }
        
        



    }

    private void RemovePlacedFreight()
    {
        if(PlacedFreightList != null)
        {
            for(int i = 0;i < PlacedFreightList.Count;i++)
            {
                DestroyImmediate(PlacedFreightList[i]);
            }
            PlacedFreightList.Clear();
        }
        else
        {
            Debug.Log("Nothing to Distroy");
        }
    }
    
}
