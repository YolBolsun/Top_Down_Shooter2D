using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyBoosts : MonoBehaviour
{
    [SerializeField] SetPlayerStats setPlayerStats;

    public float powerLevel()
    {
        return getDamageBuff() + getAttackSpeedBuff() + getMovementSpeedBuff() + getArmorBuff() + getHealthBuff();
    }

    /*
     * Addition to damage per shot directly
     * */
    public float getDamageBuff()
    {
        float totalBuff = 0;
        if (equipment != null)
        {
            foreach (Item item in equipment)
            {
                totalBuff += item.equippableBoost.attackDamageModifier;
            }
        }
        return totalBuff;
    }

    /*
     * Attack speed percentage increase so 100 will double attack speed
     * */
    public float getAttackSpeedBuff()
    {
        float totalBuff = 0;
        if (equipment != null)
        {
            foreach (Item item in equipment)
            {
                totalBuff += item.equippableBoost.attackSpeedModifier;
            }
        }
        return totalBuff;
    }
    /*
     * Same as attack speed
     * */
    public float getMovementSpeedBuff()
    {
        float totalBuff = 0;
        if (equipment != null)
        {
            foreach (Item item in equipment)
            {
                totalBuff += item.equippableBoost.movementSpeedModifier;
            }
        }
        return totalBuff/2f;
    }
    /*
     * 100% will half your damage
     * 200% will make it a 3rd
     * */
    public float getArmorBuff()
    {
        float totalBuff = 0;
        if (equipment != null)
        {
            foreach (Item item in equipment)
            {
                totalBuff += item.equippableBoost.armorModifier;
            }
        }
        return totalBuff;
    }
    /*
     * straight up addition to your max health
     * */
    public float getHealthBuff()
    {
        float totalBuff = 0;
        if (equipment != null)
        {
            foreach (Item item in equipment)
            {
                totalBuff += item.equippableBoost.healthModifier;
            }
        }
        return totalBuff;
    }

    public Boost.ShotBehaviour getShootingType()
    {
        foreach(Item item in equipment)
        {
            if(item.equippableBoost.shotBehavior != Boost.ShotBehaviour.None && item.equippableBoost.shotBehavior != Boost.ShotBehaviour.Rifle)
            {
                return item.equippableBoost.shotBehavior;
            }
        }
        return Boost.ShotBehaviour.Rifle;
    }

    /*
    Item equippedPrimaryWeapon;
    Item equippedSecondaryWeapon;
    Item equippedHelmet;
    Item equippedArmor;
    Item equippedBoots;*/

    public List<Item> equipment;

    HitHandler hitHandler;
    // Start is called before the first frame update
    void Start()
    {
        equipment = new List<Item>();
        hitHandler = GetComponent<HitHandler>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ApplyBoost(Item item)
    {
        if (equipment != null && hitHandler != null)
        {
            for (int i = 0; i < equipment.Count; i++)
            {
                if (equipment[i].equippableItemType == item.equippableItemType)
                {
                    equipment.RemoveAt(i);
                    break;
                }
            }
            equipment.Add(item);
            Debug.Log("power level " + powerLevel());
            //hitHandler.ResetHealthBar();
        }
        setPlayerStats.updateFields();
    }

    public void RemoveBoost(Item item)
    {
        for (int i = 0; i< equipment.Count; i++)
        {
            if(equipment[i].equippableItemType == item.equippableItemType)
            {
                equipment.RemoveAt(i);
            }
        }
        //hitHandler.ResetHealthBar();
        setPlayerStats.updateFields();
    }

}
