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

        _dialogueBox.Set(true);
        _dialogueBox.Dialogues = _dialogues;
        _dialogueBox.Nodes = Nodes;
        _dialogueBox.Filename = DialoguePath;
        _dialogueBox.CurrentTrigger = this;

        _dialogueBox.StartDoingTheThing();
    }

    public void Exit()
    {
        enabled = false;
    }

    public void Start()
    {
        _dialogueBox = FindObjectOfType<DialogueBox>().GetComponent<DialogueBox>();
    }
}
