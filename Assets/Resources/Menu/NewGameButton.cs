using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewGameButton : MonoBehaviour
{
    public LevelsAsset Levels;

    public GameObject HardMode;
    public GameObject QueenMode;

    // Use this for initialization
    void Start()
    {
        HardMode.SetActive(Levels.HardUnlocked);
        QueenMode.SetActive(Levels.QueenUnlocked);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
