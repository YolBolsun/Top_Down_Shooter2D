using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRarityGradient : MonoBehaviour
{
    /*
        Scrap,
        Common,
        Uncommon,
        Rare,
        Epic,
        Legendary
    */
    [SerializeField] Gradient rarityColors;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Color GetRarityColor(Item.Rarity rarity)
    {
        switch (rarity)
        {
            case Item.Rarity.Scrap:
                return rarityColors.Evaluate(.1f);
            case Item.Rarity.Common:
                return rarityColors.Evaluate(.25f);
            case Item.Rarity.Uncommon:
                return rarityColors.Evaluate(.4f);
            case Item.Rarity.Rare:
                return rarityColors.Evaluate(.55f);
            case Item.Rarity.Epic:
                return rarityColors.Evaluate(.7f);
            case Item.Rarity.Legendary:
                return rarityColors.Evaluate(.85f);
            default:
                return rarityColors.Evaluate(0);
        }
    }
    public float GetRarityLightMultiplier(Item.Rarity rarity)
    {
        switch (rarity)
        {
            case Item.Rarity.Scrap:
                return 0f;
            case Item.Rarity.Common:
                return 0f;
            case Item.Rarity.Uncommon:
                return .2f;
            case Item.Rarity.Rare:
                return .4f;
            case Item.Rarity.Epic:
                return 2f;
            case Item.Rarity.Legendary:
                return 1.5f;
            default:
                return 0f;
        }
    }
}
