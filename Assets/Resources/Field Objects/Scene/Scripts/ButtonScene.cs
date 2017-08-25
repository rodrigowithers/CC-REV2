using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonScene : Scene
{
    public List<FieldButton> Buttons;

    public override bool SceneCompleted()
    {
        // Checa todos os botões, se todos estiverem Pressed, acaba
        foreach(var b in Buttons)
        {
            if (!b.Pressed)
                return false;
        }

        return true;
    }

    public override void SceneEnter()
    {
        base.SceneEnter();
    }

    public override void SceneExit()
    { 
        base.SceneExit();
    }

    // Use this for initialization
    void Start()
    {
        Buttons = new List<FieldButton>(this.gameObject.GetChildrenOfType<FieldButton>());


        base.Start();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void SceneUpdate()
    {
        base.SceneUpdate();
    }
}
