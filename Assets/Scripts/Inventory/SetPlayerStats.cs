using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SetPlayerStats : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI healthField;
    [SerializeField] TextMeshProUGUI ArmorField;
    [SerializeField] TextMeshProUGUI attackDamageField;
    [SerializeField] TextMeshProUGUI attackSpeedField;
    [SerializeField] TextMeshProUGUI movementSpeedField;
    [SerializeField] ApplyBoosts applyBoosts;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateFields()
    {
        healthField.text = (100 + applyBoosts.getHealthBuff()).ToString();
        ArmorField.text = (applyBoosts.getArmorBuff()).ToString();
        attackDamageField.text = (10 + applyBoosts.getDamageBuff()).ToString();
        attackSpeedField.text = (applyBoosts.getAttackSpeedBuff()).ToString();
        movementSpeedField.text = (applyBoosts.getMovementSpeedBuff()).ToString();
    }
}
