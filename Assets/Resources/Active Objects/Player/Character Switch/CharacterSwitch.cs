using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSwitch : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {

    }

    private void ChangeCharacter()
    {
        // Seta o Canvas
        CanvasManager.Instance.CurrentPanel = CanvasManager.Instance.ChangePanel;
    }

    // Update is called once per frame
    void Update()
    {
        // Aparece as paradas pra trocar
        if (Input.GetButtonDown("Change"))
            ChangeCharacter();
    }
}
