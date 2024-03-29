﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HabilityManager : MonoBehaviour
{
    private Piece _piece;

    public Hability Hability;
    public float Energy = 100;

    public float StaminaRecoverSpeed = 1;

    private bool _canRecover = true;
    public bool CanRecover
    {
        get { return _canRecover; }
        set
        {
            if (value == false)
            {
                _canRecover = value;
                StartCoroutine(CRecoverDelay());
            }
            else
            {
                _canRecover = value;
            }
        }
    }

    public bool HasStamina()
    {
        if (Hability.Cost <= Energy)
            return true;
        return false;
    }
    IEnumerator CRecoverDelay()
    {
        yield return new WaitForSeconds(1.0f);

        _canRecover = true;
        yield return null;
    }

    public bool Use()
    {
        if (Hability == null || Energy < Hability.Cost)
            return false;

        if (Hability.Use())
        {
            CanRecover = false;
            Energy -= Hability.Cost;
            return true;
        }
        return false;
    }

    private void Awake()
    {
        _piece = GetComponent<Piece>();
    }

    void Start()
    {

    }

    void Update()
    {
        if(Energy < 100 && CanRecover)
        {
            Energy += StaminaRecoverSpeed * Time.deltaTime;
        }
    }
}
