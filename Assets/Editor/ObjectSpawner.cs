using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class ObjectSpawner : EditorWindow
{
    private Vector2 _position;

    private List<GameObject> ObjectToSpawnList = new();

    private List<GameObject> FreightPositionList = new();

    private List<GameObject> PlacedFreightList = new();

    private string spawnChance;

    [MenuItem("Tools/ObjectSpawner")]

    public static void ShowWindow()
    {
        GetWindow(typeof(ObjectSpawner));
    }

    //Controller
    private void OnGUI()
    {
        //Get´s Prefabs out of Resources ordner

        GUILayout.Label("List of Freight", EditorStyles.boldLabel);

        EditorGUILayout.BeginScrollView(_position);
        LoadAllObjects();


        for(int i = 0; i < ObjectToSpawnList.Count; i++)
        {
            ObjectToSpawnList[i] = (GameObject)EditorGUILayout.ObjectField(ObjectToSpawnList[i], typeof(GameObject), false);
        }


        //Get´s Prefabs out of Resources ordner

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

    //Model

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


        GameObject[] FrPos = GameObject.FindGameObjectsWithTag("FreightPosition");

        foreach (var item in FrPos)
        {
            FreightPositionList.Add(item);
        }
    }

    //View
    private void PlaceFreightRandom()
    {
        RemovePlacedFreight();

        //spawn 1 of 6 Freights on position 
        //has a 25% of not spawn anything 
        
        for (int i = 0; i < FreightPositionList.Count; i++)
        {
            int Coin_Toss = Random.Range(0, 100);

            if (Coin_Toss <= 75)
            {
                int ranFreinum = Random.Range(0, ObjectToSpawnList.Count);


                GameObject spawnPosition = FreightPositionList[i];

                GameObject randomFreight = ObjectToSpawnList[ranFreinum];

                GameObject placedFreight = Instantiate(randomFreight, spawnPosition.transform.position, Quaternion.identity);

                PlacedFreightList.Add(placedFreight);
            }

            //Debug.Log(Coin_Toss);
            
        }
        
    }

    private void RemovePlacedFreight()
    {
        //Destroys Object in List

        if(PlacedFreightList.Count != 0)
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
