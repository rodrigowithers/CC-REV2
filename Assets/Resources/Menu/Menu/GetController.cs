using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GetController : MonoBehaviour
{
    private StandaloneInputModule inputs;

    public PlayerController controller;

    // Use this for initialization
    void Start()
    {
        if(controller == null)
        {
            controller = FindObjectOfType<PlayerController>();
        }
        inputs = GetComponent<StandaloneInputModule>();
        inputs.horizontalAxis = "Horizontal";
        inputs.verticalAxis = "Vertical";
        //switch (controller.Type)
        //{
        //    case PlayerController.ControllerType.Keyboard:
        //        inputs.horizontalAxis = "HorizontalMovementKeyboard";
        //        inputs.verticalAxis = "VerticalMovementKeyboard";
        //        break;
        //    case PlayerController.ControllerType.Controller:
        //        inputs.horizontalAxis = "HorizontalMovementController";
        //        inputs.verticalAxis = "VerticalMovementController";
        //        break;
        //}
    }

    // Update is called once per frame
    void Update()
    {

    }
}