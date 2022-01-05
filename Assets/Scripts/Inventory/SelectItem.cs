using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelectItem : MonoBehaviour
{
    private RectTransform selectedRectTransform;
    private Item selectedItem;

    [SerializeField] Color selectedColor;
    [SerializeField] Color unSelectedColor;

    [SerializeField] TextMeshProUGUI health;
    [SerializeField] TextMeshProUGUI armor;
    [SerializeField] TextMeshProUGUI attackDamage;
    [SerializeField] TextMeshProUGUI attackSpeed;
    [SerializeField] TextMeshProUGUI movementSpeed;
    [SerializeField] TextMeshProUGUI itemValue;

    [SerializeField] UI_Inventory ui_inventory;
    [SerializeField] PlayerInventory playerInventory;

    [SerializeField] TextMeshProUGUI sellPriceField;



    public RectTransform SelectedRectTransform { get => selectedRectTransform;
        set
        {
            if (selectedRectTransform != null)
            {
                selectedRectTransform.Find("background").GetComponent<Image>().color = unSelectedColor;
            }
            selectedRectTransform = value;
            if (selectedRectTransform != null)
            {
                selectedRectTransform.Find("background").GetComponent<Image>().color = selectedColor;
            }
        }
    }
    public Item SelectedItem { get => selectedItem;
        set
        {
            selectedItem = value;
            if(selectedItem == null)
            {
                sellPriceField.text = "0";
                health.text = "";
                armor.text = "";
                attackDamage.text = "";
                attackSpeed.text = "";
                itemValue.text = "";

            }
            else
            {
                sellPriceField.text = selectedItem.itemValue.ToString();
                health.text = selectedItem.equippableBoost.healthModifier.ToString();
                armor.text = selectedItem.equippableBoost.armorModifier.ToString();
                attackDamage.text = selectedItem.equippableBoost.attackDamageModifier.ToString();
                attackSpeed.text = selectedItem.equippableBoost.attackSpeedModifier.ToString();
                movementSpeed.text = selectedItem.equippableBoost.movementSpeedModifier.ToString();
                itemValue.text = selectedItem.itemValue.ToString();
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MarkItemSelected(RectTransform rectTransform, Item item)
    {
        SelectedRectTransform = rectTransform;
        SelectedItem = item;
    }

    public void DeselectItem()
    {
        SelectedRectTransform = null;
        SelectedItem = null;
    }

    public void SellSelectedItem()
    {
        //add to player money
        playerInventory.Money += selectedItem.itemValue;
        ui_inventory.RemoveItem(selectedItem);
        DeselectItem();
    }
}
