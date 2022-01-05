using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class SaveTownState
{
    public int townFood;
    public List<int> barricadeHealths;
    public int barricadeBuff;

    public int townPopulation;
    public int numFarmers;
    public int numBarricadeDefenders;
    public List<SaveFarmingPatchState> farmingPatchStates;


}
