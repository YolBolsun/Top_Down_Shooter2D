using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharedSceneState : MonoBehaviour
{
    public static bool inputEnabled = true;
    private static SharedSceneState sharedSceneState;
    public int zombieKilledCounter = 0;

    [SerializeField] private float timeToDelayFirstLoad;
    private bool loaded = false;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        if(sharedSceneState == null)
        {
            sharedSceneState = this;
        }
        else
        {
            DestroyImmediate(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!loaded && Time.realtimeSinceStartup > timeToDelayFirstLoad)
        {
            GetComponent<SaveGame>().LoadGameState();
            loaded = true;
        }
    }
}
