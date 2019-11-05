using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapScript : MonoBehaviour
{
    public Vector3 tileDimension;
    public float laneWidth;
    public int maxTiles;
    public int obstaclesPerTile;
    public int rowsOfObstacles;
    //public int pickupsPerTile;
    public int turnCDMin;
    public int turnCDMax;
    public GameObject trigger;
    public List<GameObject> straightTileSet = new List<GameObject>();
    public List<GameObject> leftTurnTileSet = new List<GameObject>();
    public List<GameObject> rightTurnTileSet = new List<GameObject>();
    public List<GameObject> obstacleSet = new List<GameObject>();

    public float spawnTime;
    private float spawnTimer;

    private GameObject tileToSpawn;
    private Vector3 spawnPoint;
    private Vector3 spawnDirection;
    private float spawnAngle;
    private int turnCD;
    private List<GameObject> spawnedTiles = new List<GameObject>();
    private int currentTileIndex;

    // Start is called before the first frame update
    void Start()
    {
        ResetMap();

        spawnTimer = spawnTime;
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer -= Time.deltaTime;

        if (spawnTimer < 0)
        {
            SpawnTile();
            spawnTimer = spawnTime;
        }
    }

    public void ResetMap()
    {
        tileToSpawn = straightTileSet[0];
        spawnPoint = Vector3.zero;
        spawnDirection = Vector3.forward;
        spawnAngle = 0;
        turnCD = Random.Range(turnCDMin, turnCDMax + 1);
        currentTileIndex = 0;

        for (int i = 0; i < maxTiles; ++i)
        {
            SpawnTile();
        }
    }

    public void SpawnTile()
    {
        tileToSpawn = Instantiate(tileToSpawn);
        tileToSpawn.transform.SetParent(this.transform);
        spawnedTiles.Add(tileToSpawn);

        if (spawnedTiles.Count > maxTiles)
        {
            DeleteTile();
        }

        tileToSpawn.transform.position = spawnPoint;
        tileToSpawn.transform.Rotate(Vector3.up, spawnAngle);

        float tileLength = Mathf.Abs(Vector3.Dot(tileDimension, spawnDirection));
        spawnPoint += spawnDirection * tileLength;
        turnCD--;

        SpawnObstacles(tileToSpawn);

        if (turnCD == 0)
        {
            int turnType = Random.Range(0, 2);

            if (turnType == 0)
            {
                int tileIndex = Random.Range(0, leftTurnTileSet.Count);
                tileToSpawn = leftTurnTileSet[tileIndex];
                spawnDirection = Quaternion.AngleAxis(-90, Vector3.up) * spawnDirection;
                spawnAngle -= 90;
            }
            else if (turnType == 1)
            {
                int tileIndex = Random.Range(0, rightTurnTileSet.Count);
                tileToSpawn = rightTurnTileSet[tileIndex];
                spawnDirection = Quaternion.AngleAxis(90, Vector3.up) * spawnDirection;
                spawnAngle += 90;
            }

            turnCD = Random.Range(turnCDMin, turnCDMax + 1);
        }
        else
        {
            int tileIndex = Random.Range(0, straightTileSet.Count);
            tileToSpawn = straightTileSet[tileIndex];
        }
    }

    public void DeleteTile()
    {
        Destroy(spawnedTiles[0]);
        spawnedTiles.RemoveAt(0);
    }

    private void SpawnObstacles(GameObject tile)
    {
        float tileLength = tileDimension.z;
        float startOfTile = -tileLength / 2;
        float endOfTile = tileLength / 2;
        float rowInterval = tileLength / rowsOfObstacles;
        float currentRowPos = 0;
        float obstacleSpawnHeight = 0;
        List<Vector3> obstacleSpawnPoints = new List<Vector3>();

        while (currentRowPos - rowInterval >= startOfTile)
        {
            currentRowPos -= rowInterval;
        }

        for (; currentRowPos < endOfTile; currentRowPos += rowInterval)
        {
            obstacleSpawnPoints.Add(new Vector3(-laneWidth, obstacleSpawnHeight, currentRowPos));
            obstacleSpawnPoints.Add(new Vector3(0, obstacleSpawnHeight, currentRowPos));
            obstacleSpawnPoints.Add(new Vector3(laneWidth, obstacleSpawnHeight, currentRowPos));
        }

        for (int i = 0; i < obstaclesPerTile; ++i)
        {
            int positionIndex = Random.Range(0, obstacleSpawnPoints.Count);
            Vector3 obstaclePos = obstacleSpawnPoints[positionIndex];
            obstacleSpawnPoints.RemoveAt(positionIndex);

            GameObject obstacle = Instantiate(obstacleSet[Random.Range(0, obstacleSet.Count)]);
            obstacle.transform.position = tile.transform.position;
            obstacle.transform.SetParent(tile.transform);
            obstacle.transform.localPosition = obstaclePos;
            obstacle.transform.rotation = tile.transform.rotation;
        }
    }

    private void SpawnTrigger(GameObject tile)
    {
        GameObject a_trigger = Instantiate(trigger);
        a_trigger.transform.SetParent(tile.transform);

    }
}