using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangePanel : MonoBehaviour
{
    public Sprite[] images;

    public Button[] Classes;

    public Button Starting;

    private Sprite GetSprite(string name)
    {
        foreach (var image in images)
        {
            if (image.name == name)
                return image;
        }

        throw new System.Exception("Não encontrou a imagem");
    }

    private void OnEnable()
    {
        //print("Changing Classes");

        // Troca as imagens

        for (int i = 0; i < 3; i++)
        {
            Classes[i].gameObject.SetActive(false);

            if(PartyManager.Instance.Classes.Count > i)
            {
                Classes[i].gameObject.SetActive(true);
                Classes[i].image.sprite = GetSprite(PartyManager.Instance.Classes[i]);

                Classes[i].Select();
            }
        }

        //Classes[0].image.sprite = GetSprite(Player.Instance.GetComponent<CharacterSwitch>().ClassesDebug[0]);
        //Classes[1].image.sprite = GetSprite(Player.Instance.GetComponent<CharacterSwitch>().ClassesDebug[1]);
        //Classes[2].image.sprite = GetSprite(Player.Instance.GetComponent<CharacterSwitch>().ClassesDebug[2]);

        Time.timeScale = 0;
        Player.Instance.enabled = false;

        //Starting.Select();
    }

    public void ChangeClass(int index)
    {
        //System.Type t = System.Type.GetType(Player.Instance.GetComponent<CharacterSwitch>().ClassesDebug[index]);
        System.Type t = System.Type.GetType(PartyManager.Instance.Classes[index]);

        // Checa se não é a mesma classe
        if (t == Player.Instance.GetClass.GetType())
        {
            Player.Instance.GetComponent<CharacterSwitch>().Energy = 100;
        }

        PieceManager.Instance.ChangeClass(Player.Instance, t);

        Time.timeScale = 1.0f;
        CanvasManager.Instance.CurrentPanel = CanvasManager.Instance.ScenarioPanel;
        Player.Instance.enabled = true;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            Time.timeScale = 1.0f;
            CanvasManager.Instance.CurrentPanel = CanvasManager.Instance.ScenarioPanel;

            Player.Instance.enabled = true;
        }
    }
}
