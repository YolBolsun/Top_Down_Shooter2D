using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    private Queue<string> sentences;
    private bool queryUserBeforeAction;
    private bool takeAction = false;
    private List<DialogueAction> dialogueActions = new List<DialogueAction>();

    [SerializeField] private Text nameText;
    [SerializeField] private Text dialogueText;
    [SerializeField] private Animator animator;

    [SerializeField] private Button continueButton;
    [SerializeField] private Button goButton;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        if(dialogue.justDoAction)
        {
            foreach(DialogueAction action in dialogue.dialogueActions)
            {
                Debug.Log("Taking action");
                action.TakeAction();
            }
            return;
        }
        takeAction = dialogue.doActionRegardlessAtEnd;
        goButton.GetComponentInChildren<Text>().text = dialogue.queryUserButtonText;
        dialogueActions = dialogue.dialogueActions;
        nameText.text = dialogue.name;
        queryUserBeforeAction = dialogue.queryUserBeforeAction;
        animator.SetBool("isOpen", true);
        sentences.Clear();
        goButton.gameObject.SetActive(false);
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        if(sentences.Count == 1)
        {
            if(queryUserBeforeAction)
            {
                goButton.gameObject.SetActive(true);
            }
            continueButton.GetComponentInChildren<Text>().text = "End Conversation";
        }
        else
        {
            continueButton.GetComponentInChildren<Text>().text = "Continue >>";
        }
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentences.Dequeue()));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach(char c in sentence)
        {
            dialogueText.text += c;
            yield return null;
        }
    }

    public void EndDialogue()
    {
        if(dialogueActions.Count != 0 && takeAction)
        {
            foreach(DialogueAction action in dialogueActions)
            {
                Debug.Log("Taking action");
                action.TakeAction();
            }
            takeAction = false;
        }
        animator.SetBool("isOpen", false);
    }

    public void TakeAction()
    {
        takeAction = true;
    }

}
