using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueTrigger : MonoBehaviour
{
    public string DialoguePath;

    public List<UnityEvent> Nodes;

    private Dialogue[] _dialogues;

    private DialogueBox _dialogueBox;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
            Enter();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

    }

    public void Enter()
    {
        if (!enabled)
            return;


        /// TODO: Refazer isso
        //if (Level.Instance.Completed)
        //{
        //    _dialogueBox.Dialogues = _dialogues;

        //    foreach (var dialogue in _dialogueBox.Dialogues)
        //    {
        //        //dialogue.EndOfDialogueEvent.Invoke();
        //    }
        
        //    return;
        //}

        _dialogueBox.Set(true);
        _dialogueBox.Dialogues = _dialogues;
        _dialogueBox.Nodes = Nodes;
        _dialogueBox.Filename = DialoguePath;
        _dialogueBox.CurrentTrigger = this;

        _dialogueBox.StartCoroutine(_dialogueBox.CExecuteNodes());
    }

    public void Exit()
    {
        //Time.timeScale = 1.0f;
        enabled = false;
    }

    public void Start()
    {
        _dialogueBox = FindObjectOfType<DialogueBox>().GetComponent<DialogueBox>();
        //LoadDialogues();

        //foreach(var node in Nodes)
        //{
        //    if(node.GetPersistentMethodName(0) == "GetDialogue")
        //    {
                
        //    }
        //}
    }

    private void LoadDialogues()
    {
        //// List<Dialogue> temp = JsonHelper.Instance.Retrieve_Conversation(DialoguePath);
        //Dialogues = JsonHelper.Instance.Retrieve_Conversation(DialoguePath).ToArray();
        //for (int i = 0;i< Dialogues.Length;i++)
        //{
        //    //Dialogues[i] = temp[i];
        //    Dialogues[i].Speaking = Adjust_Speaker_Img(Dialogues[i].person);
        //}
    }

}
