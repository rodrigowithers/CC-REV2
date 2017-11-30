using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameMenu : MonoBehaviour
{
    private bool _onMenu = false;
    public bool OnMenu
    {
        set
        {
            _onMenu = value;
            if(_onMenu)
            {
                Time.timeScale = 0.0f;
                _canvas.enabled = true;
                Quit.enabled = true;
                Quit.Select();

                SoundManager.Instance.Mixer.FindSnapshot("Menus").TransitionTo(0.0f);
            }
            else
            {
                _canvas.enabled = false;
                Time.timeScale = 1.0f;
                Quit.enabled = false;

                SoundManager.Instance.Mixer.FindSnapshot("Main").TransitionTo(0.0f);
            }
        }
        get
        {
            return _onMenu;
        }
    }

    private Canvas _canvas;

    public Button Quit;

    public void ToMenu()
    {
        Time.timeScale = 1.0f;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }

    private void Awake()
    {
        _canvas = GetComponent<Canvas>();
        _canvas.enabled = false;
        Quit.enabled = false;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Cancel"))
        {
            OnMenu = !OnMenu;
        }
    }
}
