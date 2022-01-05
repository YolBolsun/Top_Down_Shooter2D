using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayerToPosition : MonoBehaviour
{
    [SerializeField] private bool setPositionAtStart;

    // Start is called before the first frame update
    void Start()
    {
        if (setPositionAtStart)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position = transform.position;
            GameObject.FindGameObjectWithTag("Player").GetComponent<UnitRTSMovement>().SetDestination(transform.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPlayerPosition()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position = transform.position;
        GameObject.FindGameObjectWithTag("Player").GetComponent<UnitRTSMovement>().SetDestination(transform.position);
    }
}
