using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SaveFarmingPatchState
{
    //public TimeSpan timePerState;
    public DateTime timeOfLastStateUpdate;
    public int farmingPatchState;
}
