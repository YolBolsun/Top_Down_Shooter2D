using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Barricade : MonoBehaviour
{
    [SerializeField] private int barricadeMaxHealth;
    [SerializeField] private TownBarricadeManager townBarricadeManager;

    private int barricadeHealth = 20;

    [SerializeField] private float barricadeDeteriorationRate;

    private float timeForHealthToDecrement;
    private float timeOfLastDecrement = 0;

    public int BarricadeHealth
    {
        get => barricadeHealth;
        set
        {
            barricadeHealth = value;
            if (townBarricadeManager)
            {
                townBarricadeManager.updateBarricadeHealthText();
            }
        }
    }

    public int BarricadeMaxHealth { get => barricadeMaxHealth; set => barricadeMaxHealth = value; }

    // Start is called before the first frame update
    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        BarricadeHealth = BarricadeMaxHealth;
        updateTimeForHealthToDecrement();
        GetComponent<NPC>().SetAction(RepairToFull);
    }

    public void updateTimeForHealthToDecrement()
    {
        if (townBarricadeManager)
        {
            timeForHealthToDecrement = (60 / (barricadeDeteriorationRate * Mathf.Max((20 - townBarricadeManager.NumDefenders), .5f)));
        }
        else
        {
            timeForHealthToDecrement = 60 / barricadeDeteriorationRate;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.realtimeSinceStartup > timeOfLastDecrement + timeForHealthToDecrement)
        {
            BarricadeHealth--;
            timeOfLastDecrement = Time.realtimeSinceStartup;
        }
    }

    void RepairToFull()
    {
        BarricadeHealth = BarricadeMaxHealth;
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
            if(BarricadeHealth < 20)
            {
                BarricadeHealth = 20;
            }
        }
        else
        {
            DisableVisuals();
        }
    }

}
