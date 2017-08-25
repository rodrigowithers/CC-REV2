using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Pega inputs de todos os tipos diferentes
/// </summary>
public class PlayerController : MonoBehaviour
{
    public enum ControllerType
    {
        Keyboard = 0,
        Controller
    }
    private ControllerType _type;

    public ControllerType Type
    {
        get
        {
            return _type;
        }

        set
        {
            _type = value;
        }
    }

    public Vector2 LAS
    {
        get
        {
            Vector2 toReturn = Vector2.zero;

            switch (Type)
            {
                case ControllerType.Keyboard:
                    toReturn.x = Input.GetAxisRaw("HLAS_Keyboard");
                    toReturn.y = Input.GetAxisRaw("VLAS_Keyboard");
                    break;
                case ControllerType.Controller:
                    toReturn.x = Input.GetAxisRaw("HLAS_Controller");
                    toReturn.y = Input.GetAxisRaw("VLAS_Controller");
                    break;
            }

            if (toReturn.magnitude <= 0.5f)
                return Vector2.zero;

            return toReturn;
        }
    }

    public Vector2 RAS
    {
        get
        {
            Vector2 toReturn = Vector2.zero;

            switch (Type)
            {
                case ControllerType.Keyboard:
                    toReturn.x = Input.GetAxis("HRAS_Keyboard");
                    toReturn.y = Input.GetAxis("VRAS_Keyboard");
                    break;
                case ControllerType.Controller:
                    toReturn.x = Input.GetAxisRaw("HRAS_Controller");
                    toReturn.y = Input.GetAxisRaw("VRAS_Controller");
                    break;
            }

            if (toReturn.magnitude <= 0.5f)
                return Vector2.zero;

            return toReturn;
        }
    }

    public bool Hability
    {
        get
        {
            return Input.GetButtonDown("Hability");
        }
    }

    private void CheckControllers()
    {
        // Checa se um controle foi conectado na porta 1
        var joysticks = Input.GetJoystickNames();

        if (joysticks.Length <= 0)
            return;

        if (joysticks[0] != "" && Type != ControllerType.Controller)
            Type = ControllerType.Controller;
        else if (joysticks[0] == "" && Type != ControllerType.Keyboard)
            Type = ControllerType.Keyboard;
    }

    private void Awake()
    {
        CheckControllers();
    }

    void Start()
    {

    }

    void Update()
    {
        CheckControllers();
    }
}
