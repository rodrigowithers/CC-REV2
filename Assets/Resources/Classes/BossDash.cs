using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDash : Dash {

    bool stopped = false;
	public BossDash(Piece p) : base(p)
    {
        Distance = 40;
        Speed = 1;
    }

    public override bool Use()
    {
        //base.Use();

        // Pega a direção que a peça está se movendo
        var dir = _piece.MovementDirection;

        if (Dodging || dir.magnitude < 0.5f)
            return false;

        _piece.StartCoroutine(CBossDash(dir));
        return true;
    }

    IEnumerator CBossDash(Vector2 direction)
    {
        Dodging = true;
        _piece.CanMove = false;

        Vector2 finalPos = _piece.transform.position.xy() + direction * Distance;
        Vector2 originalPos = _piece.transform.position.xy();

        float time = 0;
        while (time < 1)
        {
            int blerp = BossTryLerp(originalPos, finalPos, time); 
            if (blerp == 1)
            {
               break;
            }
            else if(blerp == 2)
            {
                _piece.IsInvincible = false;
                //stopped = true;
                break;
            }

            time += 0.01f * Speed;
            yield return null;
        }

        _piece.CanMove = true;
        Dodging = false;

        
        yield return null;
    }

}
