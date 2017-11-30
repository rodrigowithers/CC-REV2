using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    public LevelsAsset Levels;

    public void NewGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("1");
    }

    public void ContinueGame()
    {
        // Checa qual dos Levels está completed, carrega o próximo
        for (int i = 0; i < Levels.Levels.Count; i++)
        {
            if(i == 2)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("3");
                break;
            }

            if (!Levels.Levels[i].completed)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene((i + 1).ToString());
                break;
            }
        }
    }

    public void Hard()
    {
        GameManager.Instance.Mode = GameManager.GameMode.HARD;
        NewGame();
    }

    public void Queen()
    {
        GameManager.Instance.Mode = GameManager.GameMode.QUEEN;
        NewGame();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
