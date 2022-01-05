using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanDropoff : MonoBehaviour
{
    [SerializeField] GameObject Van;
    [SerializeField] GameObject Player;
    [SerializeField] GameObject EnemySpawner;
    [SerializeField] float timeToLeave;
    [SerializeField] float timeToDrop;
    [SerializeField] float vanMoveSpeed;
    [SerializeField] MovePlayerToPosition playerMover;

    bool activateUser = true;
    float sceneLoadTime;
    // Start is called before the first frame update
    void Start()
    {
        SharedSceneState.inputEnabled = false;
        Player = GameObject.FindGameObjectWithTag("Player");
        Player.GetComponentInChildren<SpriteRenderer>().enabled = false;
        if (EnemySpawner != null)
        {
            EnemySpawner.SetActive(false);
        }
        sceneLoadTime = Time.realtimeSinceStartup;

    }

    // Update is called once per frame
    void Update()
    {
        if (timeToLeave + sceneLoadTime > Time.realtimeSinceStartup)
        {
            Van.transform.Translate(Vector3.right * vanMoveSpeed * Time.deltaTime);
                
        }
        else
        {
            GameObject.Destroy(Van);
            SharedSceneState.inputEnabled = true;
            if (EnemySpawner != null)
            {
                EnemySpawner.SetActive(true);
            }
            Destroy(this.gameObject);
        }
        if (activateUser && timeToDrop + sceneLoadTime < Time.realtimeSinceStartup)
        {
            activateUser = false;
            Player.GetComponentInChildren<SpriteRenderer>().enabled = true;
            playerMover.SetPlayerPosition();
        }
    }
}
