using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CodeMonkey.Utils;

public class RandomItemSpawner : MonoBehaviour
{
    float timeOfLastSpawn = 0;
    float timeBetweenSpawns = 3;
    [SerializeField] private bool ShouldSpawnItemsAutomatically = false;
    [SerializeField] private int rarityMultiplier = 1;


    [SerializeField] private bool SpawnSingleItemAtStart = false;

    [SerializeField] private Item.Rarity rarity = Item.Rarity.NoRarity;
    [SerializeField] private Item.ItemType itemType = Item.ItemType.Other;
    [SerializeField] private int armor = -1;
    [SerializeField] private int attackDamage = -1;
    [SerializeField] private int attackSpeed = -1;
    [SerializeField] private int movementSpeed = -1;
    [SerializeField] private int health = -1;

    // Start is called before the first frame update
    void Start()
    {
        if(SpawnSingleItemAtStart)
        {
            SpawnRandomItem();
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (ShouldSpawnItemsAutomatically)
        {
            if (Time.realtimeSinceStartup > timeOfLastSpawn + timeBetweenSpawns)
            {
                timeOfLastSpawn = Time.realtimeSinceStartup;
                SpawnRandomItem();
            }
        }
    }

    public void SpawnRandomItem()
    {
        Item toSpawn = new Item();
        toSpawn.itemType = itemType == Item.ItemType.Other ? (Item.ItemType)Random.Range(0, 7) : itemType;
        toSpawn.amount = 1;
        toSpawn.RarityValue = rarity == Item.Rarity.NoRarity ? GetRarity(rarityMultiplier, true) : rarity;
        toSpawn.equippableBoost.armorModifier = armor == -1 ? getArmorModifier((int)toSpawn.RarityValue + 1) : armor;
        toSpawn.equippableBoost.attackDamageModifier = attackDamage == -1 ? getDamageModifier((int)toSpawn.RarityValue + 1) : attackDamage;
        toSpawn.equippableBoost.attackSpeedModifier = attackSpeed == -1 ? getAttackSpeedModifier((int)toSpawn.RarityValue + 1) : attackSpeed;
        toSpawn.equippableBoost.movementSpeedModifier = movementSpeed == -1 ? getMovementSpeedModifier((int)toSpawn.RarityValue + 1) : movementSpeed;
        toSpawn.equippableBoost.healthModifier = health == -1 ? getHealthModifier((int)toSpawn.RarityValue + 1) : health;

        Vector3 randomDir = UtilsClass.GetRandomDir();
        WorldItem worldItem = WorldItem.SpawnWorldItem(transform.position + randomDir * 2f, toSpawn);
        worldItem.GetComponent<Rigidbody2D>().AddForce(randomDir * 5f, ForceMode2D.Impulse);
    }

    public Item.Rarity GetRarity(int rarityMultiplier, bool allowEpicAndLegendary)
    {
        float highestRoll = 0;
        for(int i = 0; i<rarityMultiplier; i++)
        {
            highestRoll = Mathf.Max(highestRoll, Random.Range(0, 1000));
        }
        if(highestRoll > 998 && allowEpicAndLegendary)
        {
            return Item.Rarity.Legendary;
        }
        else if(highestRoll > 990 && allowEpicAndLegendary)
        {
            return Item.Rarity.Epic;
        }
        else if (highestRoll > 960)
        {
            return Item.Rarity.Rare;
        }
        else if (highestRoll > 800)
        {
            return Item.Rarity.Uncommon;
        }
        else if (highestRoll > 500)
        {
            return Item.Rarity.Common;
        }
        else
        {
            return Item.Rarity.Scrap;
        }
    }

    public float getArmorModifier(int qualityLevel)
    {
        return 3f * (float)qualityLevel + (Random.Range(0,3) *(float)qualityLevel);
    }
    public float getDamageModifier(int qualityLevel)
    {
        return 3f * (float)qualityLevel + (Random.Range(0, 3) * (float)qualityLevel);
    }
    public float getAttackSpeedModifier(int qualityLevel)
    {
        return 3f * (float)qualityLevel + (Random.Range(0, 3) * (float)qualityLevel);
    }
    public float getMovementSpeedModifier(int qualityLevel)
    {
        return 3f * (float)qualityLevel + (Random.Range(0, 3) * (float)qualityLevel);
    }
    public float getHealthModifier(int qualityLevel)
    {
        return 3f * (float)qualityLevel + (Random.Range(0, 3) * (float)qualityLevel);
    }

}
