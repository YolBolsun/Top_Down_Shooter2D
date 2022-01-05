using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;

public class TownBarricadeManager : MonoBehaviour
{
    

    [SerializeField] private Text barricadeText;

    [SerializeField] public List<Barricade> barricades;

    [SerializeField] private int numDefenders = 1;

    private int barricadeBuff = 0;

    public int NumDefenders
    {
        get => numDefenders; set
        {
            numDefenders = value;
            foreach(Barricade barr in barricades)
            {
                barr.updateTimeForHealthToDecrement();
            }
            Debug.Log("num defenders " + numDefenders);
        }
    }

    public int BarricadeBuff
    {
        get => barricadeBuff; set
        {
            int oldBuff = barricadeBuff;
            barricadeBuff = value;
            foreach (Barricade barr in barricades)
            {
                barr.BarricadeHealth += (int)((barricadeBuff-oldBuff) / barricades.Count);
                barr.BarricadeMaxHealth += (int)((barricadeBuff - oldBuff) / barricades.Count);
            }
        }
    }

    public int TotalBarricadeHealth()
    {
        int total = 0;
        foreach(Barricade barricade in barricades)
        {
            total += barricade.BarricadeHealth;
        }
        if(total <= 0)
        {
            SceneManager.LoadScene(2);
        }

        return total;
    }

    public void updateBarricadeHealthText()
    {
        if (barricadeText != null)
        {
            barricadeText.text = TotalBarricadeHealth().ToString();
        }
    }

    public void SetBarricadeHealth(int totalHealth)
    {
        foreach(Barricade barr in barricades)
        {
            barr.BarricadeHealth = (int)(totalHealth / barricades.Count);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        updateBarricadeHealthText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
