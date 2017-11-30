using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Button PlayButton;

    public int ScenarioLenght = 5;

    [Header("Cursor")]
    public Texture2D CursorTex;

    [Header("Levels")]
    public LevelsAsset Levels;
    
    public void Play(string sceneName)
    {
        
    }

    private void Start()
    {
        Cursor.SetCursor(CursorTex, new Vector2(-10, -10), CursorMode.Auto);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.R))
            ResetAllProgress();

    }

    private void ResetAllProgress()
    {
        Debug.Log("RESETTING!!!!");
        Levels.ResetAll();
    }
}
