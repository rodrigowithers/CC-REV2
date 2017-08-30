using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSwitch : MonoBehaviour
{
    public float Energy = 0.0f;
    public Text EnergyText;

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
        if (Input.GetKeyDown(KeyCode.E))
            ChangeCharacter();

        // Atualiza o texto
        EnergyText.text = "Next Life: " + (int)Energy + "/100";

        if (Energy >= 100)
        {
            Energy = 0;
            Player.Instance.Life++;
        }
    }
}
