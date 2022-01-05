using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revive : MonoBehaviour
{
    [SerializeField] private float percentageOfTownValuesToKeep;
    [SerializeField] private int minimumFood;
    [SerializeField] private int minimumBarricadeHealth;

    [SerializeField] private GameObject eventSystem;

    private GameObject sharedSceneState;
    // Start is called before the first frame update
    void Start()
    {
        sharedSceneState = GameObject.Find("SharedSceneState");
        TurnOffSharedUI();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TurnOffSharedUI()
    {
        sharedSceneState.transform.Find("UI").Find("NPC").Find("Canvas").Find("ObjectsToInactivate").gameObject.SetActive(false);
        sharedSceneState.transform.Find("Player Variant").gameObject.SetActive(false);
        sharedSceneState.GetComponent<SaveGame>().enabled = false;
        eventSystem.SetActive(true);
    }

    public void TurnOnSharedUI()
    {
        sharedSceneState.transform.Find("UI").Find("NPC").Find("Canvas").Find("ObjectsToInactivate").gameObject.SetActive(true);
        sharedSceneState.transform.Find("Player Variant").gameObject.SetActive(true);
        sharedSceneState.GetComponent<SaveGame>().enabled = true;
        eventSystem.SetActive(false);
    }

    public void ResetTownValues()
    {
        TownFoodManager townFoodManager = sharedSceneState.transform.Find("TownFoodManager").GetComponent<TownFoodManager>();
        townFoodManager.NumFood = (int)(percentageOfTownValuesToKeep * townFoodManager.NumFood);
        townFoodManager.NumFood = Mathf.Max(townFoodManager.NumFood, minimumFood);

        TownBarricadeManager barricadeManager = sharedSceneState.transform.Find("BarricadeManager").GetComponent<TownBarricadeManager>();
        barricadeManager.SetBarricadeHealth((int)(barricadeManager.TotalBarricadeHealth() * percentageOfTownValuesToKeep));
        barricadeManager.SetBarricadeHealth(Mathf.Max(barricadeManager.TotalBarricadeHealth(), minimumBarricadeHealth));

        PlayerInventory playerInventory = sharedSceneState.transform.Find("Player Variant").GetComponent<PlayerInventory>();
        playerInventory.Money = (int)(percentageOfTownValuesToKeep * playerInventory.Money);
    }


}
