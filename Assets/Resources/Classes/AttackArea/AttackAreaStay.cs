using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAreaStay : AttackArea
{
    public override bool LockPosition
    {
        get
        {
            return true;
        }
    }
}
