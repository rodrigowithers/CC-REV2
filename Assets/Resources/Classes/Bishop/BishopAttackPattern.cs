using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BishopAttackPattern : AttackPattern {


    // Use this for initialization
    public override void Start () {
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
        if (_angle > 0)
        {
            if (transform.position.y < _target_pos.y)
            {
                _index = 1;// top attack area
            }
            else
            {
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
                _index = 2;// bot attack area
            }
        }
        closest = areas[_index];
    }

}
