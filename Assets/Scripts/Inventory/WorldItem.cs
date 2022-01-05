using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using CodeMonkey.Utils;
using UnityEngine.Experimental.Rendering.Universal;

public class WorldItem : MonoBehaviour
{
    private Item item;
    private SpriteRenderer spriteRenderer;
    private TextMeshPro textMeshPro;
    private ItemRarityGradient itemRarityGradient;

    public Item Item { get => item;
        set
        {
            item = value;
            itemRarityGradient = GameObject.Find("ItemRarityGradient").GetComponent<ItemRarityGradient>();
            GetComponentInChildren<Light2D>().color = itemRarityGradient.GetRarityColor(item.RarityValue);
            GetComponentInChildren<Light2D>().intensity *= itemRarityGradient.GetRarityLightMultiplier(item.RarityValue);
            //GetComponentInChildren<Light2D>().pointLightOuterRadius *= itemRarityGradient.GetRarityLightMultiplier(item.rarity);
        }
    }

    public static WorldItem SpawnWorldItem(Vector3 position, Item item)
    {
        Transform transform = Instantiate(ItemAssets.Instance.pfWorldItem, position, Quaternion.identity);

        WorldItem worldItem = transform.GetComponent<WorldItem>();
        worldItem.SetItem(item);
        return worldItem;
    }

    public static WorldItem DropItem(Vector3 position, Item item)
    {
        Vector3 randomDir = UtilsClass.GetRandomDir();
        WorldItem worldItem = SpawnWorldItem(position + randomDir*2f, item);
        worldItem.GetComponent<Rigidbody2D>().AddForce(randomDir*5f, ForceMode2D.Impulse);
        return worldItem;
    }

    // Start is called before the first frame update
    void Awake()
    {
        
        spriteRenderer = GetComponent<SpriteRenderer>();     
        textMeshPro = transform.Find("Text").GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetItem(Item item)
    {
        this.Item = item;
        spriteRenderer.sprite = item.GetSprite();
        if(item.amount > 1)
        {
            textMeshPro.SetText(item.amount.ToString());
        }
        else
        {
            textMeshPro.SetText("");
        }
    }

    public Item GetItem()
    {
        return Item;
    }
    public void DestroySelf()
    {
        Destroy(this.gameObject);
    }
}
