using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Horse_NormalAttackArea : AttackArea
{
    public GameObject Energy;

    public override IEnumerator CAttack()
    {
        if (_attacking)
            yield return null;
        _attacking = true;

        // Pega a posição e manda a flecha até ela
        var energy = Instantiate(Energy, transform.position, Quaternion.identity);
        energy.GetComponent<Energy>().Direction = (transform.position - _parent.transform.position).normalized;

        energy.GetComponent<Energy>()._piece = _parent.GetComponent<BattlePiece>();

        yield return new WaitForSeconds(Duration);
        _attacking = false;

        yield return null;
    }
}
