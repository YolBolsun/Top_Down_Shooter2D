using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class DialogueAction
{
    public enum ActionType
    {
        NoAction,
        LoadScene,
        GenericAction
    }

    public int sceneNum;
    private System.Action genericAction;


    public ActionType actionType = ActionType.NoAction;

    public void TakeAction()
    {
        switch (actionType)
        {
            case ActionType.LoadScene:
                SceneManager.LoadScene(sceneNum);
                break;
            case ActionType.GenericAction:
                genericAction();
                break;
            default:
                return;
        }
    }

    public void SetAction(System.Action action)
    {
        genericAction = action;
    }


}
