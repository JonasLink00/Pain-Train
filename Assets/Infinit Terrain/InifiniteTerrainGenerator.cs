using System.Collections.Generic;
using UnityEngine;

public class InifiniteTerrainGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] terrainTiles; // Array off Terrain-Prefabs
    [SerializeField] private Transform player; // Player Referenz
    [SerializeField] private int tileSize = 10; // Size of the Tiles
    [SerializeField] private int tileVisibleInView = 1; // how many times are visible 

    private Vector3 lastPlayerPosition;
    private Dictionary<Vector2, GameObject> activeTiles = new Dictionary<Vector2, GameObject>(); // aktive tiles
    private Queue<GameObject> tilePool = new Queue<GameObject>(); // pool of tiles

    MeshGenerator meshgenerator;

    private void Start()
    {
        lastPlayerPosition = player.position;
        UpdateTerrain();

        meshgenerator = FindObjectOfType<MeshGenerator>();
    }

    
    private void Update()
    {
        if (Vector3.Distance(player.position, lastPlayerPosition) > tileSize)
        {
            lastPlayerPosition = player.position;
            UpdateTerrain();
            
            //meshgenerator.UpdateMesh();

        }
    }

    //void UpdateTerrain()
    //{
    //    int playerTileX = Mathf.FloorToInt(player.position.x / tileSize);
    //    int playerTileZ = Mathf.FloorToInt(player.position.z / tileSize);

    //    for (int x = playerTileX - tileVisibleInView; x <= playerTileX + tileVisibleInView; x++)
    //    {
    //        for (int z = playerTileZ - tileVisibleInView; z <= playerTileZ + tileVisibleInView; z++)
    //        {
    //            Vector2 tilePos = new Vector2(x, z);
    //            if(!terrainDictionary.ContainsKey(tilePos))
    //            {
    //                GameObject tile = Instantiate(terrainTiles[Random.Range(0, terrainTiles.Length)], new Vector3(x * tileSize,0,z * tileSize), Quaternion.identity); 
    //                terrainDictionary[tilePos] = tile;
    //            }
    //        }
    //    }
    //}

    void UpdateTerrain()
    {
        // calculate the grid based on player position 
        int playerTileX = Mathf.FloorToInt(player.position.x / tileSize);
        int playerTileZ = Mathf.FloorToInt(player.position.z / tileSize);

        //marke all tiles as "not used"
        List<Vector2> tilesToRemove = new List<Vector2>();

        foreach (var tile in activeTiles)
        {
            tilesToRemove.Add(tile.Key);
        }

        //make or push tiles in vision

        for (int x = playerTileX - tileVisibleInView; x <= playerTileX + tileVisibleInView; x++)
        {
            for (int z = playerTileZ - tileVisibleInView; z <= playerTileZ + tileVisibleInView; z++)
            {
                Vector2 tilepos = new Vector2(x, z);

                if(activeTiles.ContainsKey(tilepos))
                {
                    //tile is on position, don´t destroy
                    tilesToRemove.Remove(tilepos);
                }
                else
                {
                    //recycle or create tile
                    GameObject tile;
                    if(tilePool.Count > 0)
                    {
                        //recycle tile
                        tile = tilePool.Dequeue();
                        tile.transform.position = new Vector3(x * tileSize, 0, z * tileSize);
                        tile.SetActive(true);
                    }
                    else
                    {
                        //create tile
                        tile = Instantiate(terrainTiles[Random.Range(0, terrainTiles.Length)], new Vector3(x * tileSize, 0, z * tileSize), Quaternion.identity);
                    }

                    activeTiles[tilepos] = tile;
                }
            }

        }

        //remove all tiles out of range 
        foreach(Vector2 pos in tilesToRemove)
        {
            GameObject tileToRemove = activeTiles[pos];
            tileToRemove.SetActive(false); //deaktivate tile
            activeTiles.Remove(pos);
            tilePool.Enqueue(tileToRemove); //put tile in pool
        }
    }
}
