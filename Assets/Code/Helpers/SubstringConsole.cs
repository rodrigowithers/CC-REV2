using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[System.Serializable]
public class SubstringCommand
{
    public string Command;
    public UnityEvent Event;
}

public class SubstringConsole : MonoBehaviour
{
    private Canvas _canvas;
    public Text Text;

    public List<SubstringCommand> Commands;

    public void CheckCommands(string command)
    {
        if(command == null || command == "" || Commands.Count == 0)
        {
            return;
        }

        UnityEvent e;

        foreach (var currentCommand in Commands)
        {
            if (command.Contains(currentCommand.Command))
            {
                e = currentCommand.Event;
                e.Invoke();

                break;
            }
        }
    }

    public void Print(string toPrint)
    {
        print(toPrint);
    }

    private void Awake()
    {
        _canvas = GetComponent<Canvas>();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("Enter"))
        {
            CheckCommands(Text.text);
        }
    }
}
