using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using CodeMonkey.Utils;

public class UI_Inventory : MonoBehaviour
{
    private Inventory inventory;
    private PlayerInventory playerInventory;

    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;

    private Transform equipmentItemSlotContainer;

    [SerializeField] float itemSlotCellSize;
    [SerializeField] float itemSlotCellSpacing;
    [SerializeField] int numItemsInInventoryRow;
    [SerializeField] private Animator animator;
    [SerializeField] private SetPlayerStats setPlayerStats;

    [SerializeField] private SelectItem selectItem;
    [SerializeField] private RectTransform itemStatsOnHover;

    private bool inventoryOpen = false;


    // Start is called before the first frame update
    void Awake()
    {
        itemSlotContainer = transform.Find("itemSlotContainer");
        itemSlotTemplate = itemSlotContainer.transform.Find("itemSlotTemplate");
        equipmentItemSlotContainer = transform.Find("Equipment").transform.Find("itemSlotContainer");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }
    }

    public void ToggleInventory()
    {
        inventoryOpen = !inventoryOpen;
        animator.SetBool("inventoryOpen", inventoryOpen);
        SharedSceneState.inputEnabled = !inventoryOpen;
        setPlayerStats.updateFields();
    }

    public void SetPlayerInventory(PlayerInventory inventory)
    {
        playerInventory = inventory;
    }

    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;
        inventory.OnItemListChanged += Inventory_OnItemListChanged;
        RefreshInventoryItems();
    }

    private void Inventory_OnItemListChanged(object sender, System.EventArgs e)
    {
        RefreshInventoryItems();
    }

    private void RefreshInventoryItems()
    {

        foreach(Transform child in itemSlotContainer)
        {
            if(child != itemSlotTemplate && !child.gameObject.CompareTag("DragDropTarget"))
            {
                Destroy(child.gameObject);
            }
            
        }
        int x = 0;
        int y = 0;
        foreach(Item item in inventory.GetItemList())
        {
            
            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);
            itemSlotRectTransform.Find("image").GetComponent<Button_UI>().ClickFunc = () =>
            {
                selectItem.MarkItemSelected(itemSlotRectTransform, item);
            };
            itemSlotRectTransform.Find("image").GetComponent<Button_UI>().MouseRightClickFunc = () =>
            {
                Item duplicateItem = new Item(item);
                inventory.RemoveItem(item);
                selectItem.DeselectItem();
                WorldItem.DropItem(playerInventory.transform.position, duplicateItem);
            };
            itemSlotRectTransform.Find("image").GetComponent<Button_UI>().MouseOverOnceFunc = () =>
            {
                Debug.Log("mouse in");
                itemStatsOnHover.gameObject.SetActive(true);
                Vector3 positionToShowStats = new Vector3(itemSlotCellSize + itemSlotRectTransform.position.x, itemSlotCellSize + itemSlotRectTransform.position.y);
                itemStatsOnHover.position = positionToShowStats;
                itemStatsOnHover.Find("HealthField").GetComponent<TextMeshProUGUI>().text = item.equippableBoost.healthModifier.ToString();
                itemStatsOnHover.Find("ArmorField").GetComponent<TextMeshProUGUI>().text = item.equippableBoost.armorModifier.ToString();
                itemStatsOnHover.Find("AttackDamageField").GetComponent<TextMeshProUGUI>().text = item.equippableBoost.attackDamageModifier.ToString();
                itemStatsOnHover.Find("AttackSpeedField").GetComponent<TextMeshProUGUI>().text = item.equippableBoost.attackSpeedModifier.ToString();
                itemStatsOnHover.Find("MovementSpeedField").GetComponent<TextMeshProUGUI>().text = item.equippableBoost.movementSpeedModifier.ToString();
                itemStatsOnHover.Find("ValueField").GetComponent<TextMeshProUGUI>().text = item.itemValue.ToString();
            };
            itemSlotRectTransform.Find("image").GetComponent<Button_UI>().MouseOutOnceFunc = () =>
            {
                Debug.Log("mouse out");
                itemStatsOnHover.gameObject.SetActive(false);
            };
            itemSlotRectTransform.Find("border").GetComponent<Image>().color = GameObject.Find("ItemRarityGradient").GetComponent<ItemRarityGradient>().GetRarityColor(item.RarityValue);
            float amountToAdjust = itemSlotCellSize + itemSlotCellSpacing;
            float leftPadding = 10f;
            if (item.Equipped)
            {
                ShowItemEquipped(item, ref itemSlotRectTransform);
                x--;
            }
            else
            {
                itemSlotRectTransform.anchoredPosition = new Vector2(x * amountToAdjust + leftPadding, -y * amountToAdjust - itemSlotCellSize);
            }
            Image image = itemSlotRectTransform.Find("image").GetComponent<Image>();
            image.sprite = item.GetSprite();
            TextMeshProUGUI uiText = itemSlotRectTransform.Find("amountText").GetComponent<TextMeshProUGUI>();
            itemSlotRectTransform.gameObject.GetComponent<DragDrop>().item = item;//new Item (item);
            
            if (item.amount > 1)
            {
                uiText.SetText(item.amount.ToString());
            }
            else
            {
                uiText.SetText("");
            }
            x++;
            if(x >= numItemsInInventoryRow)
            {
                x = 0;
                y++;
            }
        }
    }

    public void RemoveItem(Item item)
    {
        inventory.RemoveItem(item);
    }

    private void ShowItemEquipped(Item item, ref RectTransform itemSlotRectTransform)
    {
        switch(item.equippableItemType)
        {
            case Item.EquippableItemType.Armor:
                itemSlotRectTransform.anchoredPosition = equipmentItemSlotContainer.Find("Armor").GetComponent<RectTransform>().anchoredPosition;
                break;
            case Item.EquippableItemType.Boots:
                itemSlotRectTransform.anchoredPosition = equipmentItemSlotContainer.Find("Boots").GetComponent<RectTransform>().anchoredPosition;
                break;
            case Item.EquippableItemType.Helmet:
                itemSlotRectTransform.anchoredPosition = equipmentItemSlotContainer.Find("Helmet").GetComponent<RectTransform>().anchoredPosition;
                break;
            case Item.EquippableItemType.PrimaryWeapon:
                itemSlotRectTransform.anchoredPosition = equipmentItemSlotContainer.Find("PrimaryWeapon").GetComponent<RectTransform>().anchoredPosition;
                break;
            case Item.EquippableItemType.SecondaryWeapon:
                itemSlotRectTransform.anchoredPosition = equipmentItemSlotContainer.Find("SecondaryWeapon").GetComponent<RectTransform>().anchoredPosition;
                break;
        }
    }
}
