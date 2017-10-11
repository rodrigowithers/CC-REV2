using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HabilityManager))]
public class Boss : Piece
{
    public HabilityManager _habilityManager;
    public StateMachine _stateMachine;
    public GameObject _atkArea;
    public int _life = 10;
	// Use this for initialization
	public void Start () {
        _stateMachine = GetComponent<StateMachine>();
        _habilityManager = GetComponent<HabilityManager>();
        _stateMachine.StartManchine(this);
        
    }
}
