using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private int SceneToLoad;

    [SerializeField] private bool goWithoutPrompt = false;

    // Start is called before the first frame update
    void Start()
    {
        if(goWithoutPrompt)
        {
            LoadScene();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(SceneToLoad);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            LoadScene();
        }
    }


}
