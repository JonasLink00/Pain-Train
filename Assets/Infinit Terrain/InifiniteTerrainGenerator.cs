using System.Collections.Generic;
using UnityEngine;

public class InifiniteTerrainGenerator : MonoBehaviour
{
    public GameObject[] terrainTiles;
    public Transform player;
    public int tileSize = 1000;
    public int tileVisibleInView = 4;

    private Vector3 lastPlayerPosition;
    private Dictionary<Vector2, GameObject> terrainDictionary = new Dictionary<Vector2, GameObject>();

    private void Start()
    {
        lastPlayerPosition = player.position;
        UpdateTerrain();

    }

    private void Update()
    {
        if(Vector3.Distance(player.position, lastPlayerPosition) > tileSize)
        {
            lastPlayerPosition = player.position;
            UpdateTerrain();

        }
    }

    void UpdateTerrain()
    {
        int playerTileX = Mathf.FloorToInt(player.position.x / tileSize);
        int playerTileZ = Mathf.FloorToInt(player.position.z / tileSize);

        for (int x = playerTileX - tileVisibleInView; x <= playerTileX + tileVisibleInView; x++)
        {
            for (int z = playerTileZ - tileVisibleInView; z <= playerTileZ + tileVisibleInView; z++)
            {
                Vector2 tilePos = new Vector2(x, z);
                if(!terrainDictionary.ContainsKey(tilePos))
                {
                    GameObject tile = Instantiate(terrainTiles[Random.Range(0, terrainTiles.Length)], new Vector3(x * tileSize,0,z * tileSize), Quaternion.identity); 
                    terrainDictionary[tilePos] = tile;
                }
            }
        }
    }
}
