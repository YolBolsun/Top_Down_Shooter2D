using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBehaviour : MonoBehaviour
{
    [SerializeField] int numItemsToSpawn;
    [SerializeField] int randomAdditionaItemsToSpawn;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Died()
    {
        EnemyThrowerScript enemyThrower = GetComponent<EnemyThrowerScript>();
        if (enemyThrower!=null)
        {
            enemyThrower.enabled = false;
        }

        numItemsToSpawn += Random.Range(0, randomAdditionaItemsToSpawn+1);
        for(int i = 0; i < numItemsToSpawn; i++)
        {
            GetComponent<RandomItemSpawner>().SpawnRandomItem();
        }
    }
}
