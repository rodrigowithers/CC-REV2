using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private bool paused = false;

    public void Pause(bool p)
    {
        paused = p;
    }
    public bool IsPaused()
    {
        return paused;
    }

    private Piece _myOwner;
    //Estado atual da máquina de estados
    [SerializeField]
    private State _currentState;
    //Estado anteriormente usado na máquina de estados
    private State _previousState;
    //Estado Global
    private State _globalState;

    public string StateName;

    // Use this for initialization
    public void StartManchine(Piece owner)
    {
        _myOwner = owner;
        _currentState = null;
        _previousState = null;
        _globalState = null;
    }


    public void SetCurrentState(State s)
    {
        _currentState = s;
    }
    public void SetGlobalState(State s)
    {
        _globalState = s;
    }
    public void SetPreviousState(State s)
    {
        _previousState = s;
    }

    //Invoca este metodo para atualizar a FSM.
    void FixedUpdate()
    {
        if (!paused)
        {
            //Se existir um estado global, invoca o seu metodo execute,
            //caso contrario nao faz nada:
            if (_globalState != null)
            {
                _globalState.Execute(_myOwner);
            }
            //Idem para o estado atual:
            if (_currentState != null)
            {
                _currentState.Execute(_myOwner);
            }
        }
    }

    private void Update()
    {
        StateName = _currentState.ToString();
    }

    //Faz a troca de estados.
    public void ChangeState(State novoEstado)
    {
        //Armazena qual o estado anterior:
        _previousState = _currentState;
        //Invoca o método de saida do estado atual:
        if (_currentState != null)
        {
            _currentState.Exit(_myOwner);

        }
        //Faz a troca de estados:
        _currentState = novoEstado;
        //Invoca o metodo de Entrada do novo estado:
        _currentState.Enter(_myOwner);
    }

    //public void ChangeState(STATETYPE s)
    //{
    //    if (s == STATETYPE.FOLLOW)
    //        Adjust_FOLLOW((Enemy)_myOwner);
    //    // else if(s.Type == STATETYPE.ATTACK)

    //    //else if (s.Type == STATETYPE.DODGE)

    //    //else if (s.Type == STATETYPE.FLEE)
    //}



    //Muda para o estado anterior:
    public void RevertToPreviousState()
    {
        ChangeState(_previousState);
    }
    public State GetCurrentState()
    {
        return _currentState;
    }
    public State GetGlobalState()
    {
        return _globalState;
    }
    public State GetPreviousState()
    {
        return _previousState;
    }




   


}
