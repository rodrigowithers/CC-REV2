using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour {


    private static StateManager _instance = null;
    public static StateManager Instance
    {
        get
        {
            if (_instance == null)
            {
                if (FindObjectOfType<StateManager>() == null)
                {
                    GameObject go = new GameObject("StateManager");
                    go.AddComponent<StateManager>();

                    DontDestroyOnLoad(go);

                    _instance = go.GetComponent<StateManager>();
                }
                else
                {
                    _instance = FindObjectOfType<StateManager>();
                }
            }

            return _instance;
        }
    }

    public void AdjustHunt(Enemy e)
    {       
        // Seleciona a partir do tipo da peça
        // * qual o follow adequado para 
        // * aquela dita peça
        switch (e.GetClass.Type)
        {
            case CHESSPIECE.KING:
                e._StateMachine.ChangeState(new KingHuntState());
                break;
            case CHESSPIECE.QUEEN:
                e._StateMachine.ChangeState(new QueenHuntState());
                break;
            case CHESSPIECE.TOWER:
                e._StateMachine.ChangeState(new TowerHuntState());
                break;
            case CHESSPIECE.HORSE:
                e._StateMachine.ChangeState(new HorseHuntState());
                break;
            case CHESSPIECE.BISHOP:
                e._StateMachine.ChangeState(new BishopHuntState());
                break;
            case CHESSPIECE.PAWN:
                e._StateMachine.ChangeState(new PawnHuntState());
                break;
        }

    }


}
