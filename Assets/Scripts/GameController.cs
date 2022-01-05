using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private float levelTime;
    private float levelStartTime;

    [SerializeField] private GameObject shopClosed;
    [SerializeField] private GameObject shopOpen;
    private Transform player;

    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private GameObject indicatorArrow;

    private SharedSceneState sharedSceneState;

    
    private bool levelEnded = false;
    private Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        levelStartTime = Time.realtimeSinceStartup;
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        sharedSceneState = GameObject.FindGameObjectWithTag("SharedState").GetComponent<SharedSceneState>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.realtimeSinceStartup > levelStartTime + levelTime && !levelEnded)
        {
            EndLevel();
        }
        if(levelEnded)
        {
            Vector3 shopDirection = (shopOpen.transform.position - player.position).normalized;
            indicatorArrow.transform.rotation = Quaternion.Euler(0, 0, -Mathf.Atan2(shopDirection.x, shopDirection.y)*180/Mathf.PI);
            indicatorArrow.transform.position = mainCamera.WorldToScreenPoint(player.position + shopDirection * 3);
        }
    }

    void EndLevel()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        //stop all zombies
        //do whatever call to go to the store/show arrow to store etc.
        levelEnded = true;
        shopClosed.SetActive(false);
        shopOpen.SetActive(true);
        enemySpawner.enabled = false;
        indicatorArrow.SetActive(true);
        foreach(GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            enemy.GetComponent<HitHandler>().Die(true);
        }
    }
}
