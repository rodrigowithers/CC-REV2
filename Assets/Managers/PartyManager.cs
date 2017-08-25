using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyManager : MonoBehaviour
{
    public List<string> Classes = new List<string>()
    {
        "King_Normal"
    };

    public void AddClass(string newClass)
    {
        Classes.Add(newClass);
    }

    private static PartyManager _instance;
    public static PartyManager Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType<PartyManager>();

                if(_instance == null)
                {
                    GameObject go = new GameObject("PartyManager");
                    go.AddComponent<PartyManager>();

                    DontDestroyOnLoad(go);

                    _instance = go.GetComponent<PartyManager>();
                }
            }

            return _instance;
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
}