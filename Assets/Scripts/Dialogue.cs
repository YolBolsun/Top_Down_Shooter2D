using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public List<DialogueAction> dialogueActions = new List<DialogueAction>();
    public bool queryUserBeforeAction = false;
    public bool justDoAction = false;
    public bool doActionRegardlessAtEnd = false;

    public string name;

    public string queryUserButtonText = "Go?";

    [TextArea(3, 10)]
    public string[] sentences;

    public void SetAction(System.Action action)
    {
        dialogueActions.Clear();
        AddAction(action);
    }

    public void AddAction(System.Action action)
    {
        DialogueAction dialogueAction = new DialogueAction();
        dialogueAction.actionType = DialogueAction.ActionType.GenericAction;
        dialogueAction.SetAction(action);
        dialogueActions.Add(dialogueAction);
    }

}
