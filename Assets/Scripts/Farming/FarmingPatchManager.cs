using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class FarmingPatchManager : MonoBehaviour
{
    [SerializeField] private int timePerStateInSeconds;
    
    [SerializeField] private int harvestAmount;
    [SerializeField] private int harvestVariance;
    [SerializeField] private TownFoodManager townFoodManager;
    [SerializeField] private Dialogue TilledDialogue;
    [SerializeField] private Dialogue InProgressDialogue;
    [SerializeField] private Dialogue GrownDialogue;
    [SerializeField] private List<string> CropProgressMessages;

    private NPC npc;
    public TimeSpan timePerState;
    public DateTime timeOfLastStateUpdate;
    public FarmingPatchState farmingPatchState;
    private Animator animator;

    public enum FarmingPatchState
    {
        Tilled,
        Planted,
        HalfGrowth,
        Grown
    }

    // Start is called before the first frame update
    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        TilledDialogue.SetAction(Plant);
        GrownDialogue.SetAction(Harvest);
        animator = GetComponent<Animator>();
        npc = GetComponent<NPC>();
        npc.SetDialogue(TilledDialogue);
        timePerState = new TimeSpan(0, 0, timePerStateInSeconds);
        farmingPatchState = FarmingPatchState.Tilled;
        timeOfLastStateUpdate = DateTime.Now;
    }

    // Update is called once per frame
    void Update()
    {
        if(farmingPatchState != FarmingPatchState.Tilled && farmingPatchState != FarmingPatchState.Grown && timeOfLastStateUpdate + timePerState < DateTime.Now)
        {
            farmingPatchState++;
            animator.SetTrigger(farmingPatchState.ToString());
            timeOfLastStateUpdate = timeOfLastStateUpdate + timePerState;
            if (farmingPatchState == FarmingPatchState.Grown)
            {
                npc.SetDialogue(GrownDialogue);
            }
            else
            {
                InProgressDialogue.sentences[0] = CropProgressMessages[(int)farmingPatchState - 1];
            }
        }
    }

    public void Plant()
    {
        farmingPatchState = FarmingPatchState.Planted;
        animator.SetTrigger(farmingPatchState.ToString());
        timeOfLastStateUpdate = DateTime.Now;
        InProgressDialogue.sentences[0] = CropProgressMessages[0];
        npc.SetDialogue(InProgressDialogue);
    }

    public void Harvest()
    {
        if (townFoodManager != null)
        {
            townFoodManager.AddFood(harvestAmount + UnityEngine.Random.Range(0, harvestVariance));
        }
        farmingPatchState = FarmingPatchState.Tilled;
        animator.SetTrigger(farmingPatchState.ToString());
        npc.SetDialogue(TilledDialogue);
    }

    public void DisableVisuals()
    {
        GetComponent<NPC>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        foreach (SpriteRenderer child in gameObject.GetComponentsInChildren<SpriteRenderer>())
        {
            child.enabled = false;
        }
    }

    public void EnableVisuals()
    {
        GetComponent<NPC>().enabled = true;
        GetComponent<Collider2D>().enabled = true;
        foreach (SpriteRenderer child in gameObject.GetComponentsInChildren<SpriteRenderer>())
        {
            child.enabled = true;
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Town")
        {
            EnableVisuals();
        }
        else
        {
            DisableVisuals();
        }
    }

}
