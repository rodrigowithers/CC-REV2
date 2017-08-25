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
                    GameObject go = new GameObject("PieceManager");
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

    public void AdjustFollow(Enemy e)
    {
        // Temporariamente manda todas as peças para o FollowPathState
        e._StateMachine.ChangeState(new FollowPathState());

        ///
        /* Seleciona a partir do tipo da peça
         * qual o follow adequado para 
         * aquela dita peça
        switch (e.Type)
        {
            case CHESSPIECE.KING:
                break;
            case CHESSPIECE.QUEEN:
                break;
            case CHESSPIECE.TOWER:
                break;
            case CHESSPIECE.HORSE:
                break;
            case CHESSPIECE.BISHOP:
                break;
            case CHESSPIECE.PAWN:
                break;
        }*/

    }
}
