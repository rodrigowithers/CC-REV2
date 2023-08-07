using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyManager : MonoBehaviour
{
    public int pieceskilled = 0;
    public List<Character_Stats> Classes = new List<Character_Stats>();

    public void AddClass(string newClass)
    {
        if (GameManager.Instance.Mode == GameManager.GameMode.QUEEN)
            return;

        Classes.Add(new Character_Stats(newClass, 10, 10));
    }

    private static PartyManager _instance;
    public static PartyManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<PartyManager>();

                if (_instance == null)
                {
                    GameObject go = new GameObject("PartyManager");
                    go.AddComponent<PartyManager>();
                    _instance = go.GetComponent<PartyManager>();
                }
            }

            return _instance;
        }
    }

    public void PieceKilled(string s)
    {
        pieceskilled++;

        for (int i = 0; i < Classes.Count; i++)
        {
            if (Classes[i].Name == s)
            {
                Classes.RemoveAt(i);
            }
        }
        //if(!Classes.Remove(s))
        //{
        //    Debug.Log("nao removeu classe direito");
        //    Debug.Log("Removeu : " + s);
        //}
    }

    private void Awake()
    {

    }

    private void Start()
    {
        if (GameManager.Instance.Mode == GameManager.GameMode.QUEEN)
        {
            Classes = new List<Character_Stats>();
            Classes.Add(new Character_Stats("Queen_Normal", 10, 10));

            System.Type t = System.Type.GetType("Queen_Normal");
            PieceManager.Instance.ChangeClass(Player.Instance, t);
        }

        if (GameManager.Instance.Mode == GameManager.GameMode.HARD)
        {
            foreach (var c in Classes)
            {
                c.Life = c.Max_Life = 3;
            }
        }
    }


    public void SetCharacterLife(string name, int l)
    {
        for (int i = 0; i < Classes.Count; i++)
        {
            if (Classes[i].Name == name)
            {
                Classes[i].Life = l;
                break;
            }
        }
    }
    public int GetCharacterLife(string name)
    {
        int toreturn = 0;
        for (int i = 0; i < Classes.Count; i++)
        {
            if (Classes[i].Name == name)
            {
                toreturn = Classes[i].Life;
                break;
            }
        }
        return toreturn;
    }
}

[System.Serializable]
public class Character_Stats
{
    public string Name;
    public int Max_Life;
    public int Life;
    public Character_Stats()
    {
        Name = "King_Normal";
        Max_Life = 10;
        Life = 10;
    }
    public Character_Stats(string name, int max, int life)
    {
        Name = name;
        Max_Life = max;
        Life = life;

        if (GameManager.Instance.Mode == GameManager.GameMode.HARD)
        {
            Max_Life = Life = 3;
        }
    }
}