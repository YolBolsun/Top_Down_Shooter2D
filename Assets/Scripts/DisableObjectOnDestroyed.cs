using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableObjectOnDestroyed : MonoBehaviour
{
    [SerializeField] List<HitHandler> gameObjectsToWatch;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool enemiesDestroyed = true;
        Debug.Log(gameObjectsToWatch.Count);
        foreach(HitHandler hitHandler in gameObjectsToWatch)
        {
            if(!hitHandler.isDead)
            {
                enemiesDestroyed = false;
            }
        }
        if(enemiesDestroyed)
        {
            this.gameObject.SetActive(false);
        }
    }
}
