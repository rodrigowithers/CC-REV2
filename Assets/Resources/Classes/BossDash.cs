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

        // Toca o som
        SoundManager.Play("dash");

        _piece.StartCoroutine(CBossDash(dir));
        return true;
    }

    IEnumerator CBossDash(Vector2 direction)
    {
        // Toca a animação
        float ang = _piece.ThisAngleFromPlayer();


        // Toca a animação
        _piece.GetComponent<ClassAnimator>().Stop();

        if (ang > 0.6)
        {   // left attack area
            _piece.GetComponent<ClassAnimator>().Stop();
            _piece.GetComponent<ClassAnimator>().Play("DashLeft", 0, true, true); ;
        }
        else if (ang <= 0.6 && ang >= -0.6)
        {
            if (_piece.transform.position.y < Player.Instance.transform.position.y)
            {   // top attack area
                _piece.GetComponent<ClassAnimator>().Stop();
                _piece.GetComponent<ClassAnimator>().Play("DashUp", 0, true, true); ;
            }
            else
            {   // bot attack area
                _piece.GetComponent<ClassAnimator>().Stop();
                _piece.GetComponent<ClassAnimator>().Play("DashDown", 0, true, true); ;
            }
        }
        else if (ang < -0.6)
        {   // right attack area
            _piece.GetComponent<ClassAnimator>().Stop();
            _piece.GetComponent<ClassAnimator>().Play("DashRight", 0, true, true); ;
        }


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
                // Toca o som
                SoundManager.Play("boom");

                // Toca a animação
                _piece.GetComponent<ClassAnimator>().Stop();
                _piece.GetComponent<ClassAnimator>().Play("Dizzy", 0, true, true);

                yield return new WaitForSeconds(0.5f);

                _piece.GetComponent<ClassAnimator>().Stop();
                _piece.GetComponent<ClassAnimator>().Play("DizzyLoop", 0, true, true);

                _piece.IsInvincible = false;
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
