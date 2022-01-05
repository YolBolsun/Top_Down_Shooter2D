using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RetrievalMissionEnd : MonoBehaviour
{
    [SerializeField] GameObject toBeRetrieved;
    [SerializeField] int sceneToReturnTo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && toBeRetrieved == null)
        {
            SceneManager.LoadScene(sceneToReturnTo);
        }
    }
}
