using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetStoryElementOnLoad : MonoBehaviour
{
    [SerializeField] string storyElement;
    [SerializeField] bool value = true;

    // Start is called before the first frame update
    void Start()
    {
        SaveGame.SetStoryElement(storyElement, value);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
