using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower_NormalAttackArea : AttackArea
{
    public GameObject CannonBall;

    public override IEnumerator CAttack()
    {
        if (_attacking)
            yield return null;

        // Instanancia uma bala e manda para a posição no meio dessa área de ataque
        var dir = (transform.position - _parent.transform.position).normalized;

        var attack = Instantiate(CannonBall, _parent.transform.position, Quaternion.identity);
        attack.GetComponent<CannonBall>().Destination = transform.position + dir / 2;
        attack.GetComponent<CannonBall>()._piece = _parent.GetComponent<BattlePiece>();

        // Empurra a torre na outra direção
        _parent.GetComponent<BattlePiece>().Pushback(-dir, 10);

        yield return null;
    }
}
