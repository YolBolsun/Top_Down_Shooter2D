using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveGameProgress
{
   /* public struct GameProgressStates
    {
        static string INTRO_SCENE = "introScene";
        static string WELCOME_TO_TOWN = "welcomeToTown";
        static string BARRICADE_HELP = "barricadeHelp";
        static string BARRICADE_QUEST = "barricadeQuest";
        static string FARMING_HELP = "farmingHelp";
        static string FARMING_QUEST = "farmingQuest";

    }*/

    [System.Serializable]
    public class progressDictionaryElement
    {
        public string name;
        public bool status;
    }

    public bool preferEditorValues = false;

    public List<progressDictionaryElement> progressElements;

    public Dictionary<string, bool> progressDictionary;

    public SaveGameProgress()
    {
        progressElements = new List<progressDictionaryElement>();
        progressDictionary = new Dictionary<string, bool>();
    }

    public void ConstructDictionary()
    {
        progressDictionary.Clear();
        foreach(progressDictionaryElement element in progressElements)
        {
            progressDictionary.Add(element.name, element.status);
        }
    }

    public void SaveDictionaryItemsBack()
    {
        if(preferEditorValues)
        {
            return;
        }
        foreach(string key in progressDictionary.Keys)
        {
            SaveItemFromDictionary(key, progressDictionary[key]);
        }
    }
    private void SaveItemFromDictionary(string key, bool value)
    {
        for(int i = 0; i < progressElements.Count; i++)
        {
            if (progressElements[i].name == key)
            {
                progressElements[i].status = value;
                return;
            }
        }
    }

}
