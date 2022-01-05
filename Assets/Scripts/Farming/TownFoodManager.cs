using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;

public class TownFoodManager : MonoBehaviour
{
    [SerializeField] private int numFood = 20;

    [SerializeField] private float foodConsumptionRatePerMinute;

    [SerializeField] private Text foodText;
    [SerializeField] private Text populationText;

    [SerializeField] public List<FarmingPatchManager> farmingPatches;

    [SerializeField] private int townPopulation = 10;
    [SerializeField] private int numFarmers = 1;

    private float timeForFoodToDecrement;
    private float timeOfLastFoodConsumption = 0;

    public int NumFood { get => numFood;
        set
        {
            numFood = value;
            if (foodText != null)
            {
                foodText.text = numFood.ToString();
            }
            if (numFood <= 0)
            {
                SceneManager.LoadScene(2);
            }
        }
    }

    public int TownPopulation
    {
        get => townPopulation;
        set
        {
            townPopulation = value;
            if (populationText != null)
            {
                populationText.text = townPopulation.ToString();
            }
            updateTimeForFoodToDecrement();
        }
    }
    public int NumFarmers
    {
        get => numFarmers;
        set
        {
            numFarmers = value;
            updateTimeForFoodToDecrement();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (foodText != null)
        {
            foodText.text = NumFood.ToString();
        }
        updateTimeForFoodToDecrement();
    }

    public void updateTimeForFoodToDecrement()
    {
        timeForFoodToDecrement = (60 / foodConsumptionRatePerMinute) / Mathf.Max(1, (TownPopulation - 3 * NumFarmers));
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.realtimeSinceStartup > timeOfLastFoodConsumption + timeForFoodToDecrement)
        {
            NumFood--;
            timeOfLastFoodConsumption = Time.realtimeSinceStartup;
            if(numFood <= 0)
            {
                SceneManager.LoadScene(2);
            }
        }
    }

    public void AddFood(int numFoodCollected)
    {
        NumFood += numFoodCollected;
    }
}
