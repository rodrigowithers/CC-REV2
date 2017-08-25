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
            player = Player.Instance.gameObject;
        }
    }

    // Update is called once per frame
    public virtual void Execute (Piece piece) {
		
	}

    public virtual void Exit(Piece piece)
    {
    }

    protected void CheckforDanger()
    {
        //checa se a peça está próxima a um obstáculo que lhe cause dano
        RaycastHit2D[] hits = Physics2D.CircleCastAll(main_script.transform.position, 3, Vector2.zero);
        foreach (RaycastHit2D h in hits)
        {
            //checa se algum dos objetos em torno da peça possui componente FloorSpike
            if (h.collider.GetComponent<FloorSpike>() != null)
            {
                // mudar para o estado em que o inimigo procura por um local mais seguro
                main_script._StateMachine.ChangeState(new FindSafePath());
            }
        }


    }

}
