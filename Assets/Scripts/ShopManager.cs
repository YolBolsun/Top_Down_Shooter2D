using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    NPC npc;
    Animator shopAnim;
    Animator inventoryAnim;
    bool shopOpen = false;
    // Start is called before the first frame update
    void Start()
    {
        npc = GetComponent<NPC>();
        npc.SetAction(ToggleShop);
        shopAnim = GameObject.Find("Store").GetComponent<Animator>();
        inventoryAnim = GameObject.Find("UI_Inventory").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(shopOpen && Input.GetKeyDown(KeyCode.I))
        {
            ToggleShop();
        }
    }

    public void ToggleShop()
    {
        shopOpen = !shopOpen;
        SharedSceneState.inputEnabled = !shopOpen;
        shopAnim.SetBool("StoreOpen", shopOpen);
        inventoryAnim.SetBool("inventoryOpen", shopOpen);
    }

    void OnDestroy()
    {
        if (shopAnim)
        {
            shopAnim.SetBool("StoreOpen", false);
        }
        if (inventoryAnim)
        {
            inventoryAnim.SetBool("inventoryOpen", false);
        }
    }

}
