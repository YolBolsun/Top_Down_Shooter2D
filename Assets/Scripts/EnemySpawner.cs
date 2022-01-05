using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform bottomLeftSpawn;
    [SerializeField] private Transform TopRightSpawn;
    [SerializeField] private List<GameObject> enemies;
    [SerializeField] private float timeBetweenSpawns;

    private float timeOfLastSpawn = 0;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.realtimeSinceStartup > timeOfLastSpawn + timeBetweenSpawns)
        {
            SpawnUnit();
            timeOfLastSpawn = Time.realtimeSinceStartup;
        }
    }

    void SpawnUnit()
    {
        Vector3 spawnLocation = new Vector3(
            Random.Range(bottomLeftSpawn.position.x, TopRightSpawn.position.x),
            Random.Range(bottomLeftSpawn.position.y, TopRightSpawn.position.y),
            0);
        Collider2D coll = Physics2D.OverlapArea(spawnLocation + Vector3.left + Vector3.down, spawnLocation - Vector3.left - Vector3.down);
        //try again if it would spawn in something
        if(coll != null)
        {
            SpawnUnit();
            return;
        }
        //try again if it would be too close to player
        if((spawnLocation - player.transform.position).magnitude < 20)
        {
            SpawnUnit();
            return;
        }


        GameObject enemyToSpawn = enemies[Mathf.FloorToInt(Random.Range(0, enemies.Count))];

        GameObject.Instantiate(enemyToSpawn, spawnLocation, Quaternion.identity);
    }
}
