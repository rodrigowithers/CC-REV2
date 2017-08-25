using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSwitch : MonoBehaviour
{
    //public List<System.Type> Classes;
    //public string[] ClassesDebug;

    public float Energy = 0.0f;
    public Text EnergyText;

    private bool _canChange = false;
    public bool CanChange
    {
        get
        {
            return _canChange;
        }
        set
        {
            _canChange = value;

            switch (value)
            {
                case true:
                    EnergyText.color = Color.yellow;
                    break;
                case false:
                    EnergyText.color = Color.red;
                    break;
            }
        }
    }

    public void SetEnergy(float energy)
    {
        Energy = energy;
    }

    // Use this for initialization
    void Start()
    {
        //ClassesDebug = PartyManager.Instance.Classes.ToArray();
    }

    private void ChangeCharacter()
    {
        Energy = 0;
        CanChange = false;

        // Seta o Canvas
        CanvasManager.Instance.CurrentPanel = CanvasManager.Instance.ChangePanel; 
    }

    // Update is called once per frame
    void Update()
    {
        if (CanChange)
        {
            // Aparece as paradas pra trocar

            if (Input.GetKeyDown(KeyCode.E))
                ChangeCharacter();

            return;
        }

        if (Input.GetKeyDown(KeyCode.E))
            Energy = 100;

        // Atualiza o texto
        EnergyText.text = "Energy: " + (int)Energy + "/100";

        if (Energy < 100)
        {
            Energy += Time.deltaTime;
        }
        if (Energy >= 100)
        {
            Energy = 100;
            CanChange = true;
        }
    }
}
