using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangePanel : MonoBehaviour
{
    public Sprite[] images;

    public Button[] Classes;

    public Button Starting;

    public Button Selected;

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
                Classes[i].image.sprite = GetSprite(PartyManager.Instance.Classes[i].Name);

                Classes[i].Select();
                Selected = Classes[i];
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
        Player player = Player.Instance;

        PartyManager.Instance.SetCharacterLife(player.GetClass.GetType().Name, Player.Instance.Life);

        //System.Type t = System.Type.GetType(Player.Instance.GetComponent<CharacterSwitch>().ClassesDebug[index]);
        System.Type t = System.Type.GetType(PartyManager.Instance.Classes[index].Name);

        //////////////// muda a vida do player //////////////////////////////
        player.Life = PartyManager.Instance.Classes[index].Life;

        PieceManager.Instance.ChangeClass(player, t);

        Time.timeScale = 1.0f;
        CanvasManager.Instance.CurrentPanel = CanvasManager.Instance.ScenarioPanel;
        player.enabled = true;
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
