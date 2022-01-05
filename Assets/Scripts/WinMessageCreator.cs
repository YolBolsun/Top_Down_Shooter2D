using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinMessageCreator : MonoBehaviour
{
    [SerializeField] private Text winText;
    private SharedSceneState sharedSceneState;
    // Start is called before the first frame update
    void Start()
    {
        sharedSceneState = GameObject.FindGameObjectWithTag("SharedState").GetComponent<SharedSceneState>();
        winText.text = "You Won, killing " + sharedSceneState.zombieKilledCounter + " zombies in the process.";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
