using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseAttackPattern : AttackPattern
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
        bool up = true;
        if (_angle > 0)
        {
            if (transform.position.y < _target_pos.y)
            {
                _index = 1;// top attack area
            }
            else
            {
                up = false;
                _index = 3;// bot attack area
            }
        }
        else
        {
            if (transform.position.y < _target_pos.y)
            {
                _index = 0;// top attack area
            }
            else
            {
                up = false;
                _index = 2;// bot attack area
            }
        }

       
        //if (up)
        //    atk_area.localPosition.Set(0, -2.6f, 0);
        //else
        //    atk_area.localPosition.Set(0, 2.6f, 0);
        
        closest = areas[_index];
    }

}