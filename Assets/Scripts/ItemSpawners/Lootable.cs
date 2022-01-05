using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lootable : MonoBehaviour
{
    [SerializeField] private int numItemsToLoot;


    // Start is called before the first frame update
    void Start()
    {
        GetComponent<NPC>().SetAction(Loot);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Loot()
    {
        RandomItemSpawner itemSpawner =  GetComponent<RandomItemSpawner>();
        for(int i = 0; i < numItemsToLoot; i++)
        {
            itemSpawner.SpawnRandomItem();
        }
        this.GetComponent<NPC>().enabled = false;
        Animator anim = GetComponentInChildren<Animator>();
        if(anim != null)
        {
            anim.SetTrigger("Loot");
        }
    }
}
