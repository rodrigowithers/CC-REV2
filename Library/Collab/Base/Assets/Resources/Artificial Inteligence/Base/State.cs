using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum STATETYPE
{
    FOLLOW,
    MOVEMENT,
    ATTACK,
    DODGE,
    FLEE,
    WANDER
}

[System.Serializable]
public class State  {
    protected Unit unit;
    protected GameObject player;
    protected Enemy main_script;
    public STATETYPE Type;

    // Use this for initialization
    public virtual void Enter (Piece piece) {
        if (piece.tag == "Enemy")
        {
            unit = piece.GetComponent<Unit>();
            main_script = piece.GetComponent<Enemy>();
            player = GameManager.Instance.Player;
        }
    }

    // Update is called once per frame
    public virtual void Execute (Piece piece) {
		
	}

    public virtual void Exit(Piece piece)
    {
    }

}
