using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_TileManager : MonoBehaviour
{
    public GameObject tilePrefab;

    private Transform playerTransform;
    private float spawnZ = 0.0f;
    private float tileLength = 10.0f;
    private int maxTilesOnScreen = 3;

    private float Zspawn = 0;
    private float Xspawn = 0;

    private int counter = 0;

    private List<GameObject> tilesOnScreen = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.Find("Player").transform;

        //tileLength = tilePrefab.transfor
        for (int i = 0; i < maxTilesOnScreen; ++i)
        {
            SpawnTile();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform.position.z > (Zspawn - maxTilesOnScreen * tileLength))
        {
            SpawnTile();
        }
        //else
        //if (playerTransform.position.x > (Xspawn - maxTilesOnScreen * tileLength))
        //{
        //    SpawnTile();
        //}

        if (tilesOnScreen.Count > maxTilesOnScreen + 1)
        {
            DeleteTile();
        }
    }

    private void SpawnTile()
    {
        if (counter == 7)
        {

            GameObject tile = Instantiate(tilePrefab);
            tile.transform.SetParent(transform);
            tile.transform.position = new Vector3(Xspawn, 0, Zspawn);
            Xspawn += tileLength;

            tilesOnScreen.Add(tile);

            Debug.Log(tilesOnScreen.Count);

            counter=0;
        }
        else
        {
            GameObject tile = Instantiate(tilePrefab);
            tile.transform.SetParent(transform);
            tile.transform.position = new Vector3(Xspawn,0,Zspawn);
            Zspawn += tileLength;

            tilesOnScreen.Add(tile);

            Debug.Log(tilesOnScreen.Count);


            counter++;
        }

    }

    private void DeleteTile()
    {
        Destroy(tilesOnScreen[0]);
        tilesOnScreen.RemoveAt(0);
    }
}
