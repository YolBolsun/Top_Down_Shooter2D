using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    [SerializeField] private List<Item.EquippableItemType> acceptableTypes;

    [SerializeField] GameObject player;

    private RectTransform rectTransform;
    void IDropHandler.OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag != null)
        {
            if (acceptableTypes.Count == 0 || acceptableTypes.Contains(eventData.pointerDrag.GetComponent<DragDrop>().item.equippableItemType))
            {
                //equipping an item
                if(acceptableTypes.Contains(eventData.pointerDrag.GetComponent<DragDrop>().item.equippableItemType))
                {
                    player.GetComponent<ApplyBoosts>().ApplyBoost(eventData.pointerDrag.GetComponent<DragDrop>().item);
                    eventData.pointerDrag.GetComponent<DragDrop>().item.Equipped = true;
                    //player.GetComponent<PlayerInventory>().inventory.EquipItem(eventData.pointerDrag.GetComponent<DragDrop>().item);
                }
                else if(eventData.pointerDrag.GetComponent<DragDrop>().item.Equipped)
                {
                    eventData.pointerDrag.GetComponent<DragDrop>().item.Equipped = false;
                    player.GetComponent<ApplyBoosts>().RemoveBoost(eventData.pointerDrag.GetComponent<DragDrop>().item);
                    //player.GetComponent<PlayerInventory>().inventory.UnEquipItem(eventData.pointerDrag.GetComponent<DragDrop>().item);
                }
                eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = rectTransform.anchoredPosition;
                eventData.pointerDrag.GetComponent<DragDrop>().SetDropIsValid();
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
