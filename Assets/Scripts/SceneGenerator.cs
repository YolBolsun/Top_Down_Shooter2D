using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneGenerator : MonoBehaviour
{
    [SerializeField] private List<GameObject> tilePrefabs;
    [SerializeField] private Transform generationStart;
    [SerializeField] private int totalTiles;
    [SerializeField] private GameObject StartTile;
    [SerializeField] private GameObject blockerTile;

    private Dictionary<int, List<int>> usedLocations = new Dictionary<int, List<int>>();
    private Dictionary<Tuple<int, int>, GameObject> placedTiles = new Dictionary<Tuple<int, int>, GameObject>();
    //SceneTile, sides to spawn on
    private List<Tuple<GameObject, List<int>>> spawnLocations = new List<Tuple<GameObject, List<int>>>();

    // Start is called before the first frame update
    void Start()
    {

        GameObject created = GameObject.Instantiate(StartTile, generationStart);
        created.transform.Translate(new Vector3(16f, 16f));
        SceneTile createdTile = created.GetComponent<SceneTile>();
        createdTile.gridX = 0;
        createdTile.gridY = 0;
        usedLocations[0] = new List<int>();
        usedLocations[0].Add(0);
        placedTiles[new Tuple<int, int>(0, 0)] = created;
        AddRemainingSides(created, -1);
        SpawnRemainingTiles();
        FillRemainingTiles();

        /*foreach (GameObject tile in tilePrefabs)
        {
            foreach (GameObject tiley in tilePrefabs)
            {
                Debug.Log(tile.name + " matches left with " + tiley.name + " " + tile.GetComponent<SceneTile>().MatchesToTheLeft(tiley.GetComponent<SceneTile>()));
                Debug.Log(tile.name + " matches right with " + tiley.name + " " + tile.GetComponent<SceneTile>().MatchesToTheRight(tiley.GetComponent<SceneTile>()));
                Debug.Log(tile.name + " matches top with " + tiley.name + " " + tile.GetComponent<SceneTile>().MatchesToTheTop(tiley.GetComponent<SceneTile>()));
                Debug.Log(tile.name + " matches bottom with " + tiley.name + " " + tile.GetComponent<SceneTile>().MatchesToTheBottom(tiley.GetComponent<SceneTile>()));
            }
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AddRemainingSides(GameObject tileAdded, int usedSide)
    {
        List<int> sidesRemaining = tileAdded.GetComponent<SceneTile>().sides;
        sidesRemaining.Remove(usedSide);
        spawnLocations.Add(new Tuple<GameObject, List<int>>(tileAdded, sidesRemaining));
    }

    void SpawnRemainingTiles()
    {
        int totalTryCount = 1000;
        while (totalTiles > 1 && totalTryCount > 0)
        {
            totalTryCount--;
            if (SpawnRandomTile())
            {
                totalTiles--;
            }
        }
    }

    bool SpawnRandomTile()
    {
        int spawnLocation = UnityEngine.Random.Range(0, spawnLocations.Count);
        SceneTile adjacentObjectTile = spawnLocations[spawnLocation].Item1.GetComponent<SceneTile>();
        if (spawnLocations[spawnLocation].Item2.Contains(1))
        {
            return TrySpawn(spawnLocation, adjacentObjectTile, 1, 0, 1);
        }
        else if (spawnLocations[spawnLocation].Item2.Contains(2))
        {
            return TrySpawn(spawnLocation, adjacentObjectTile, 0, 1, 2);

        }
        else if (spawnLocations[spawnLocation].Item2.Contains(0))
        {
            return TrySpawn(spawnLocation, adjacentObjectTile, -1, 0, 0);
        }
        else if (spawnLocations[spawnLocation].Item2.Contains(3))
        {
            return TrySpawn(spawnLocation, adjacentObjectTile, 0, -1, 3);
        }
        return false;

    }
    bool TrySpawn(int spawnLocation, SceneTile adjacentObjectTile, int xDir, int yDir, int side)
    {
        spawnLocations[spawnLocation].Item2.Remove(side);
        if (!usedLocations.ContainsKey(adjacentObjectTile.gridX + xDir) || !usedLocations[adjacentObjectTile.gridX].Contains(adjacentObjectTile.gridY + yDir))
        {
            if (!usedLocations.ContainsKey(adjacentObjectTile.gridX + xDir))
            {
                usedLocations[adjacentObjectTile.gridX + xDir] = new List<int>();
            }
            usedLocations[adjacentObjectTile.gridX + xDir].Add(adjacentObjectTile.gridY + yDir);
            GameObject toSpawn = GetRandomMatchingTile(adjacentObjectTile.gridX + xDir, adjacentObjectTile.gridY + yDir);
            GameObject spawnedObject = GameObject.Instantiate(toSpawn, spawnLocations[spawnLocation].Item1.transform);
            spawnedObject.transform.Translate(new Vector3(xDir * 32, yDir * 32));
            SceneTile createdTile = spawnedObject.GetComponent<SceneTile>();
            //check usedLocations before spawning, add to that createdTile gridx/gridy
            createdTile.gridX = adjacentObjectTile.gridX + xDir;
            createdTile.gridY = adjacentObjectTile.gridY + yDir;
            AddRemainingSides(spawnedObject, (side+2)%4);
            placedTiles[new Tuple<int, int>(createdTile.gridX, createdTile.gridY)] = spawnedObject;
            return true;
        }
        return false;
    }

    GameObject GetRandomMatchingTile(int x, int y)
    {
        List<GameObject> tilesThatMatch = new List<GameObject>();
        foreach(GameObject tile in tilePrefabs)
        {
            bool matches = true;
            if(placedTiles.ContainsKey(new Tuple<int, int>(x + 1, y)) && !placedTiles[new Tuple<int, int>(x+1, y)].GetComponent<SceneTile>().MatchesToTheLeft(tile.GetComponent<SceneTile>()))
            {
                matches = false;
            }
            if (matches  && placedTiles.ContainsKey(new Tuple<int, int>(x - 1, y)) && !placedTiles[new Tuple<int, int>(x - 1, y)].GetComponent<SceneTile>().MatchesToTheRight(tile.GetComponent<SceneTile>()))
            {
                matches = false;
            }
            if (matches && placedTiles.ContainsKey(new Tuple<int, int>(x, y +1)) && !placedTiles[new Tuple<int, int>(x, y+1)].GetComponent<SceneTile>().MatchesToTheBottom(tile.GetComponent<SceneTile>()))
            {
                matches = false;
            }
            if (matches && placedTiles.ContainsKey(new Tuple<int, int>(x, y - 1)) && !placedTiles[new Tuple<int, int>(x, y-1)].GetComponent<SceneTile>().MatchesToTheTop(tile.GetComponent<SceneTile>()))
            {
                matches = false;
            }
            if (matches)
            {
                tilesThatMatch.Add(tile);
            }
        }
        //Debug.Log(tilesThatMatch.Count);
        if (tilesThatMatch.Count > 0)
        {
            return tilesThatMatch[UnityEngine.Random.Range(0, tilesThatMatch.Count)];
        }
        else
        {
            return StartTile;
        }
    }

    void FillRemainingTiles()
    {
        for(int i = -5; i < 5; i++)
        {
            for(int j = -5; j < 5; j++)
            {
                
                if(!placedTiles.ContainsKey((new Tuple<int, int>(i, j))))
                {
                    GameObject spawnedObject = GameObject.Instantiate(blockerTile, generationStart);
                    spawnedObject.transform.Translate(new Vector3(32f * i + 16f, 32f * j + 16f));
                    

                }
            }
        }
    }
}
