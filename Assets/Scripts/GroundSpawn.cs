using UnityEngine;
using System.Collections.Generic;
using System.Linq;


public class GroundSpawn : MonoBehaviour
{
    public GameObject groundTilePrefab;
    public GameObject coinPrefab;
    public Transform player;

    public GameObject obstaclePrefab;

    public float tileLength = 10f;
    public int tilesOnScreen = 5;
    public float laneDistance = 3f;

    private float spawnZ = 0f;
    
    private int tilesSpawned = 0;
    public int obstacleStartDelay = 5; // number of safe tiles

    void Start()
    {
        for (int i = 0; i < tilesOnScreen; i++)
        {
            SpawnTile();
        }
    }

    void Update()
    {
        if (player.position.z + (tilesOnScreen * 3 * tileLength) > spawnZ)
        {
            SpawnTile();
        }
    }

    void SpawnTile()
    {
        Vector3 tilePos = new Vector3(0, 0, spawnZ);
        
        // Instantiate and tag the ground tile
        GameObject tile = Instantiate(groundTilePrefab, tilePos, Quaternion.identity);
        tile.tag = "Ground";

        float[] laneX = { -laneDistance, 0f, laneDistance };
        List<int> usedLanes = new List<int>();

        // Only spawn obstacles after delay
        if (tilesSpawned >= obstacleStartDelay)
        {
            float roll = Random.value;

            if (roll < 0.4f)
            {
                // 1 lane blocked
                int lane = Random.Range(0, laneX.Length);
                usedLanes.Add(lane);
            }
            else if (roll < 0.6f)
            {
                // 2 lanes blocked
                int first = Random.Range(0, laneX.Length);
                int second;
                do
                {
                    second = Random.Range(0, laneX.Length);
                } while (second == first);
                usedLanes.Add(first);
                usedLanes.Add(second);
            }
            else if (roll < 0.65f)
            {
                // All 3 lanes blocked
                usedLanes.AddRange(new int[] { 0, 1, 2 });
            }

            // Spawn obstacles
            float obstacleZ = spawnZ + Random.Range(3f, tileLength - 2f);
            foreach (int laneIndex in usedLanes)
            {
                float x = laneX[laneIndex];
                Vector3 pos = new Vector3(x, 0.5f, obstacleZ);
                Instantiate(obstaclePrefab, pos, Quaternion.identity);
            }
        }

        // Spawn a coin in any unblocked lane (or any lane if no obstacles)
        List<int> availableLanes = Enumerable.Range(0, laneX.Length).Except(usedLanes).ToList();
        if (availableLanes.Count > 0)
        {
            int coinLane = availableLanes[Random.Range(0, availableLanes.Count)];
            float coinX = laneX[coinLane];
            float coinZ = Random.Range(2f, tileLength - 2f);
            Vector3 coinPos = tilePos + new Vector3(coinX, 0.5f, coinZ);
            Instantiate(coinPrefab, coinPos, Quaternion.identity);
        }

        tilesSpawned++;
        spawnZ += tileLength;
    }

    
}