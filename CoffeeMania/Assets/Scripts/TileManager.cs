using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public int maxTilesOnScreen; // max number of tiles allowed on screen
    public float tileLength; // length of each tile prefab

    public float laneWidth; // width of each lane
    public int obstaclesOnEachTile;
    //public Vector3 tileOffset; // offset for spawning a tile prefab in different direction
    public List<GameObject> tiles = new List<GameObject>(); // stores the different tile prefabs
    public List<GameObject> obstacles = new List<GameObject>(); // stores the different obstacle prefabs

    private List<Vector3> obstacleSpawnPoints = new List<Vector3>();
    private List<GameObject> tilesOnScreen = new List<GameObject>(); // stores references to tiles spawned in the scene
    private List<GameObject> obstaclesOnScreen = new List<GameObject>();
    private Vector3 tileSpawnPoint = Vector3.zero; // coordinates of the next tile to spawn
    private Vector3 spawnDirection = Vector3.forward; // direction of the next tile to spawn
    private float spawnAngle = 0;
    private float spawnTime = 5;
    private float spawnTimer = 5;
    private int nextTurn;
    private enum TileIndex
    {
        straight = 0, left = 1, right = 2
    }
    private int tileIndex = 0;

    void Start()
    {
        nextTurn = Random.Range(3, 5);

        for (int i = 0; i < maxTilesOnScreen; ++i)
        {
            SpawnTile();
        }

        //print(0 % -90);
    }

    void Update()
    {
        spawnTimer -= Time.deltaTime;

        if (spawnTimer < 0)
        {
            SpawnTile();

            spawnTimer = spawnTime;

            if (tilesOnScreen.Count > maxTilesOnScreen)
            {
                DeleteTile();
            }
        }
    }

    private void SpawnTile()
    {
        // randomise the prefab index of the next tile to spawn
        //int index = Random.Range(0, tilePrefabs.Count);

        GameObject tile = Instantiate(tiles[tileIndex]);
        tilesOnScreen.Add(tile);
        tile.transform.SetParent(this.transform);

        tile.transform.position = tileSpawnPoint;
        tile.transform.Rotate(Vector3.up, spawnAngle);

        tileSpawnPoint += spawnDirection * tileLength;

        nextTurn--;
        if (nextTurn == 0)
        {
            tileIndex = Random.Range(1, 3);

            if (tileIndex == (int)TileIndex.left)
            {
                TurnLeft();
            }
            else if (tileIndex == (int)TileIndex.right)
            {
                TurnRight();
            }

            nextTurn = Random.Range(3, 5);
        }
        else
        {
            tileIndex = (int)TileIndex.straight;
        }

        SpawnObstacles(tile);
    }

    private void DeleteTile()
    {
        Destroy(tilesOnScreen[0]);
        tilesOnScreen.RemoveAt(0);
    }

    private void TurnLeft()
    {
        spawnDirection = Quaternion.AngleAxis(-90, Vector3.up) * spawnDirection;
        spawnAngle -= 90;
        //Debug.Log("Turning left!");
    }

    private void TurnRight()
    {
        spawnDirection = Quaternion.AngleAxis(90, Vector3.up) * spawnDirection;
        spawnAngle += 90;
        //Debug.Log("Turning right!");
    }

    private void SpawnObstacles(GameObject tile)
    {
        float[] intervalX = { 0, laneWidth, -laneWidth };
        float[] intervalZ = { 0, tileLength / 3, -tileLength / 3 };
        List<Vector3> occupiedPoint = new List<Vector3>();

        for (int i = 0; i < obstaclesOnEachTile; ++i)
        {
            int indexX = Random.Range(0, 3);
            int indexZ = Random.Range(0, 3);
            Vector3 obstaclePos = new Vector3(intervalX[indexX], 0, intervalZ[indexZ]);

            if (tile.transform.rotation.y % 180 != 0)
            {
                float temp = obstaclePos.x;
                obstaclePos.x = obstaclePos.z;
                obstaclePos.z = temp;
            }

            if (occupiedPoint.Contains(obstaclePos))
            {
                i--;
            }
            else
            {
                occupiedPoint.Add(obstaclePos);

                GameObject obstacle = Instantiate(obstacles[Random.Range(0, obstacles.Count)]);
                obstacle.transform.position = tile.transform.position;
                obstacle.transform.SetParent(tile.transform);
                obstacle.transform.localPosition = obstaclePos;
                obstacle.transform.rotation = tile.transform.rotation;
            }
        }
    }
}
