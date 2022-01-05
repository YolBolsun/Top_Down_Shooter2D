using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public Boost equippableBoost;

    private bool equipped = false;

    public bool Equipped { get => equipped;
        set
        {
            bool wasEquipped = equipped;
            equipped = value;
            if(!equipped && equipped != wasEquipped)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<ApplyBoosts>().RemoveBoost(this);
            }
        }
    }

    public int amount;

    public int itemValue;

    public EquippableItemType equippableItemType;

    private ItemType itemTypeBacker;

    private Rarity rarity = Rarity.Scrap;

    public ItemType itemType { get => itemTypeBacker;
        set
        {
            itemTypeBacker = value;
            switch (itemTypeBacker)
            {
                case ItemType.Rifle:
                    equippableBoost.shotBehavior = Boost.ShotBehaviour.Rifle;
                    equippableItemType = EquippableItemType.PrimaryWeapon;
                    break;
                case ItemType.Shotgun:
                    equippableBoost.shotBehavior = Boost.ShotBehaviour.Shotgun;
                    equippableItemType = EquippableItemType.PrimaryWeapon;
                    break;
                case ItemType.Pistol:
                    equippableBoost.shotBehavior = Boost.ShotBehaviour.Pistol;
                    equippableItemType = EquippableItemType.PrimaryWeapon;
                    break;
                case ItemType.Grenades:
                    equippableBoost.shotBehavior = Boost.ShotBehaviour.None;
                    equippableItemType = EquippableItemType.SecondaryWeapon;
                    break;
                case ItemType.Boots:
                    equippableBoost.shotBehavior = Boost.ShotBehaviour.None;
                    equippableItemType = EquippableItemType.Boots;
                    break;
                case ItemType.Armor:
                    equippableBoost.shotBehavior = Boost.ShotBehaviour.None;
                    equippableItemType = EquippableItemType.Armor;
                    break;
                case ItemType.Helmet:
                    equippableBoost.shotBehavior = Boost.ShotBehaviour.None;
                    equippableItemType = EquippableItemType.Helmet;
                    break;
                default:
                    equippableBoost.shotBehavior = Boost.ShotBehaviour.None;
                    equippableItemType = EquippableItemType.NonEquippable;
                    break;
            }
        }
    }

    public Rarity RarityValue { get => rarity;
        set
        {
            rarity = value;
            System.Random rand = new System.Random();
            itemValue = (int)(Mathf.Pow(2f, (int)rarity) * (rand.NextDouble() + 1.5) * 5f);
        }
    }

    public enum ItemType
    {
        Pistol,
        Rifle,
        Shotgun,
        Helmet,
        Armor,
        Boots,
        Grenades,
        Other
    }
    

    public enum EquippableItemType
    {
        PrimaryWeapon,
        SecondaryWeapon,
        Helmet,
        Armor,
        Boots,
        NonEquippable
    }
    
    public enum Rarity
    {
        Scrap,
        Common,
        Uncommon,
        Rare,
        Epic,
        Legendary,
        NoRarity
    }

    public Item()
    {
        equippableBoost = new Boost();
        System.Random rand = new System.Random();
        itemValue = (int)(Mathf.Pow(2f, (int)rarity) * (rand.NextDouble() + 1.5) * 5f);
    }

    public Item(Item other)
    {
        this.equippableBoost = new Boost(other.equippableBoost);
        this.Equipped = other.Equipped;
        this.itemType = other.itemType;
        this.equippableItemType = other.equippableItemType;
        this.RarityValue = other.RarityValue;
        this.amount = other.amount;
        this.itemValue = other.itemValue;
    }

    public Sprite GetSprite()
    {
        switch(itemType)
        {
            default:
            case ItemType.Pistol: return ItemAssets.Instance.pistolSprite;
            case ItemType.Rifle: return ItemAssets.Instance.rifleSprite;
            case ItemType.Shotgun: return ItemAssets.Instance.shotgunSprite;
            case ItemType.Helmet: return ItemAssets.Instance.helmetSprite;
            case ItemType.Armor: return ItemAssets.Instance.armorSprite;
            case ItemType.Boots: return ItemAssets.Instance.bootsSprite;
            case ItemType.Grenades: return ItemAssets.Instance.grenadeSprite;
            case ItemType.Other: return ItemAssets.Instance.otherSprite;
        }
    }

    public bool isStackable()
    {
        switch (itemType)
        {
            default:
            case ItemType.Helmet:
            case ItemType.Armor: 
            case ItemType.Boots: 
            case ItemType.Pistol:
            case ItemType.Rifle:
            case ItemType.Shotgun:
            case ItemType.Grenades: return false;
            case ItemType.Other: return true;
        }
    }

    public override bool Equals(object obj) => this.Equals(obj as Item);

    public override int GetHashCode() => (itemType, equippableBoost, amount, rarity, itemValue).GetHashCode();

    public bool Equals(Item p)
    {
        if (p is null)
        {
            return false;
        }

        // Optimization for a common success case.
        if (Object.ReferenceEquals(this, p))
        {
            return true;
        }

        // If run-time types are not exactly the same, return false.
        if (this.GetType() != p.GetType())
        {
            return false;
        }

        // Return true if the fields match.
        // Note that the base class is not invoked because it is
        // System.Object, which defines Equals as reference equality.
        return (itemType == p.itemType) && (equippableBoost == p.equippableBoost) && (amount == p.amount) && (RarityValue == p.RarityValue) && (itemValue == p.itemValue);
    }

    public static bool operator ==(Item lhs, Item rhs)
    {
        if (lhs is null)
        {
            if (rhs is null)
            {
                return true;
            }

            // Only the left side is null.
            return false;
        }
        // Equals handles case of null on right side.
        return lhs.Equals(rhs);
    }

    public static bool operator !=(Item lhs, Item rhs)
    {
        return !(lhs == rhs);
    }

}
