using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUp : Hability
{
    public float Duration = 1;
    public float Multiplier = 2;
    public bool Dashing = false;

    public SpeedUp(Piece piece) : base(piece)
    {
        Cost = 40;
        _piece = piece;
    }

    public override bool Use()
    {
        base.Use();

        if (Dashing)
            return false;

        _piece.StartCoroutine(CSpeedUp());
        return true;
    }

    private IEnumerator CSpeedUp()
    {
        Dashing = true;
        var oldSpeed = _piece.Speed;
        _piece.Speed *= Multiplier;

        float time = 0.0f;
        var pTime = 0.0f;

        while (time < Duration)
        {
            pTime++;
            if (pTime >= 4)
            {
                pTime = 0;
                _piece.GetComponent<ParticleSystem>().Emit(1);
            }

            time += Time.deltaTime;
            yield return null;
        }

        _piece.Speed = oldSpeed;
        Dashing = false;
        yield return null;
    }
}
