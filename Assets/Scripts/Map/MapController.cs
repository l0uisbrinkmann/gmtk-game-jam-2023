using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    [SerializeField] List<GameObject> terrainChunks;
    public GameObject CurrentChunk { get; set; }
    [SerializeField] GameObject player;
    [SerializeField] float checkerRadius;
    [SerializeField] LayerMask terrainMask;
    Vector3 playerLastPosition;

    [Header("Optimization")]
    [SerializeField] List<GameObject> spawnedChunks;
    GameObject latestChunk;
    [SerializeField] float maxOptimizationDistance; //must be greater than the length and width of the tilemap
    float optimizationDistance;
    float optimizerCooldown;
    [SerializeField] float optimizerCooldownDuration;

    // Start is called before the first frame update
    void Start()
    {
        playerLastPosition = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        ChunkChecker();
        optimizerCooldown -= Time.deltaTime;
        if (optimizerCooldown <= 0f)
        {
            ChunkOptimizer();
            optimizerCooldown = optimizerCooldownDuration;
        }
        else
        {
            return;
        }
    }

    void ChunkChecker()
    {
        if (!CurrentChunk)
        {
            return;
        }

        Vector3 moveDirection = player.transform.position - playerLastPosition;
        playerLastPosition = player.transform.position;

        string directionName = getDirectionName(moveDirection);

        if (directionName.Contains("Up"))
        {
            CheckAndSpawnChunk("Up Left");
            CheckAndSpawnChunk("Up");
            CheckAndSpawnChunk("Up Right");
        }
        if (directionName.Contains("Right"))
        {
            CheckAndSpawnChunk("Up Right");
            CheckAndSpawnChunk("Right");
            CheckAndSpawnChunk("Down Right");
        }
        if (directionName.Contains("Down"))
        {
            CheckAndSpawnChunk("Down Right");
            CheckAndSpawnChunk("Down");
            CheckAndSpawnChunk("Down Left");
        }
        if (directionName.Contains("Left"))
        {
            CheckAndSpawnChunk("Down Left");
            CheckAndSpawnChunk("Left");
            CheckAndSpawnChunk("Up Left");
        }
    }

    void CheckAndSpawnChunk(string directionName)
    {
        if(!Physics2D.OverlapCircle(CurrentChunk.transform.Find(directionName).position, checkerRadius, terrainMask))
        {
            SpawnChunk(CurrentChunk.transform.Find(directionName).position);
        }
    }

    string getDirectionName(Vector3 direction)
    {
        direction = direction.normalized;
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.y > 0.5f)
            {
                return direction.x > 0 ? "Up Right" : "Up Left";
            }
            else if (direction.y < -0.5f)
            {
                return direction.x > 0 ? "Down Right" : "Down Left";
            }
            else
            {
                return direction.x > 0 ? "Right" : "Left";
            }
        }
        else
        {
            if (direction.x > 0.5f)
            {
                return direction.y > 0 ? "Up Right" : "Down Right";
            }
            else if (direction.x < -0.5f)
            {
                return direction.y > 0 ? "Up Left" : "Down Left";
            }
            else
            {
                return direction.y > 0 ? "Up" : "Down";
            }
        }
    }

    void SpawnChunk(Vector3 spawnPosition)
    {
        int random = UnityEngine.Random.Range(0, terrainChunks.Count);
        latestChunk = Instantiate(terrainChunks[random], spawnPosition, Quaternion.identity);
        spawnedChunks.Add(latestChunk);
    }

    void ChunkOptimizer()
    {

        foreach (GameObject chunk in spawnedChunks)
        {
            optimizationDistance = Vector3.Distance(player.transform.position, chunk.transform.position);
            if (optimizationDistance > maxOptimizationDistance)
            {
                chunk.SetActive(false);
            }
            else
            {
                chunk.SetActive(true);
            }
        }
    }
}
