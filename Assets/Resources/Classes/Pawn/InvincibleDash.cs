using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibleDash : Hability
{
    public float Distance = 3;
    public float Speed = 10;

    public bool Dodging = false;
    public float InvincibleTime = 0.5f;
    Color originalcolor = new Color();

    public InvincibleDash(Piece piece) : base(piece)
    {
        Cost = 30;
    }

    public override bool Use()
    {
        base.Use();

        originalcolor = _piece.GetColor();
        // Pega a direção que a peça está se movendo
        var dir = _piece.MovementDirection;

        if (Dodging || dir.magnitude < 0.5f)
            return false;

        _piece.StartCoroutine(CDash(dir));
        return true;
    }

    private Vector2 CheckDirection(Vector2 direction)
    {
        Vector2 toReturn = _piece.transform.position.xy() + direction * Distance;

        // Casta um raio, ve se bateu em algo que para o Dash
        var hits = Physics2D.RaycastAll(_piece.transform.position.xy(), direction, Distance);
        foreach (var hit in hits)
        {
            if (hit.collider.GetComponent(typeof(IStopDash)))
            {
                // Retorna a posição mais perto
                toReturn = hit.point - direction;
            }
        }

        return toReturn;
    }

    IEnumerator CDash(Vector2 direction)
    {
        Dodging = true;
        _piece.CanMove = false;

        Vector2 finalPos = _piece.transform.position.xy() + direction * Distance;
        Vector2 originalPos = _piece.transform.position.xy();

        float time = 0;
        var pTime = 0.0f;

        while (time < 1)
        {
            pTime++;
            if (pTime >= 3)
            {
                pTime = 0;
                _piece.GetComponent<ParticleSystem>().Emit(1);
            }

            if (!TryLerp(originalPos, finalPos, time))
                break;

            time += 0.01f * Speed;
            yield return null;
        }


        if(!_piece.IsInvincible)
            _piece.StartCoroutine(CInvincibleTime());

        _piece.CanMove = true;
        Dodging = false;

        yield return null;
    }

    IEnumerator CInvincibleTime()
    {
        _piece.SetColor(Color.white);

        _piece.IsInvincible = true;
        _piece.SetColor(Color.yellow);

        yield return new WaitForSeconds(InvincibleTime);
        //_piece.SetColor(originalcolor);

        if (_piece.GetComponent<Enemy>())
            _piece.SetColor(Color.red);
        else
            _piece.SetColor(Color.white);

        _piece.IsInvincible = false;
        yield return null;
    }


}