using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttackPattern : AttackPattern
{


    // Use this for initialization
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        if (_isenemy)
        {
            base.Update();
            if (canupdate)
                UpdateAtkArea();
        }
    }


    public override void UpdateAtkArea()
    {
        if (_angle > 0.6)
        {
            _index = 2;// left attack area
        }
        else if (_angle <= 0.6 && _angle >= -0.6)
        {
            if (transform.position.y < _target_pos.y)
            {
                _index = 0;// top attack area
            }
            else
            {
                _index = 3;// bot attack area
            }
        }
        else if (_angle < -0.6)
        {
            _index = 1;// right attack area
        }
       
        closest = areas[_index];
    }

}
