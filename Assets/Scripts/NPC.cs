using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class NPC : MonoBehaviour
{
    [SerializeField] private Dialogue dialogue;
    [SerializeField] private string storyElement;
    [SerializeField] private bool triggerDialogueOnStart = false;

    private Collider2D coll;
    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<Collider2D>();
        if(triggerDialogueOnStart)
        {
            TriggerDialogue();
        }
    }

    public void TriggerDialogue()
    {
        if (!string.IsNullOrEmpty(storyElement))
        {
            dialogue.AddAction(ExecuteStoryElement);
        }
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseLoc = UtilsClass.GetMouseWorldPosition();
            if (coll.bounds.Contains(mouseLoc))
            {
                GetComponent<NPC>().TriggerDialogue();
            }
        }
    }

    public void ExecuteStoryElement()
    {
        SaveGame.SetStoryElement(storyElement, true);
    }

    public void SetAction(System.Action action)
    {
        dialogue.SetAction(action);
    }

    public void SetDialogue(Dialogue dialogue)
    {
        this.dialogue = dialogue;
    }

}
