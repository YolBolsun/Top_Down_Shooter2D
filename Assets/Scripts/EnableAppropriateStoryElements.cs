using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnableAppropriateStoryElements : MonoBehaviour
{
    public List<ItemToEnable> itemsToEnable;
    public SaveGame save;

    [System.Serializable]
    public struct ItemToEnable
    {
        public string itemNameToEnable;
        public string storyElementPreReq;
        public bool EnableOnUncompleted;
        public bool disableOnComplete;
    }
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnableItems()
    {
        GameObject storyElementsObject = GameObject.Find("StoryElements");
        if(storyElementsObject == null)
        {
            return;
        }
        Transform[] storyElements = storyElementsObject.GetComponentsInChildren<Transform>(true);
        foreach (ItemToEnable item in itemsToEnable)
        {
            GameObject itemToEnable = null;
            foreach(Transform storyElement in storyElements)
            {
                if(storyElement.gameObject.name == item.itemNameToEnable)
                {
                    itemToEnable = storyElement.gameObject;
                    break;
                }
            }
            if (itemToEnable != null)
            {
                if (save.saveProgress.progressDictionary[item.storyElementPreReq])
                {
                    if (!item.EnableOnUncompleted)
                    {
                        itemToEnable.SetActive(true);
                    }
                    if (item.disableOnComplete)
                    {
                        itemToEnable.SetActive(false);
                    }
                }
                else if (item.EnableOnUncompleted)
                {
                    itemToEnable.SetActive(true);
                }
            }
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        EnableItems();
    }
}
