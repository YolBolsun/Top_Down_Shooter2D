using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Purchaseable : MonoBehaviour
{
    [SerializeField] private int price;
    [SerializeField] private Text priceText;

    public int Price
    {
        get => price; set
        {
            price = value;
            priceText.text = price.ToString();
        }
    }

    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private TownFoodManager townFoodManager;
    [SerializeField] private TownBarricadeManager townBarricadeManager;

    [SerializeField] private int improveBarricadesBuff = 0;

    // Start is called before the first frame update
    void Start()
    {
        priceText.text = price.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ImprveBarricades()
    {
        if(playerInventory.Money > Price)
        {
            playerInventory.Money -= Price;
            townBarricadeManager.BarricadeBuff += improveBarricadesBuff;
        }
    }

    public void HireFarmer()
    {
        if (playerInventory.Money > Price)
        {
            playerInventory.Money -= Price;
            townFoodManager.NumFarmers += 1;
            townFoodManager.TownPopulation += 1;
        }
    }

    public void HireTownGuard()
    {

        if (playerInventory.Money > Price)
        {
            playerInventory.Money -= Price;
            townBarricadeManager.NumDefenders += 1;
            townFoodManager.TownPopulation += 1;
        }
    }
}
