using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueenAttackPattern : AttackPattern
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
            UpdateAtkArea();
        }
    }


    public override void UpdateAtkArea()
    {
        if (_angle < -0.8) // right
        {
            _index = 0; // right attack area
        }
        else if (_angle >= -0.8 && _angle < -0.2) // topright && bottomright
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
        else if (_angle >= -0.2 && _angle < 0.2)// top && bottom
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
        else if (_angle > 0.2 && _angle <= 0.8)// topleft && bottomleft
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
        else if (_angle > 0.8) // left
        {
            _index = 4; // left attack area
        }
        closest = areas[_index];
    }

}
