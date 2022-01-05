using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    private int money;

    public Inventory inventory;
    [SerializeField] UI_Inventory ui_inventory;

    public int Money { get => money;
        set
        {
            money = value;
            Text moneyText = GameObject.FindGameObjectWithTag("MoneyText")?.GetComponent<Text>();
            if (moneyText != null)
            {
                moneyText.text = money.ToString();
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        SetPlayerInventory(new Inventory());
    }

    public void SetPlayerInventory(Inventory inventory)
    {
        this.inventory = inventory;
        ui_inventory.SetInventory(inventory);
        ui_inventory.SetPlayerInventory(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        WorldItem worldItem = collision.GetComponent<WorldItem>();
        if(worldItem != null && inventory.InventoryHasSpace())
        {
            inventory.AddItem(worldItem.GetItem());
            worldItem.DestroySelf();
        }
    }
}
