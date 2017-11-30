using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingAttackPattern : AttackPattern
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
        if (_angle < -0.6) // right
        {
            _index = 0; // right attack area
        }
        else if (_angle >= -0.6 && _angle < -0.3) // topright && bottomright
        {
            if (transform.position.y < _target_pos.y)
            {
                _index = 1;// topright attack area
            }
            else
            {
                _index = 7;// botright attack area
            }
        }
        else if (_angle >= -0.3 && _angle < 0.3)// top && bottom
        {
            if (transform.position.y < _target_pos.y)
            {
                _index = 2;// top attack area
            }
            else
            {
                _index = 6;// bot attack area
            }
        }
        else if (_angle >= 0.3 && _angle < 0.6)// topleft && bottomleft
        {
            if (transform.position.y < _target_pos.y)
            {
                _index = 3; // topleft attack area
            }
            else
            {
                _index = 5; // botleft attack area
            }
        }
        else if (_angle >= 0.6) // left
        {
            _index = 4; // left attack area
        }

        closest = areas[_index];
    }

}