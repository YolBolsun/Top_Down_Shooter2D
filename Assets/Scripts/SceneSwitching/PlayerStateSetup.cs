using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateSetup : MonoBehaviour
{
    private GameObject player;

    [SerializeField] private bool ShootingEnabled;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        player.GetComponent<ShootingController>().enabled = ShootingEnabled ? true : false;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
