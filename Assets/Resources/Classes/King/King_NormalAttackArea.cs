using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King_NormalAttackArea : AttackArea
{
    public GameObject Sword;

    public override IEnumerator CAttack()
    {
        if (_attacking)
            yield return null;
        _attacking = true;

        // Cria a direção
        var dir = (transform.position - _parent.transform.position).normalized;

        var sword = Instantiate(Sword, _parent.transform.position, Quaternion.identity);
        sword.transform.parent = transform;
        sword.transform.localRotation = Quaternion.FromToRotation(Vector3.up, dir);

        sword.GetComponent<Sword>()._piece = _parent.GetComponent<BattlePiece>();
        sword.GetComponent<Sword>().Direction = dir;

        yield return new WaitForSeconds(Duration);
        _attacking = false;

        yield return null;
    }
}
