using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BringNPCToTown : MonoBehaviour
{
    public bool addToFarmers;
    public bool addToBarricadeDefense;
    public bool goToTownWithNPC = false;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<NPC>().SetAction(AddToTown);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddToTown()
    {
        TownFoodManager townFoodManager = GameObject.Find("TownFoodManager").GetComponent<TownFoodManager>();
        townFoodManager.TownPopulation++;
        if(addToFarmers)
        {
            townFoodManager.NumFarmers++;
        }
        if (addToBarricadeDefense)
        {
            GameObject.Find("BarricadeManager").GetComponent<TownBarricadeManager>().NumDefenders++;
        }
        if(goToTownWithNPC)
        {
            SceneManager.LoadScene(5);
        }
        if(!goToTownWithNPC)
        {
            GameObject.Destroy(this.gameObject);
        }
    }
}
