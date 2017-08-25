using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen_NormalAttackArea : AttackArea
{
    public GameObject Arrow;

    public override IEnumerator CAttack()
    {
        if (_attacking)
            yield return null;
        _attacking = true;

        // Pega a posição e manda a flecha até ela
        var arrow = Instantiate<GameObject>(Arrow, _parent.transform.position + (transform.position - _parent.transform.position).normalized, Quaternion.identity);
        arrow.GetComponent<Arrow>().Destination = transform.position;
        arrow.GetComponent<Arrow>()._piece = _parent.GetComponent<BattlePiece>();

        yield return new WaitForSeconds(Duration);
        _attacking = false;

        yield return null;
    }
}
