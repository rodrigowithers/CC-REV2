using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{

    //TODO:
    [HideInInspector]
    public string DialoguePath;

    public Dialogue[] Dialogues;

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

        //Time.timeScale = 0.0f;

        _dialogueBox.Set(true);
        _dialogueBox.Dialogues = Dialogues;
        _dialogueBox.CurrentTrigger = this;

        _dialogueBox.StartCoroutine(_dialogueBox.CDisplayText());
    }

    public void Exit()
    {
        //Time.timeScale = 1.0f;
        enabled = false;
    }

    public void Start()
    {
        _dialogueBox = FindObjectOfType<DialogueBox>().GetComponent<DialogueBox>();
    }
}
