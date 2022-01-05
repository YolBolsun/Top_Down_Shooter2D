using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveGame : MonoBehaviour
{
    [SerializeField] SaveState save;
    [SerializeField] SaveTownState saveTown;
    [SerializeField] float timeBetweenAutoSaves;
    [SerializeField] public SaveGameProgress saveProgress;
    [SerializeField] EnableAppropriateStoryElements enableStoryElements;
    float timeOfLastSave = 10;
    // Start is called before the first frame update
    void Start()
    {
        saveProgress.ConstructDictionary();
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.realtimeSinceStartup > timeOfLastSave + timeBetweenAutoSaves)
        {
            timeOfLastSave = Time.realtimeSinceStartup;
            SaveGameState();
        }
    }

    public static void SetStoryElement(string element, bool value)
    {
        GameObject sharedState = GameObject.Find("SharedSceneState");
        sharedState.GetComponent<SaveGame>().saveProgress.progressDictionary[element] = value;
        sharedState.GetComponent<EnableAppropriateStoryElements>().EnableItems();
    }

    public void SaveGameState()
    {
        SavePlayerState();
        SaveTownState();
        SaveGameProgress();
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");
        Debug.Log(Application.persistentDataPath);
        bf.Serialize(file, save);
        file.Close();
    }

    public void LoadGameState()
    {
        //GameObject.Destroy(GameObject.FindObjectOfType<SharedSceneState>().gameObject);
        BinaryFormatter bf = new BinaryFormatter();
        string filePath = Application.persistentDataPath + "/gamesave.save";
        if(!File.Exists(filePath))
        {
            enableStoryElements.EnableItems();
            return;
        }
        FileStream file = File.Open(filePath, FileMode.Open);
        SaveState save = (SaveState)bf.Deserialize(file);
        LoadPlayerState(save.savePlayer);
        LoadTownState(save.saveTownState);
        LoadGameProgress(save.saveGameProgress);
        file.Close();
    }

    public void SavePlayerState()
    {
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        save.savePlayer.inventory = new Inventory(Player.GetComponent<PlayerInventory>().inventory);
        save.savePlayer.equippedItems = Player.GetComponent<ApplyBoosts>().equipment;
        save.savePlayer.playerMoney = Player.GetComponent<PlayerInventory>().Money;
    }

    public void SaveTownState()
    {
        TownFoodManager townFoodManager = GameObject.Find("TownFoodManager").GetComponent<TownFoodManager>();
        save.saveTownState.townFood = townFoodManager.NumFood;
        save.saveTownState.farmingPatchStates.Clear();
        foreach(FarmingPatchManager patch in townFoodManager.farmingPatches)
        {

            save.saveTownState.farmingPatchStates.Add(new SaveFarmingPatchState { timeOfLastStateUpdate = patch.timeOfLastStateUpdate, farmingPatchState = (int)patch.farmingPatchState });
        }
        save.saveTownState.numFarmers = townFoodManager.NumFarmers;
        save.saveTownState.townPopulation = townFoodManager.TownPopulation;


        TownBarricadeManager townBarricadeManager = GameObject.Find("BarricadeManager").GetComponent<TownBarricadeManager>();
        save.saveTownState.barricadeBuff = townBarricadeManager.BarricadeBuff;
        save.saveTownState.barricadeHealths.Clear();
        foreach(Barricade barricade in townBarricadeManager.barricades)
        {
            save.saveTownState.barricadeHealths.Add(barricade.BarricadeHealth);
        }
        save.saveTownState.numBarricadeDefenders = townBarricadeManager.NumDefenders;
    }

    public void SaveGameProgress()
    {
        saveProgress.SaveDictionaryItemsBack();
        save.saveGameProgress = saveProgress;
    }

    public void LoadPlayerState(SavePlayer savePlayer)
    {
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        Player.GetComponent<PlayerInventory>().inventory.itemList.Clear();
        foreach (Item item in savePlayer.inventory.itemList)
        {
            Player.GetComponent<PlayerInventory>().inventory.AddItem(new Item(item));
        }
        //Player.GetComponent<ApplyBoosts>().equipment = save.equippedItems;
        Player.GetComponent<PlayerInventory>().Money = savePlayer.playerMoney;
        foreach(Item item in savePlayer.equippedItems)
        {
            //Debug.Log(item.itemType);
            //Debug.Log(item.Equipped);
            //item.Equipped = true;
            Player.GetComponent<ApplyBoosts>().ApplyBoost(item);
        }
    }

    public void LoadTownState(SaveTownState saveTownState)
    {
        TownFoodManager townFoodManager = GameObject.Find("TownFoodManager").GetComponent<TownFoodManager>();
        townFoodManager.NumFood = saveTownState.townFood;
        int patchNumber = 0;
        foreach (SaveFarmingPatchState patch in saveTownState.farmingPatchStates)
        {
            if(patch.farmingPatchState != 0)
            {
                townFoodManager.farmingPatches[patchNumber].Plant();
            }
            //townFoodManager.farmingPatches[patchNumber].timePerState = patch.timePerState;
            townFoodManager.farmingPatches[patchNumber].timeOfLastStateUpdate = patch.timeOfLastStateUpdate;
            for(int i = 1; i < patch.farmingPatchState; i++)
            {
                townFoodManager.farmingPatches[patchNumber].timeOfLastStateUpdate -= townFoodManager.farmingPatches[patchNumber].timePerState;
            }

            patchNumber++;
        }
        townFoodManager.TownPopulation = saveTownState.townPopulation;
        townFoodManager.NumFarmers = saveTownState.numFarmers;

        TownBarricadeManager townBarricadeManager = GameObject.Find("BarricadeManager").GetComponent<TownBarricadeManager>();
        townBarricadeManager.BarricadeBuff = saveTownState.barricadeBuff;
        for (int i = 0; i < saveTownState.barricadeHealths.Count; i++)
        {
            townBarricadeManager.barricades[i].BarricadeHealth = saveTownState.barricadeHealths[i];
        }
        townBarricadeManager.NumDefenders = saveTownState.numBarricadeDefenders;
    }

    public void LoadGameProgress(SaveGameProgress saveProgress)
    {
        this.saveProgress = saveProgress;
        this.saveProgress.ConstructDictionary();
        enableStoryElements.EnableItems();
    }

    private void OnApplicationQuit()
    {
        SaveGameState();
    }
}
