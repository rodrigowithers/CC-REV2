﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPattern : MonoBehaviour {
    protected Enemy _main_script;
    protected bool _isenemy = false;
    protected float _angle = 0;
    protected int _index = 0;
    protected Vector3 _target_pos;
    protected List<Transform> areas = new List<Transform>();
    protected Transform closest = null;
    protected bool canupdate = true;

    protected Player _player;

    public Transform CurrenAtkPattern
    {
        get
        {
            return closest;
        }
        set
        {
            closest = value;
        }
    }

    public virtual void Start()
    {
        if (transform.parent.GetComponent<Enemy>() != null)
        {
            _isenemy = true;
            _main_script = transform.parent.GetComponent<Enemy>();
            if (transform.childCount != 0)
                GetAreas();
        }

        _player = Player.Instance;
       
    }

    protected void GetAreas()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            areas.Add(transform.GetChild(i));
        }
    }

    public void CanUpdate(bool c)
    {
        canupdate = c;
    }

    public virtual void Update()
    {
        _target_pos = GameManager.Instance._Player.transform.position;
        _angle = _main_script.ThisAngleFromPlayer();
    }

    public virtual void UpdateAtkArea()
    {

    }
    
}
