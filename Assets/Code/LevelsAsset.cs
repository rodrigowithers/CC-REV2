using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level Data", menuName = "Levels/Level Data", order = 0)]
public class LevelsAsset : ScriptableObject
{
    [System.Serializable]
    public struct LevelData
    {
        public LevelData(string n, bool c)
        {
            name = n;
            completed = c;
        }

        public string name;
        public bool completed;
    }

    public List<LevelData> Levels;

    public bool HardUnlocked = false;
    public bool QueenUnlocked = false;

    public void CompleteStage(string name)
    {
        for (int i = 0; i < Levels.Count; i++)
        {
            if (Levels[i].name == name)
            {
                Levels[i] = new LevelData(name, true);
            }
        }
    }

    public void UnlockNewMode() 
    {   
        if(GameManager.Instance.Mode == GameManager.GameMode.NORMAL)
        {
            HardUnlocked = true;
        }
        else if(GameManager.Instance.Mode == GameManager.GameMode.HARD)
        {
            QueenUnlocked = true;
        }
        
    }


    public void ResetAll()
    {
        HardUnlocked = false;
        QueenUnlocked = false;

        for (int i = 0; i < Levels.Count; i++)
        {
            Levels[i] = new LevelData(name, false);
        }
    }

    public bool Check(string name)
    {
        foreach (var level in Levels)
        {
            if (level.name == name)
            {
                return level.completed;
            }
        }

        return false;
    }
}
