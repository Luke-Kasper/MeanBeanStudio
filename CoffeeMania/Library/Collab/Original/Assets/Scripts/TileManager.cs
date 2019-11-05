using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public int maxTilesOnScreen; // max number of tiles allowed on screen
    public float tileLength; // length of each tile prefab

    public float laneWidth; // width of each lane
    //public Vector3 tileOffset; // offset for spawning a tile prefab in different direction
    public List<GameObject> tiles = new List<GameObject>(); // stores the different tile prefabs
    public List<GameObject> obstacles = new List<GameObject>(); // stores the different obstacle prefabs

    private List<GameObject> tilesOnScreen = new List<GameObject>(); // stores references to tiles spawned in the scene
    private Vector3 spawnPoint = Vector3.zero; // coordinates of the next tile to spawn
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

        Vector3 size = new Vector3 ( 50, 0, 30 );
        print(Vector3.Dot(size, Vector3.left));
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

        tile.transform.position = spawnPoint;
        tile.transform.Rotate(Vector3.up, spawnAngle);

        spawnPoint += spawnDirection * tileLength;

        SpawnObstacle(tile);

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

    private void SpawnObstacle(GameObject tile)
    {
        float[] intervals = { 0, laneWidth, -laneWidth };
        int indexX = Random.Range(0, 3);
        int indexZ = Random.Range(0, 3);

        Vector3 obstaclePos = new Vector3(intervals[indexX], -0.2f, intervals[indexZ]);

        GameObject obstacle = Instantiate(obstacles[Random.Range(0, obstacles.Count)]);
        obstacle.transform.position = tile.transform.position;
        obstacle.transform.SetParent(tile.transform);
        obstacle.transform.Translate(obstaclePos);
    }
}
