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
    Vector4 noTerrainPosition;
    [SerializeField] LayerMask terrainMask;
    PlayerMovement playerMovement;

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
        playerMovement = FindObjectOfType<PlayerMovement>();
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
        Console.WriteLine("ChunkChecker()");
        if (!CurrentChunk)
        {
            return;
        }
        if (playerMovement.MoveDirection.x > 0 && playerMovement.MoveDirection.y == 0) //right
        {
            if (!Physics2D.OverlapCircle(CurrentChunk.transform.Find("Right").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = CurrentChunk.transform.Find("Right").position;
                SpawnChunk();
            }
        }
        if (playerMovement.MoveDirection.x < 0 && playerMovement.MoveDirection.y == 0) //left
        {
            if (!Physics2D.OverlapCircle(CurrentChunk.transform.Find("Left").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = CurrentChunk.transform.Find("Left").position;
                SpawnChunk();
            }
        }
        if (playerMovement.MoveDirection.x == 0 && playerMovement.MoveDirection.y > 0) //up
        {
            if (!Physics2D.OverlapCircle(CurrentChunk.transform.Find("Up").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = CurrentChunk.transform.Find("Up").position;
                SpawnChunk();
            }
        }
        if (playerMovement.MoveDirection.x == 0 && playerMovement.MoveDirection.y < 0) //down
        {
            if (!Physics2D.OverlapCircle(CurrentChunk.transform.Find("Down").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = CurrentChunk.transform.Find("Down").position;
                SpawnChunk();
            }
        }
        if (playerMovement.MoveDirection.x > 0 && playerMovement.MoveDirection.y > 0) //up right
        {
            if (!Physics2D.OverlapCircle(CurrentChunk.transform.Find("Up Right").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = CurrentChunk.transform.Find("Up Right").position;
                SpawnChunk();
            }
        }
        if (playerMovement.MoveDirection.x < 0 && playerMovement.MoveDirection.y > 0) //up left
        {
            if (!Physics2D.OverlapCircle(CurrentChunk.transform.Find("Up Left").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = CurrentChunk.transform.Find("Up Left").position;
                SpawnChunk();
            }
        }
        if (playerMovement.MoveDirection.x > 0 && playerMovement.MoveDirection.y < 0) //down right
        {
            if (!Physics2D.OverlapCircle(CurrentChunk.transform.Find("Down Right").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = CurrentChunk.transform.Find("Down Right").position;
                SpawnChunk();
            }
        }
        if (playerMovement.MoveDirection.x < 0 && playerMovement.MoveDirection.y < 0) //down left
        {
            if (!Physics2D.OverlapCircle(CurrentChunk.transform.Find("Down Left").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = CurrentChunk.transform.Find("Down Left").position;
                SpawnChunk();
            }
        }
    }

    void SpawnChunk()
    {
        int random = UnityEngine.Random.Range(0, terrainChunks.Count);
        latestChunk = Instantiate(terrainChunks[random], noTerrainPosition, Quaternion.identity);
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
