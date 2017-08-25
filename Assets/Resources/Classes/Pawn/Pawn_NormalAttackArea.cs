using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn_NormalAttackArea : AttackArea
{
    public GameObject Lance;

    public override IEnumerator CAttack()
    {
        if (_attacking)
            yield return null;
        _attacking = true;

        // Pega a posição e manda a flecha até ela
        var lance = Instantiate(Lance, _parent.transform.position + (transform.position - _parent.transform.position).normalized, Quaternion.identity);
        lance.transform.parent = transform;
        lance.GetComponent<Lance>().Direction = transform.position - _parent.transform.position;
        lance.GetComponent<Lance>()._piece = _parent.GetComponent<BattlePiece>();


        yield return new WaitForSeconds(Duration);
        _attacking = false;

        yield return null;
    }
}
