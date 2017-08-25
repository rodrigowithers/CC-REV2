using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop_NormalAttackArea : AttackArea {

    public GameObject Sicle;

    public override IEnumerator CAttack()
    {
        if (_attacking)
            yield return null;
        _attacking = true;

        // Pega a posição e manda a flecha até ela
        var sicle = Instantiate(Sicle, _parent.transform.position + (transform.position - _parent.transform.position).normalized, Quaternion.identity);
        sicle.transform.parent = transform;
        sicle.GetComponent<Sicle>().Destination = transform.position;
        sicle.GetComponent<Sicle>().Parent = _parent.transform;

        sicle.GetComponent<Sicle>()._piece = _parent.GetComponent<BattlePiece>();


        yield return new WaitForSeconds(Duration);
        _attacking = false;

        yield return null;
    }
}
