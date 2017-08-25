using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAreaFollow : AttackArea
{
    public override bool LockPosition
    {
        get
        {
            return false;
        }
    }
}
