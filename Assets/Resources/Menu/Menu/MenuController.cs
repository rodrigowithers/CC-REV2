using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public enum MenuScene
    {
        Main = 0,
        Scenarios,
        Settings
    }
    private MenuScene _currentScene;

    public MenuScene CurrentScene
    {
        get { return _currentScene; }
        set
        {
            _currentScene = value;
        }
    }


    // Use this for initialization
    private void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {

    }
}
